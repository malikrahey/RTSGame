using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 5.0f;
    private float scrollSpeed = -30.0f;

    public int AmountOfResources = 0;

    public List<Unit> selectedUnits = new List<Unit>();

    

    public GameObject carriedSite;
    public Building selectedBuilding;
    private bool isCarryingSite;

    public GameObject indicatorArrow;
    private Coroutine arrowCoroutine;

    void Start()
    {
        carriedSite = null;
    }

    void Update()
    {
        if (Time.timeScale == 0) return; //If game is paused or ended don't allow input

        int xDir = Convert.ToInt32(Input.GetKey(KeyCode.D)) - Convert.ToInt32(Input.GetKey(KeyCode.A));
        int zDir = Convert.ToInt32(Input.GetKey(KeyCode.W)) - Convert.ToInt32(Input.GetKey(KeyCode.S));

        Vector3 moveVector = new Vector3( xDir, Input.mouseScrollDelta.y * scrollSpeed, zDir);

        gameObject.transform.Translate(moveVector * moveSpeed);
        
        if(Input.GetMouseButtonDown(0))
        {
            
            Debug.Log("press");
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawRay(mousePosition, Vector3.forward*1000, Color.red);
                Debug.Log("Ray hit");
                if(carriedSite != null)
                {
                    InProgressBuilding inProgressBuilding = carriedSite.gameObject.GetComponent<InProgressBuilding>();
                    UIManager.Instance.buildingActionBar.SetActive(true);
                    isCarryingSite = false;
                    //inProgressBuilding.IsBuilding = true;
                    //carriedSite = null;
                }
                else if (hit.transform.gameObject.CompareTag("Unit"))
                {
                    Debug.Log("unit hit");
                    Unit other = hit.transform.gameObject.GetComponent<Unit>();
                    other.isSelected = true;
                    if (!selectedUnits.Contains(other))
                    {
                        selectedUnits.Add(other);
                        UIManager.Instance.selectedUnitsText.text += other.unitName + '\n';
                        UIManager.Instance.selectedUnitsText.gameObject.SetActive(true);
                    }
                }
                else if(hit.transform.gameObject.CompareTag("Building"))
                {
                    Debug.Log("Building clicked ");
                    ClearSelectedunits();
                    Building building = hit.transform.gameObject.GetComponent<Building>();
                  if(building.owner == Owner.PLAYER)
                    {
                        if (building.GetType() == typeof(UnitFactoryBuilding))
                        {
                            UnitFactoryBuilding unitFactory = building as UnitFactoryBuilding;
                            unitFactory.SetActionBar(true);
                            selectedBuilding = unitFactory;

                        }
                        else if (building.GetType() == typeof(MainBase))
                        {
                            MainBase mainBase = building as MainBase;
                            mainBase.baseActionBar.SetActive(true);
                            selectedBuilding = mainBase;
                        }
                    }
                }
                else
                {
                    ClearSelectedunits();
                }
            }
        }

        if(Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                Target target;
                if(hit.transform.gameObject.CompareTag("Unit"))
                {
                    Unit targetUnit = hit.transform.gameObject.GetComponent<Unit>();
                    target = new Target(targetUnit);
                } 
                else if(hit.transform.gameObject.CompareTag("Resource"))
                {
                    Debug.Log("Resource hit");
                    ResourceBehaviour resource = hit.transform.gameObject.GetComponent<ResourceBehaviour>();
                    target = new Target(hit.transform.position,resource);
                }
                else if(hit.transform.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log("Enemy hit");
                    Unit targetUnit = hit.transform.gameObject.GetComponent<Unit>();
                    target = new Target(targetUnit, TargetType.ENEMY);
                }
                else if(hit.transform.gameObject.CompareTag("Construction"))
                {
                    Debug.Log("Construction hit");
                    InProgressBuilding building = hit.transform.gameObject.GetComponent<InProgressBuilding>();
                    target = new Target(building);
                }
                else if(hit.transform.gameObject.CompareTag("Building"))
                {
                    Debug.Log("Building hit");
                    Building building = hit.transform.gameObject.GetComponent<Building>();
                    target = new Target(building);
                }
                else
                {
                    DisplayArrowAtPoint(hit.point);
                    target = new Target(hit.point);
                }


                Debug.Log(target.Position);
                foreach(Unit unit in selectedUnits)
                {
                    unit.currentTarget = target;
                    unit.InteractWithTarget(target);
                }
            }
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            GameManager.Instance.PauseGame();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            ClearSelectedunits();
            //place construction site
            if(carriedSite == null)
            {
                GameObject site = Instantiate(GameManager.Instance.constructionPrefab) as GameObject;
                carriedSite = site;
                isCarryingSite = true;
            }
            else
            {
                Destroy(carriedSite.gameObject);
                carriedSite = null;
                isCarryingSite = false;
            }
            
        }
        if(carriedSite != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.transform.CompareTag("Ground") && isCarryingSite)
                {
                    carriedSite.transform.position = hit.point;
                }
            }
        }
    }

    private void ClearSelectedunits()
    {
        foreach(Unit unit in selectedUnits)
        {
            unit.isSelected = false;
        }
        selectedUnits.Clear();
        UIManager.Instance.selectedUnitsText.text = "";
        UIManager.Instance.selectedUnitsText.gameObject.SetActive(false);
    }

    private void DisplayArrowAtPoint(Vector3 position)
    {
        if(arrowCoroutine != null) StopCoroutine(arrowCoroutine);
        indicatorArrow.transform.position = new Vector3(position.x, position.y + 2, position.z);
        arrowCoroutine = StartCoroutine(DisplayArrowCoroutine());

    }

    private IEnumerator DisplayArrowCoroutine()
    {
        indicatorArrow.SetActive(true);
        int counts = 0;
        while(counts < 200)
        {
            indicatorArrow.transform.Rotate(new Vector3(0, 1, 0));
            yield return new WaitForSeconds(0.005f);
            counts++;
        }
        
        indicatorArrow.SetActive(false);
    }
}
