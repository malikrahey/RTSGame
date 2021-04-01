using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public float Health { get; set; }
    private HealthBar healthbar;


    private void Start()
    {
        healthbar = this.gameObject.GetComponentInChildren<HealthBar>();
        if(healthbar == null)
        {
            GameObject healthBarGO = Instantiate(GameManager.Instance.healthBarPrefab) as GameObject;
            healthBarGO.transform.position = new Vector3(50, 50, -2);
            healthBarGO.transform.rotation = Quaternion.Euler(-45, 180, 0);
            healthBarGO.SetActive(false);
            healthbar = healthBarGO.GetComponent<HealthBar>();
            Transform canvas = this.gameObject.transform.GetChild(0);
            healthBarGO.transform.SetParent(canvas, false);
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
