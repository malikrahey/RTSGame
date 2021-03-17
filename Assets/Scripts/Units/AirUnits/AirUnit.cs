using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirUnit : Unit
{

    protected float elevationHeight;
    void OnEnable()
    {
        this.CarryCapacity = 10;
        this.Carrying = 0; //start with no resources
    }


}
