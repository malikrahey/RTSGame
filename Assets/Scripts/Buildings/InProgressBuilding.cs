using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InProgressBuilding : MonoBehaviour
{

    public float BuildProgress { get; set; }
    public float TimeToBuild { get; set; }

    public float Health { get; set; }

    public bool IsBuilding { get; set; }

    private BuildingProject project;

    private bool isNotifiedOfFinish = false;

    public void FinishBuilding()
    {
        if (isNotifiedOfFinish) return;
        isNotifiedOfFinish = true;
        Vector3 pos = transform.position;
        GameObject newBuilding = Instantiate(project.buildingPrefab, transform) as GameObject;
        transform.gameObject.SetActive(false);
        Destroy(this.gameObject);

    }
}

public class BuildingProject : MonoBehaviour
{

    public GameObject buildingPrefab;
    public float buildTime;
    public string buildingName;

}
