using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarContainer : MonoBehaviour
{
    [SerializeField]
    Slider Bar;
    [SerializeField]
    InProgressBuilding IPbuilding;
    private bool isBuilt = false;

    void Awake()
    {
        Debug.Log("hb constructor:)");
        IPbuilding = this.gameObject.GetComponentInParent<InProgressBuilding>();
        Bar = this.gameObject.GetComponentInChildren<Slider>(); 
        isBuilt = true;
    }
    private void OnEnable()
    {        
        IPbuilding = this.gameObject.GetComponentInParent<InProgressBuilding>();              
        if (IPbuilding != null)
        {
            Debug.Log("Building good");
        }
        if (Bar != null)
        {
            Debug.Log("Bar good");
        }
        Debug.Log("Building progress Enabled");
        if(isBuilt)
        {
            StartCoroutine(UpdateBPCoroutine());
        }
           

    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator UpdateBPCoroutine()
    {
        while(Bar.value < 1)
        {
            Bar.value = IPbuilding.BuildProgress;
            yield return null;
        }            
    }
}
