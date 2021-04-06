using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    public static bool paused = false;

    public GameObject actionBarItemPrefab;
    public Text resourceText;
    public Text timerText;
    public Text selectedUnitsText;
    public Text notEnoughResourceText;
    public GameObject actionBar;

    public GameObject buildingActionBar;
    public GameObject hangarActionBar;
    public GameObject garageActionBar;

    public GameObject pauseUI;

    private int[] timer = new int[2];


    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {

    }

    private void Update()
    {
        resourceText.text = GameManager.Instance.Player.AmountOfResources.ToString() + " : [] ";
        //Pause functionality
        if (paused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                resumeGame();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseGame();
            }
            else
            {
                pauseUI.SetActive(false);
            }
        }
    }

    public void AddActionBarItemToBar(ActionBarItem item)
    {
        GameObject itemGO = Instantiate(actionBarItemPrefab) as GameObject;
        ActionBarItem oldItem = itemGO.GetComponent<ActionBarItem>();
        oldItem.Name = item.Name;
        itemGO.SetActive(true);
    }

    public void ClearActionBar()
    {

    }

    public void DisplayNotEnoughResourceText()
    {
        StartCoroutine(NotEnoughResourceTextCoroutine());
    }

    private IEnumerator NotEnoughResourceTextCoroutine()
    {
        notEnoughResourceText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        notEnoughResourceText.gameObject.SetActive(false);
    }

    public void resumeGame()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }

    public void pauseGame()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }

    public void menu()
    {
        SceneManager.LoadScene("Menu");
        paused = false;
    }

    public void quitter()
    {
        Application.Quit();
    }

}

