using System.Collections;
using System.Collections.Generic;
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

    public Target CurrentTarget { get; set; }

    private HealthBar healthBar;


    private void Start()
    {
        healthBar = this.gameObject.GetComponentInChildren<HealthBar>();
        //lazy instantiation
        if(healthBar == null)
        {
            GameObject healthBarGO = Instantiate(GameManager.Instance.healthBarPrefab) as GameObject;
            healthBarGO.SetActive(false);
            healthBar = healthBarGO.GetComponent<HealthBar>();
            healthBarGO.transform.SetParent(this.gameObject.transform, false);

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
        this.IsBeingAttacked = isBeingAttacked;
        this.healthBar.gameObject.SetActive(isBeingAttacked);

    }
}
