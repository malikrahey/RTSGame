using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 5.0f;
    private float scrollSpeed = -30.0f;

    public int AmountOfResources { get; set; }

    private List<Unit> selectedUnits = new List<Unit>();

    public GameObject carriedSite;

    void Start()
    {
        carriedSite = null;
    }

    void Update()
    {


        int xDir = Convert.ToInt32(Input.GetKey(KeyCode.D)) - Convert.ToInt32(Input.GetKey(KeyCode.A));
        int zDir = Convert.ToInt32(Input.GetKey(KeyCode.W)) - Convert.ToInt32(Input.GetKey(KeyCode.S));

        Vector3 moveVector = new Vector3( xDir, Input.mouseScrollDelta.y * scrollSpeed, zDir);

        gameObject.transform.Translate(moveVector * moveSpeed *Time.deltaTime);
        
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
                    inProgressBuilding.IsBuilding = true;
                    carriedSite = null;
                }
                else if (hit.transform.gameObject.CompareTag("Unit"))
                {
                    Debug.Log("unit hit");
                    Unit other = hit.transform.gameObject.GetComponent<Unit>();
                    other.IsSelected = true;
                    selectedUnits.Add(other);
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
                    //construct
                    target = new Target(hit.point);
                }
                else
                {
                    target = new Target(hit.point);
                }


                Debug.Log(target.Position);
                foreach(Unit unit in selectedUnits)
                {
                    unit.CurrentTarget = target;
                    unit.InteractWithTarget(target);
                }
            }
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            //place construction site
            GameObject site = Instantiate(GameManager.Instance.constructionPrefab) as GameObject;
            carriedSite = site;
        }
        if(carriedSite != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.transform.CompareTag("Ground"))
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
            unit.IsSelected = false;
        }
        selectedUnits.Clear();
    }
}
