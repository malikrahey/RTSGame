using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBase : Building
{
    public GameObject dronePrefab;
    public GameObject baseActionBar;

    private float currentBuildProgress;

    public void BuildDrone()
    {
        currentBuildProgress = 0;
        StartCoroutine(BuildDroneCoroutine());
    }

    private IEnumerator BuildDroneCoroutine()
    {
        while (currentBuildProgress < 1)
        {
            Debug.Log(currentBuildProgress);
            currentBuildProgress += 0.2f;
            yield return new WaitForSeconds(0.5f);
        }
        GameObject go = Instantiate(dronePrefab) as GameObject;
        go.transform.position = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
    }

    public override void DeathFade()
    {
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();

        Material[] materials = renderer.materials;
        StopAllCoroutines();
        StartCoroutine(FadeAwayCoroutine(materials));
        //EndGame
        Debug.Log("Game Endind");
        switch(owner)
        {
            case Owner.PLAYER:
                GameManager.Instance.AIWins();
                break;
            case Owner.AI:
                GameManager.Instance.PlayerWins();
                break;
        }
    }
}
