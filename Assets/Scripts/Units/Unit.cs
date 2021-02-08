using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    protected GameObject body { get; }   
    
    [SerializeField]
    public bool IsSelected { get; set; } //is Selected by the player
    protected bool IsCarryingResources { get; set; } //is carrying resources


    protected float BaseSpeed { get; set; }  //base moving speed for the unit
    protected float TransportatingModifier { get; } //speed modifier for when carrying resources
    protected float BaseHealth { get; } //health of the unit

    public Target CurrentTarget { get; set; }

    public void MoveToPosition(Vector3 position)
    {
        StopAllCoroutines();
        StartCoroutine(MoveCoroutine(position));
    }

    private IEnumerator MoveCoroutine(Vector3 position)
    {
        while(Vector3.Distance(position,this.gameObject.transform.position) > 0.05)
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, position, BaseSpeed/1000);
            yield return null;
        }
        Debug.Log("Coroutine exit"); 
        
    }
    
}
