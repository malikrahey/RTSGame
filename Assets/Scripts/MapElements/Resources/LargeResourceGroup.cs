using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeResourceGroup : ResourceBehaviour
{
    void OnEnable()
    {
        this.ResourcesRemaining = 250;
    }
}
