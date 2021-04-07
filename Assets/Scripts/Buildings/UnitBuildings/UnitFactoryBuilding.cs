using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FactoryType
{
    GARAGE,
    HANGAR
}

public class UnitFactoryBuilding : Building
{
    public List<UnitMenuItem> items;

    public GameObject actionBar;

    protected float currentBuildProgress;
    public float buildingBuildSpeed;
    public FactoryType type;
    

    public void BuildUnit(UnitMenuItem unit)
    {
        currentBuildProgress = 0;
        StartCoroutine(BuildUnitCoroutine(unit));
    }

    public void SetActionBar(bool active)
    {
        Debug.Log(this.gameObject.name);
        switch(type)
        {
            case FactoryType.HANGAR:
                Debug.Log("Building is a hangar");
                UIManager.Instance.hangarActionBar.SetActive(active);
                break;
            case FactoryType.GARAGE:   
                Debug.Log("Building is a garage");
                UIManager.Instance.garageActionBar.SetActive(active);
                break;
        }

    }

    private IEnumerator BuildUnitCoroutine(UnitMenuItem unit)
    {
        while(currentBuildProgress < 1)
        {
            Debug.Log(currentBuildProgress);
            currentBuildProgress += buildingBuildSpeed / unit.buildTime;
            yield return new WaitForSeconds(1);
        }
        GameObject go = Instantiate(unit.itemUnit) as GameObject;
        Unit unitScript = go.GetComponent<Unit>();
        GameManager.Instance.Player.availableUnits.Add(unitScript); //AI unit building doesnt go through here so this is fine
        go.transform.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);

    }


}
