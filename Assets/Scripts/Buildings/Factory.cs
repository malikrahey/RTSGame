using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Building
{
    private void OnEnable()
    {
        StartCoroutine(ProduceResources());
        BaseHealth = 100;
        CurrentHealth = 100;
    }
    private IEnumerator ProduceResources()
    {
        while(gameObject.activeInHierarchy)
        {
            GameManager.Instance.Player.AmountOfResources += 10;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
