using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider Bar;
    Unit unit;
    private bool isBuilt = false;

    void Awake()
    {
        Debug.Log("hb constructor:)");
        unit = this.gameObject.GetComponentInParent<Unit>();
        Bar = this.gameObject.GetComponentInChildren<Slider>();
        isBuilt = true;
    }
    public void ApplyDamage(float damage)
    {
    }

    /*
     * Better approach probably:
     * Bar stores unit it is on
     * when bar is active, start coroutine that keeps
     * value equal to current health / base health 
     * 
     */

    private void OnEnable()
    {
        if(isBuilt)
        StartCoroutine(UpdateBarCoroutine());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator UpdateBarCoroutine()
    {
        while(Bar.value > 0)
        {
            Bar.value = unit.CurrentHealth / unit.BaseHealth;
            yield return null;
        }
    }
}
