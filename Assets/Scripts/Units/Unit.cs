using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    protected GameObject Body { get; }   
    
    [SerializeField]
    public bool IsSelected { get; set; } //is Selected by the player
    protected bool IsCarryingResources { get; set; } //is carrying resources


    protected float BaseSpeed { get; set; }  //base moving speed for the unit
    protected float TransportatingModifier { get; } //speed modifier for when carrying resources
    protected float BaseHealth { get; } //health of the unit

    protected float AttackRange { get; }

    public Target CurrentTarget { get; set; }


    public void InteractWithTarget(Target target)
    {
        switch(target.Type)
        {
            case TargetType.POSITION:
                break;
            case TargetType.ENEMY:
                break;
            case TargetType.RESOURCE:
                break;

        }
    }


    public void MoveToPosition(Vector3 position)
    {
        StopAllCoroutines();
        TurnToTarget(position);
        StartCoroutine(MoveCoroutine(position));
    }

    private IEnumerator MoveCoroutine(Vector3 position)
    {
        while(Vector3.Distance(position,this.gameObject.transform.position) > 0.05)
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, position, BaseSpeed/1000);
            yield return null;
        }
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
    
}
