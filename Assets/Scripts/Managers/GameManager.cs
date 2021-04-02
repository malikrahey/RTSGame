using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; }}

    public GameObject healthBarPrefab;
    public GameObject constructionPrefab;
    public GameObject BuildingHealthPrefab;


    public PlayerController Player;

    public List<BuildingProject> buildingProjects = new List<BuildingProject>();

    private void Awake()
    {
        _instance = this;

        var projects = Resources.LoadAll("Assets/Prefabs/Buildings/BuildingProjects");
        foreach(var obj in projects)
        {
            Debug.Log(obj);
            GameObject go = obj as GameObject;
            BuildingProject project = go.GetComponent<BuildingProject>();
            //buildingProjects.Add(project);
        }
    }
}
