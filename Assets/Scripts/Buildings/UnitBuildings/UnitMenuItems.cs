using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMenuItems : MonoBehaviour
{
    public static List<UnitMenuItems> AirUnitMenuItems;
}

public class UnitMenuItem
{
    public int Cost { get;}
    public Unit ItemUnit { get; }
    public string Name { get; }

    public float BuildTime { get; }

    public UnitMenuItem(int cost, Unit unit, string name, int buildTime)
    {
        Cost = cost;
        ItemUnit = unit;
        Name = name;
        BuildTime = buildTime;
    }
}
