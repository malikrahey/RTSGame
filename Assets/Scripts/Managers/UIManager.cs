
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    public GameObject actionBarItemPrefab;
    public Text resourceText;
    public Text timerText;
    public Text selectedUnitsText;
    public Text notEnoughResourceText;
    public Text winnerText;

    public GameObject actionBar;

    public GameObject buildingActionBar;
    public GameObject hangarActionBar;
    public GameObject garageActionBar;

    public GameObject gameOverPanel;
    public GameObject pauseMenu;

    private float Timer;


    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        Timer = 0.0f;
    }

    private void Update()
    {
        resourceText.text = GameManager.Instance.Player.AmountOfResources.ToString();
        Timer += Time.deltaTime;
        int mins = (int)Timer / 60;
        int secs = (int)Timer % 60;
        string minsText = mins >= 10 ? mins.ToString() : "0" + mins.ToString();
        string secsText = secs >= 10 ? secs.ToString() : "0" + secs.ToString();
        timerText.text = minsText + ":" + secsText;
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
  
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
