using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    public GameObject actionBarItemPrefab;
    public Text resourceText;
    public Text timerText;
    public GameObject actionBar;

    public GameObject buildingActionBar;

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

}
