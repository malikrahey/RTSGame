using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMenuItem
{
    public int cost;
    public GameObject itemUnit;
    public string unitName;
    public float buildTime;

    public UnitMenuItem(int cost, GameObject unit, string unitName, float buildTime)
    {
        this.cost = cost;
        this.itemUnit = unit;
        this.unitName = unitName;
        this.buildTime = buildTime;
    }
}
