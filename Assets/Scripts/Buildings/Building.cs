using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public float Health { get; set; }
    private BuildingHealth healthbar;
    public float BaseHealth { get; set; } //health of the unit

    public float CurrentHealth { get; set; }


    private void Start()
    {
        healthbar = this.gameObject.GetComponentInChildren<BuildingHealth>();
        if(healthbar == null)
        {
            GameObject buildingHealthGO = Instantiate(GameManager.Instance.BuildingHealthPrefab) as GameObject;
            buildingHealthGO.transform.position = new Vector3(50, 50, -2);
            buildingHealthGO.transform.rotation = Quaternion.Euler(-45, 180, 0);
            buildingHealthGO.SetActive(false);
            healthbar = buildingHealthGO.GetComponent<BuildingHealth>();
            Transform canvas = this.gameObject.transform.GetChild(0);
            buildingHealthGO.transform.SetParent(canvas, false);
        }
    }
    public void FadeAway()
    {

    }

    private IEnumerator FadeAwayCoroutine()
    {
        yield return null;
    }

}
