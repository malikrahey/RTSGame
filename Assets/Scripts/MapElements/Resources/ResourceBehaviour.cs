using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBehaviour : MonoBehaviour
{
   public int ResourcesRemaining { get;  set; }


    public int TakeResources(int otherCollectRate)
    {
        if(this.ResourcesRemaining > otherCollectRate)
        {
            ResourcesRemaining -= otherCollectRate;
            Debug.Log("Recources taken, resources left = " + this.ResourcesRemaining.ToString());
            return otherCollectRate;
        }
        else
        {
            int resourcesLeft = ResourcesRemaining;
            ResourcesRemaining = 0;
            Debug.Log("resources gone");
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
