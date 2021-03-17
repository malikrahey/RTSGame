using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBehaviour : MonoBehaviour
{
   protected int ResourcesRemaining { get; set; }


    public int TakeResources(int otherCarryCapacity)
    {
        if(this.ResourcesRemaining > otherCarryCapacity)
        {
            Debug.Log("Resources collected");
            ResourcesRemaining -= otherCarryCapacity;
            return otherCarryCapacity;
        }
        else
        {
            int resourcesLeft = ResourcesRemaining;
            ResourcesRemaining = 0;
            StartCoroutine(FadeAway());
            return resourcesLeft;
        }
    }

    private IEnumerator FadeAway()
    {
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();
        Material material = renderer.material;
        while(material.color.a > 0)
        {
            material.color = new Color(material.color.r, material.color.g, material.color.b, material.color.a - 5);

            yield return new WaitForSeconds(0.1f);
        }
        this.gameObject.SetActive(false);
    }

}
