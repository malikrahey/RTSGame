using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InProgressBuilding : MonoBehaviour
{

    public float BuildProgress = 0;

    public float Health { get; set; }

    public bool IsBuilding { get; set; }
    public string BuildName { get; set; }

    [SerializeField]
    private BuildingProject project;

    private bool isNotifiedOfFinish = false;

    private ProgressBarContainer progressBar;

    private void Start()
    {
        BuildProgress = 0;
        progressBar = this.gameObject.GetComponentInChildren<ProgressBarContainer>();
        if (progressBar == null)
        {
            GameObject buildingProgressGO = Instantiate(GameManager.Instance.BuildingprogressPrefab) as GameObject;
            buildingProgressGO.transform.position = new Vector3(50, 50, -2);
            buildingProgressGO.transform.rotation = Quaternion.Euler(-45, 180, 0);
            buildingProgressGO.SetActive(false);
            progressBar = buildingProgressGO.GetComponent<ProgressBarContainer>();
            Transform canvas = this.gameObject.transform.GetChild(0);
            buildingProgressGO.transform.SetParent(canvas, false);
        }
    }

    public void StartBuilding()
    {
        Debug.Log(BuildName);
        foreach(BuildingProject project in GameManager.Instance.buildingProjects)
        {

            Debug.Log(project.buildingName);
            if(project.buildingName == BuildName)
            {
                Debug.Log("Match found");
                this.project = project;
                Debug.Log(project.buildingPrefab);
            }
        }
        Health = 100;
        BuildProgress = 0;
    }

    public void ProgressBuild(float unitBuildSpeed)
    {
        this.progressBar.gameObject.SetActive(true);
        this.BuildProgress += unitBuildSpeed / project.buildTime;
    }

    public void FinishBuilding()
    {
        if (isNotifiedOfFinish) return;
        isNotifiedOfFinish = true;
        Vector3 pos = transform.position;
        GameObject newBuilding = Instantiate(project.buildingPrefab) as GameObject;
        newBuilding.transform.position = transform.position;
        transform.gameObject.SetActive(false);
        Destroy(this.gameObject);

    }
}

