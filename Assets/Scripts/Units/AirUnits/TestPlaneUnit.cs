using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlaneUnit : AirUnit
{
    public TestPlaneUnit()
    {
        this.BaseSpeed = 5.0f;
        this.AttackRange = 10.0f;
        this.AttackSpeed = 0.1f;
        this.AttackStrength = 3f;
        this.BaseHealth = 50f;
        this.CurrentHealth = 50f;
        this.CollectionRate = 10;
    }
}
