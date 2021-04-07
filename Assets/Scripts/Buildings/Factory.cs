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
            switch(owner)
            {
                case Owner.PLAYER:
                    GameManager.Instance.Player.AmountOfResources += 10;
                    break;
                case Owner.AI:
                    GameManager.Instance.AIPlayer.amountOfResources += 10;
                    break;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
