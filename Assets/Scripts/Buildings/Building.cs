using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public float Health { get; set; }


    public void FadeAway()
    {

    }

    private IEnumerator FadeAwayCoroutine()
    {
        yield return null;
    }

}
