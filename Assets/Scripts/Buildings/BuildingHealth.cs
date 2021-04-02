using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingHealth : MonoBehaviour
{
    [SerializeField]
    Slider Bar;
    [SerializeField]
    Building building;
    private bool isBuilt = false;

    void Awake()
    {
        Debug.Log("hb constructor:)");
        building = this.gameObject.GetComponentInParent<Building>();
        Bar = this.gameObject.GetComponentInChildren<Slider>();
        isBuilt = true;
    }
    public void ApplyDamage(float damage)
    {
    }


    private void OnEnable()
    {
        building = this.gameObject.GetComponentInParent<Building>();
        Debug.Log("Building helath Enabled");
        if (isBuilt)
        {
            StartCoroutine(UpdateBHCoroutine());
            Debug.Log("Is Built");
        }

    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator UpdateBHCoroutine()
    {
        while (Bar.value > 0)
        {
            Bar.value = building.CurrentHealth / building.BaseHealth;
            yield return null;
        }
    }
}
