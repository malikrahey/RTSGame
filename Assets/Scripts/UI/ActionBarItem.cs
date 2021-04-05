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
        Text[] texts = GetComponentsInChildren<Text>();
        Text nameText = texts[0];
        Text costText = texts[1];
        nameText.text = Name;
        costText.text = Cost.ToString();
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
        if(BuyItem())
        {
            Debug.Log("Clicked the onlckick clicker");
            GameObject site = GameManager.Instance.Player.carriedSite;
            InProgressBuilding inProgressBuilding = site.gameObject.GetComponent<InProgressBuilding>();
            Debug.Log(Name);
            inProgressBuilding.BuildName = this.Name;
            inProgressBuilding.StartBuilding();

            GameManager.Instance.Player.carriedSite = null;
            
        }
        else
        {
            UIManager.Instance.DisplayNotEnoughResourceText();
            Destroy(GameManager.Instance.Player.carriedSite);
            GameManager.Instance.Player.carriedSite = null;
        }
        UIManager.Instance.buildingActionBar.SetActive(false);
    }

    private void BuildUnit()
    {
        UnitFactoryBuilding factory = GameManager.Instance.Player.selectedBuilding as UnitFactoryBuilding;
        if (BuyItem())
        {
            
            UnitMenuItem unit = new UnitMenuItem(Cost, unitItem, name, BuildTime);
            factory.BuildUnit(unit);       
        }
        else
        {
            UIManager.Instance.DisplayNotEnoughResourceText();
        }
        factory.SetActionBar(false);
    }

    private bool BuyItem()
    {
        if(Cost <= GameManager.Instance.Player.AmountOfResources)
        {
            GameManager.Instance.Player.AmountOfResources -= Cost;
            return true;
        }
        return false;
    }
}
