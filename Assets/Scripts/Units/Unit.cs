using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{

    protected GameObject Body { get; }   
    
    [SerializeField]
    public bool IsSelected { get; set; } //is Selected by the player
    public bool IsBeingAttacked { get; set; }
    protected bool IsCarryingResources { get; set; } //is carrying resources

    protected float BaseSpeed { get; set; }  //base moving speed for the unit
    protected float TransportatingModifier { get; } //speed modifier for when carrying resources
    public float BaseHealth { get; set; } //health of the unit

    public float CurrentHealth { get; set; }

    protected float AttackRange { get; set; }

    protected float AttackSpeed { get; set; }

    protected float AttackStrength { get; set; }

    protected int CollectionRate { get; set; }

    protected float BuildSpeed { get; set; }

    public Target CurrentTarget { get; set; }

    private HealthBar healthBar;


    private void Start()
    {
        healthBar = this.gameObject.GetComponentInChildren<HealthBar>();
        //lazy instantiation
        if(healthBar == null)
        {
            GameObject healthBarGO = Instantiate(GameManager.Instance.healthBarPrefab) as GameObject;
            healthBarGO.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            healthBarGO.SetActive(false);
            healthBar = healthBarGO.GetComponent<HealthBar>();
            Transform canvas = this.gameObject.transform.GetChild(0);
            healthBarGO.transform.SetParent(canvas, false);

        }
    }

    public void InteractWithTarget(Target target)
    {
        switch(target.Type)
        {
            case TargetType.POSITION:
                this.MoveToPosition(target.Position);
                break;
            case TargetType.ENEMY:
                this.HandleAttackingEnemy(target);
                break;
            case TargetType.RESOURCE:
                this.HandleCollectResources(target);
                break;
            case TargetType.CONSTRUCTION:
                break;
            default:
                break;

        }
    }


    public void MoveToPosition(Vector3 position)
    {
        StopAllCoroutines();
        TurnToTarget(position);
        StartCoroutine(MoveToPositionCoroutine(position));
    }

    private IEnumerator MoveToPositionCoroutine(Vector3 position)
    {
        while(Vector3.Distance(position,this.gameObject.transform.position) > 0.05)
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, position, BaseSpeed/1000);
            yield return null;
        }
    }

    private void HandleCollectResources(Target target)
    {
        StopAllCoroutines();
        this.TurnToTarget(target.Position);
        this.MoveToTarget(target, 5.5f);
        this.CollectResources(target);
    }

    private void HandleAttackingEnemy(Target target)
    {
        StopAllCoroutines();
        this.TurnToTarget(target.Position);
        this.MoveToTarget(CurrentTarget, AttackRange);
        this.AttackEnemy(CurrentTarget.TargetUnit);
    }

    private void TurnToTarget(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized; //DIR AB = B - A
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        StartCoroutine(TurnToTargetCoroutine(targetRotation));
    }

    private IEnumerator TurnToTargetCoroutine(Quaternion lookDirection)
    {
        
        while(Quaternion.Dot(transform.rotation, lookDirection) > 0.05f)
        {

            transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, 0.05f);
            yield return null;
        }
        
    }


    private void MoveToTarget(Target target, float range)
    {
        StartCoroutine(MoveToTargetCoroutine(target, range));
    }

    private IEnumerator MoveToTargetCoroutine(Target target, float range)
    {
        while(Vector3.Distance(target.Position, transform.position) > range)
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, target.Position, BaseSpeed / 1000);
            yield return null;
        }
    }

    private void AttackEnemy(Unit other)
    {
        StartCoroutine(AttackEnemyCoroutine(other));
        other.SetBeingAttacked(true);
    }

    private IEnumerator AttackEnemyCoroutine(Unit other)
    {
        while (Vector3.Distance(transform.position, other.transform.position) > (this.AttackRange + 2f)) { yield return null; }
        while (other.CurrentHealth > 0 && Vector3.Distance(transform.position, other.transform.position) < (this.AttackRange + 2f))
        {
            Debug.Log(Vector3.Distance(transform.position, other.transform.position));
            other.CurrentHealth -= this.AttackStrength;
            Debug.Log("shot fired");
            yield return new WaitForSeconds(this.AttackSpeed);
        }
        other.DeathFade();
    }

    private void HandleConstruction(Target target)
    {
        StopAllCoroutines();
        TurnToTarget(target.Position);
        MoveToTarget(target, 5);
        StartCoroutine(BuildConstructionCoroutine(target.ConstructionBuilding));

    }

    private IEnumerator BuildConstructionCoroutine(InProgressBuilding inProgressBuilding)
    {
        while(inProgressBuilding.BuildProgress < 1)
        {
            inProgressBuilding.BuildProgress += BuildSpeed;
            yield return new WaitForSeconds(1);
        }
    }
    
    public void DeathFade()
    {
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();

        Material[] materials = renderer.materials;
        StopAllCoroutines();
        StartCoroutine(DeathFadeAwayCoroutine(materials));
    }

    private IEnumerator DeathFadeAwayCoroutine(Material[] materials)
    {
        while(materials[0].color.a > 0)
        {
            foreach(Material material in materials)
            {
                
                material.color = new Color(material.color.r, material.color.g, material.color.b, material.color.a - 0.01f);
            }

            yield return new WaitForSeconds(0.01f) ;
        }
        this.gameObject.SetActive(false);
    }

    /*
     * Handles all needed functions calls when a unit is being attakced
     * 
     */
    public void SetBeingAttacked(bool isBeingAttacked)
    {
        Debug.Log("Is being attacked: " + isBeingAttacked.ToString());
        this.IsBeingAttacked = isBeingAttacked;
        this.healthBar.gameObject.SetActive(isBeingAttacked);

    }

    private void CollectResources(Target target)
    {
        ResourceBehaviour resource = target.ResourceGroup;
        StartCoroutine(CollectResourcesCoroutine(resource));
    }

    private IEnumerator CollectResourcesCoroutine(ResourceBehaviour resource)
    {
        while (Vector3.Distance(resource.transform.position, this.transform.position) > 6) yield return null;
        while(resource.ResourcesRemaining > 0)
        {
            GameManager.Instance.Player.AmountOfResources += resource.TakeResources(this.CollectionRate);
            yield return new WaitForSeconds(1);
        }
    }
}
