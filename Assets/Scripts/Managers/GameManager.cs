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
    public GameObject BuildingprogressPrefab;


    public PlayerController Player;
    public AIController AIPlayer;

    public List<BuildingProject> buildingProjects = new List<BuildingProject>();

    public int numberOfPlayerUnits;
    public int numberOfAIUnits;

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

    private void Start()
    {
        numberOfPlayerUnits = 2;
        numberOfAIUnits = 2;
    }

   

    public void PlayerWins()
    {
        UIManager.Instance.winnerText.text = "You win!";
        EndGame();
    }

    public void AIWins()
    {
        UIManager.Instance.winnerText.text = "You Lost";
        EndGame();
    }

    private void EndGame()
    {
        Time.timeScale = 0;
        UIManager.Instance.gameOverPanel.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        UIManager.Instance.pauseMenu.SetActive(true);
    }
}
