using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularResourceGroup : ResourceBehaviour
{
    void OnEnable()
    {
        this.ResourcesRemaining = 250;
    }
}
