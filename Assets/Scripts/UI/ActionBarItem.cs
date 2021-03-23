using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBarItem : MonoBehaviour
{
    public string Name;
    public int Cost;
    public int BuildTime;

    private void OnEnable()
    {
        Text text = GetComponentInChildren<Text>();
        text.text = Name;
    }

    public void OnClick()
    {
        Debug.Log("Clicked the onlckick clicker");
        GameObject site = GameManager.Instance.Player.carriedSite;
        //InProgressBuilding inProgressBuilding = site.gameObject.GetComponent<InProgressBuilding>();
        //inProgressBuilding.IsBuilding = true;
        UIManager.Instance.buildingActionBar.SetActive(false);
    }

}
