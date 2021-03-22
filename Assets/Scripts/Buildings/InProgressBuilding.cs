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

}

class BuildingProject
{

    Building building;
    float buildTime;
    string name;

    BuildingProject(Building building, float buildTime, string name)
    {
        this.building = building;
        this.buildTime = buildTime;
        this.name = name;
    }

}
