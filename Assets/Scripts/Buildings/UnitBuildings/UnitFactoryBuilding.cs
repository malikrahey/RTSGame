using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFactoryBuilding : Building
{
    public List<UnitMenuItem> items;

    public GameObject actionBar;

    protected float currentBuildProgress;
    public float buildingBuildSpeed;
    

    public void BuildUnit(UnitMenuItem unit)
    {
        currentBuildProgress = 0;
        StartCoroutine(BuildUnitCoroutine(unit));
    }

    public void SetActionBar(bool active)
    {
        Debug.Log(this.gameObject.name);
        if (this.gameObject.name.Contains("hangar"))
        {
            Debug.Log("Building is a hangar");
            UIManager.Instance.hangarActionBar.SetActive(active);
        }
        else if(this.gameObject.name.Contains("Garage"))
        {
            Debug.Log("Building is a garage");
            UIManager.Instance.garageActionBar.SetActive(active);
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
        go.transform.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);

    }


}
