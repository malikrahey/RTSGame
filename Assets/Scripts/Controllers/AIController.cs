using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AIState
{
    GATHER,
    ATTACK,
    DEFEND,
    BUILDUP

}


public class AIController : MonoBehaviour
{

    public List<GameObject> unitFactoryPrefabs = new List<GameObject>();

    public BuildingProject factoryProject;
    public BuildingProject hangarProject;
    public BuildingProject garageProject;

    public GameObject currentConstructionSite; //AI should never make more than one construction
                                               //This could be changed to a list if we change it

    public bool isCurrentlyBuilding;

    public List<GameObject> airUnitPrefabs = new List<GameObject>();
    public List<GameObject> groundUnitPrefabs = new List<GameObject>();
    public GameObject factoryPrefab;
    public GameObject mainBase;
    public GameObject dronePrefab;

    public int amountOfResources;
    public int resourceThreshold;

    private int numberOfUnits;
    public int maxUnits;
    public int unitThreshold;

    public AIState state;
    public List<Unit> availableUnits = new List<Unit>();
    public List<Building> availableBuildings = new List<Building>();
    public List<UnitFactoryBuilding> availableFactories = new List<UnitFactoryBuilding>();




    private void Start()
    {
        state = AIState.GATHER;
        isCurrentlyBuilding = false;
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemys)
        {
            Unit unit = enemy.GetComponent<Unit>();
            if (unit != null)
            {
                availableUnits.Add(unit);
            }
        }

        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
        foreach (GameObject buildingGO in buildings)
        {
            Building building = buildingGO.GetComponent<Building>();
            if (building != null)
            {
                if(building.owner == Owner.AI)
                availableBuildings.Add(building);
            }
        }

        StartCoroutine(Play());
    }

    private void MakeDecision()
    {
        switch (state)
        {
            case AIState.GATHER:
                state = GatherResources();
                break;
            case AIState.BUILDUP:
                state = BuildUp();
                break;
            case AIState.DEFEND:
                break;
            case AIState.ATTACK:
                break;
            default:
                break;
        }
    }

    private IEnumerator Play()
    {

        while(mainBase.activeInHierarchy)
        {
            MakeDecision();
            yield return new WaitForSeconds(Random.Range(0.4f, 5.5f));
        }

    }

    private AIState GatherResources()
    {
        Debug.Log("Gathering");
        if (amountOfResources >= resourceThreshold || availableUnits.Count <= 0) return AIState.BUILDUP;

        Unit scout = GetAvailableUnit();
        Debug.Log("Scout is: " + scout.unitName);
        GameObject[] resources = GameObject.FindGameObjectsWithTag("Resource");
        Debug.Log("Resources found: " + resources.Length.ToString());
        GameObject closestResource = null;
        foreach(GameObject resource in resources)
        {
            if (closestResource == null)
            {
                closestResource = resource;
            }
            else if(Vector3.Distance(closestResource.transform.position,scout.transform.position) > Vector3.Distance(resource.transform.position, scout.transform.position))
            {
                closestResource = resource;
            }
        }

        if (closestResource == null) return AIState.BUILDUP;

        Debug.Log("Closest Resource: " + closestResource.name);

        ResourceBehaviour resourceBehaviour = closestResource.GetComponent<ResourceBehaviour>();

        //Send unit to gather resources

        Target target = new Target(closestResource.transform.position, resourceBehaviour);
        scout.InteractWithTarget(target);

        return AIState.GATHER;
    }

    private AIState BuildUp()
    {
        Debug.Log("Building up");
        if (availableUnits.Count > unitThreshold) return AIState.ATTACK;
        if (amountOfResources < resourceThreshold && !(availableUnits.Count <= 0)) return AIState.GATHER;

        if(availableUnits.Count < 1)
        {
            if(availableBuildings.Count <= 1 && amountOfResources >= 50)
            {
                Debug.Log("Building more drones");
                MainBase baseScript = mainBase.GetComponent<MainBase>();
                baseScript.BuildDrone();
                amountOfResources -= 50;
            }
        }
        else if(currentConstructionSite != null)
        {
            Unit scout = GetAvailableUnit();
            InProgressBuilding inProgressBuilding = currentConstructionSite.GetComponent<InProgressBuilding>();
            Target target = new Target(inProgressBuilding);
            scout.InteractWithTarget(target);
        }
        else if(availableBuildings.Count <= 1 )
        {
            Debug.Log("Building factory");
            Vector3 buildingPosition = new Vector3(
                mainBase.transform.position.x + Random.Range(15, 30),
                mainBase.transform.position.y,
                mainBase.transform.position.z + Random.Range(-10, 10)
                );

            GameObject site = Instantiate(GameManager.Instance.constructionPrefab) as GameObject;
            site.transform.position = buildingPosition;
            site.SetActive(true);
            InProgressBuilding inProgressBuilding = site.GetComponent<InProgressBuilding>();
            inProgressBuilding.project = factoryProject;
            currentConstructionSite = site;

            //old lets change
            //GameObject factory = Instantiate(factoryPrefab) as GameObject;
            //factory.transform.position = buildingPosition;
            //factory.SetActive(true);

        }
        else if(availableFactories.Count < 2 && availableUnits.Count < maxUnits)
        {
            Vector3 buildingPosition = new Vector3(
               mainBase.transform.position.x + Random.Range(15, 60),
               mainBase.transform.position.y,
               mainBase.transform.position.z + Random.Range(-20, 20)
               );

            GameObject site = Instantiate(GameManager.Instance.constructionPrefab) as GameObject;
            site.transform.position = buildingPosition;
            site.SetActive(true);
            InProgressBuilding inProgressBuilding = site.GetComponent<InProgressBuilding>();
            inProgressBuilding.project = availableFactories.Count == 0 ? hangarProject : garageProject;
            currentConstructionSite = site;


            //GameObject factory = Instantiate(unitFactoryPrefabs[Random.Range(0,1)]) as GameObject;
            //factory.transform.position = buildingPosition;
            //factory.SetActive(true);
            //UnitFactoryBuilding factoryScript = factory.GetComponent<UnitFactoryBuilding>();
            //factoryScript.owner = Owner.AI;
            //availableFactories.Add(factoryScript);
            //availableBuildings.Add(factoryScript);
        }
        else
        {
            UnitFactoryBuilding unitFactory = availableFactories[Random.Range(0, availableFactories.Count)];
            GameObject prefab = dronePrefab; //default case
            switch(unitFactory.type)
            {
                case FactoryType.GARAGE:
                    prefab = groundUnitPrefabs[Random.Range(0, groundUnitPrefabs.Count)];
                    break;
                case FactoryType.HANGAR:
                    prefab = airUnitPrefabs[Random.Range(0, airUnitPrefabs.Count)];
                    break;
            }

            GameObject GO = Instantiate(prefab);
            GO.transform.position = new Vector3(
               mainBase.transform.position.x + Random.Range(15, 50),
               mainBase.transform.position.y,
               mainBase.transform.position.z + Random.Range(-20, 20)
               );
            Unit unit = GO.GetComponent<Unit>();
            GO.SetActive(true);
            availableUnits.Add(unit);
        }

        return AIState.BUILDUP;
    }

    private Unit GetAvailableUnit()
    {
        Unit unit = null;

        foreach(Unit availableUnit in availableUnits)
        {
            if(availableUnit.currentTarget == null)
            {
                unit = availableUnit;
                break;
            }
        }

        return unit;
    }
    private AIState Attack()
    {
        return AIState.GATHER;
    }

}
