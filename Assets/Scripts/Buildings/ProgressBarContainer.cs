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

    void Start()
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
        Bar = this.gameObject.GetComponentInChildren<Slider>();
        if (Bar != null)
        {
            Debug.Log("Bar good");
        }
        Debug.Log("Building progress Enabled");
        
        StartCoroutine(UpdateBPCoroutine());
           

    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator UpdateBPCoroutine()
    {
            Bar.value = IPbuilding.BuildProgress;
            yield return null;
    }
}
