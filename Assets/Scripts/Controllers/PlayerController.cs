using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 1.0f;
    private float scrollSpeed = -30.0f;

    private List<Unit> selectedUnits = new List<Unit>();

    void Start()
    {
        
    }

    void Update()
    {
        /*if(Input.GetKey(KeyCode.W))
        {
            this.gameObject.transform.Translate(Vector3.forward  * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.gameObject.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.gameObject.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.gameObject.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }*/


        int xDir = Convert.ToInt32(Input.GetKey(KeyCode.D)) - Convert.ToInt32(Input.GetKey(KeyCode.A));
        int zDir = Convert.ToInt32(Input.GetKey(KeyCode.W)) - Convert.ToInt32(Input.GetKey(KeyCode.S));

        Vector3 moveVector = new Vector3( xDir, Input.mouseScrollDelta.y * scrollSpeed, zDir);

        gameObject.transform.Translate(moveVector * moveSpeed /100);
        
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("press");
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawRay(mousePosition, Vector3.forward, Color.red);
                Debug.Log("Ray hit");
                if (hit.transform.gameObject.CompareTag("Unit"))
                {
                    Debug.Log("unit hit");
                    Unit other = hit.transform.gameObject.GetComponent<Unit>();
                    other.IsSelected = true;
                    selectedUnits.Add(other);
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
                
                Target target = new Target(hit.point);
                Debug.Log(target.Position);
                foreach(Unit unit in selectedUnits)
                {
                    unit.CurrentTarget = target;
                    unit.MoveToPosition(target.Position);
                }
            }
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
