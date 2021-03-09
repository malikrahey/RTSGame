using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; }}

    public GameObject healthBarPrefab;

    private void Awake()
    {
        _instance = this;
    }
}
