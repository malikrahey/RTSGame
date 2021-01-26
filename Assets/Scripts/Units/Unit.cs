using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    protected GameObject body { get; }   
    

    protected bool isSelected { get; set; } //is Selected by the player
    protected bool isCarryingResources { get; set; } //is carrying resources


    protected float baseSpeed { get; set; }  //base moving speed for the unit
    protected float transportatingModifier { get; } //speed modifier for when carrying resources
    protected float baseHealth { get; } //health of the unit

    public void MoveToPosition(Vector3 position)
    {
        StartCoroutine(MoveCoroutine(position));
    }

    private IEnumerator MoveCoroutine(Vector3 position)
    {
        while(Vector3.Distance(position,this.gameObject.transform.position) > 0.05)
        {
            Vector3.Lerp(this.gameObject.transform.position, position, baseSpeed);
            yield return 0.05f;
        }
        yield return null; 
        
    }
    
}
