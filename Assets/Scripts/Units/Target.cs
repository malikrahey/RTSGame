using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target
{

    public Vector3 Position { get; set; }
    public Unit TargetUnit { get; set; }

    public Target()
    {

    }
    public Target(Vector3 position)
    {
        this.Position = position;
        this.TargetUnit = null;
    }

    public Target(Unit target)
    {
        this.Position = target.transform.position;
        this.TargetUnit = target;
    }


}
