using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowCar : Unit
{
    LowCar()
    {
        this.BaseSpeed = 3.0f;
        this.AttackRange = 15.0f;
        this.AttackSpeed = 0.4f;
        this.AttackStrength = 5f;
        this.BaseHealth = 200f;
        this.CurrentHealth = 200f;
        this.CollectionRate = 10;
        this.BuildSpeed = 5.0f;
    }
}
