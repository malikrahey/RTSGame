using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ActionType
{ 
    BUILD_BUILDING,
    BUILD_UNIT
}


public class ActionBarItem : MonoBehaviour
{
    public string Name;
    public int Cost;
    public int BuildTime;
    public ActionType type;

    public GameObject unitItem;

    private void OnEnable()
    {
        Text text = GetComponentInChildren<Text>();
        text.text = Name;
        Name = text.text;
    }

    public void OnClick()
    {
        switch(type)
        {
            case ActionType.BUILD_BUILDING:
                BuildBuilding();
                break;
            case ActionType.BUILD_UNIT:
                BuildUnit();
                break;
            default:
                break;
        }
            
        
    }

    private void BuildBuilding()
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

    private void BuildUnit()
    {
        UnitFactoryBuilding factory = GameManager.Instance.Player.selectedBuilding as UnitFactoryBuilding;
        UnitMenuItem unit = new UnitMenuItem(Cost,unitItem,name,BuildTime);
        factory.BuildUnit(unit);

        factory.SetActionBar(false);
    }
}
