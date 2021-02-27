using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallResourceGroup : ResourceBehaviour
{
    void OnEnable()
    {
        this.ResourcesRemaining = 100;
    }

}
