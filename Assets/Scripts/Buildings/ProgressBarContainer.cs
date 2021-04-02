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
        Debug.Log("Building helath Enabled");
        if (isBuilt)
        {
            StartCoroutine(UpdateBPCoroutine());
            Debug.Log("Is Built");
        }

    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator UpdateBPCoroutine()
    {
        while (Bar.value <= 1)
        {
            Bar.value = IPbuilding.BuildProgress;
            yield return null;
        }
    }
}
