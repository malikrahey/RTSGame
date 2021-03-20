﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TargetType
{
    POSITION,
    ENEMY,
    RESOURCE
}

public class Target
{

    public Vector3 Position { get; set; }
    public Unit TargetUnit { get; set; }

    public ResourceBehaviour ResourceGroup {get;set;}

    public TargetType Type { get; set; }

    public Target()
    {
        this.Type = TargetType.POSITION;
    }
    public Target(Vector3 position)
    {
        this.Position = position;
        this.TargetUnit = null;
        this.Type = TargetType.POSITION;
    }

    public Target(Unit target)
    {
        this.Position = target.transform.position;
        this.TargetUnit = target;
        this.Type = TargetType.ENEMY;
    }

    public Target(Unit target, TargetType type)
    {
        this.Position = target.transform.position;
        this.TargetUnit = target;
        this.Type = type;
    }

    public Target(Vector3 position, ResourceBehaviour resource)
    {
        this.Position = position;
        this.ResourceGroup = resource;
        this.TargetUnit = null;
        this.Type = TargetType.RESOURCE;
    }


}
