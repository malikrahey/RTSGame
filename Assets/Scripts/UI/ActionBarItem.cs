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
        Name = text.text;
    }

    public void OnClick()
    {
        Debug.Log("Clicked the onlckick clicker");
        GameObject site = GameManager.Instance.Player.carriedSite;
        InProgressBuilding inProgressBuilding = site.gameObject.GetComponent<InProgressBuilding>();
        Debug.Log(Name);
        inProgressBuilding.BuildName = this.Name;
        inProgressBuilding.StartBuilding();

        GameManager.Instance.Player.carriedSite = null;
        UIManager.Instance.buildingActionBar.SetActive(false);
    }

}
