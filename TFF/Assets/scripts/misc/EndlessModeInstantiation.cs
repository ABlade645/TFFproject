using Pathfinding;
using Pathfinding.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessModeInstantiation : MonoBehaviour
{
    [Header("General")]
    public GameObject[] instantiators;
    public int enemyCount;
    public float waitTime;
    public AstarPath pathfinding;
    public int numberOfWaves;
    public int islandNumber;

    [Header("Enemy Buffer")]
    public string[] enemies;

    [Header("Probabilities")]
    public int maxRange = 100;
    public int probabilityValue;
    public int slime;
    public int sneka;
    public int difficultyModif;

    [Header("UI")]
    public GameObject wCount;
    public GameObject eCount;

    [Header("Island Instantiation")]
    public GameObject[] islands;
    public GameObject boatPrefab;
    public float xOffset;

    [HideInInspector]
    public bool spawnNext;
    [HideInInspector]
    public bool intersection;

    bool canFind;
    bool canCount;      
    bool canSpawnIsland;
    int wave;
    GameObject[] maps;
    EndlessModeBoat boat;
    Transform boatInstantiationPoint;
    GameObject player;
    public GameObject[] islandBuffer;
    bool canDestroy;
    GameObject tracker;
    GameObject instantiationQueue;

    float spawnCD;
    float maxSpawnCD = 0.5f;

    EnemyCache cache;


    void Start()
    {
        wave = 0;
        islandNumber = 1;
        wCount.GetComponent<Text>().text = ("Wave: " + wave);
        eCount.GetComponent<Text>().text = ("Enemies left: " + enemyCount);
        player = GameObject.FindGameObjectWithTag("Player");
        canDestroy = true;
        tracker = GameObject.Find("Boat tracker");
        tracker.SetActive(false);
        instantiationQueue = GameObject.Find("InstantiationQueue");
        spawnCD = maxSpawnCD;

        cache = GameObject.FindGameObjectWithTag("EnemyCache").GetComponent<EnemyCache>();
    }

    private void Update()
    {
        // Island sorting
        if (maps != null && maps.Length > 0)
        {
            islandBuffer = new GameObject[maps.Length];

            int index = 0;
            foreach (var map in maps)
            {
                if (map != null && !System.Array.Exists(islandBuffer, island => island == map))
                {
                    islandBuffer[index] = map;
                    index++;
                }
            }
        }

        //pathfinder follow----------------------------------------------------------------
        if (intersection)
        {
            pathfinding.data.gridGraph.center = player.transform.position;
        }       

        eCount.GetComponent<Text>().text = ("Enemies left: " + enemyCount);

        if (canFind == true)
        {
            //wave starter------------------------------------------------------------------
            instantiators = GameObject.FindGameObjectsWithTag("EnemyInstantiatior");
            canFind = false;
            Invoke("StartWave", waitTime);
            pathfinding.Scan();
        }

        if (wave > 0)
        {
            if (wave / islandNumber == numberOfWaves)
            {
                if (intersection == false && enemyCount <= 0 && canSpawnIsland == false)
                {
                    canSpawnIsland = true;
                    intersection = true;
                    canDestroy = true;
                    NextIsland();
                    islandNumber++;
                    boat = GameObject.FindGameObjectWithTag("boat").GetComponent<EndlessModeBoat>();
                }

                if (boat != null)
                {
                    if (boat.isSailing == false)
                    {
                        if (tracker.activeSelf == false)
                        {
                            tracker.SetActive(true);
                            tracker.GetComponent<handTurn>().Object = boat.gameObject;
                        }
                    }                    
                }              
            }
        }

        if (tracker.activeSelf)
        {
            if (boat.isSailing)
            {
                tracker.SetActive(false);
            }
        }

        if (intersection == false)
        {
            if (spawnNext == true)
            {
                //instantiation-------------------------------------------------------------------
                canCount = true;
                sneka = maxRange;

                for (int i = 0; i < instantiators.Length; i++)
                {
                    probabilityValue = Random.Range(1, maxRange);
                    if (probabilityValue <= slime)
                    {
                        instantiators[i].GetComponent<enemyInstanciator>().enemyName = enemies[0];
                    }

                    if (probabilityValue > slime && probabilityValue <= sneka)
                    {
                        instantiators[i].GetComponent<enemyInstanciator>().enemyName = enemies[1];
                    }

                    if (i == instantiators.Length - 1)
                    {
                        //wave counter----------------------------------------------------------------
                        spawnNext = false;
                        wave++;
                        wCount.GetComponent<Text>().text = ("Wave: " + wave);
                        instantiationQueue.GetComponent<InstantiationQueue>().StartInstantiation();
                    }
                }
            }            
        }

        enemyCount = (GameObject.FindGameObjectsWithTag("Slime").Length + GameObject.FindGameObjectsWithTag("Sneka").Length);
        if (canCount == true)
        {
            if (spawnCD > 0)
            {
                spawnCD -= Time.deltaTime;
            }

            //enemy counter-----------------------------------------------------------------------------------         
            if (enemyCount <= 0 && spawnCD <= 0)
            {
                canCount = false;
                spawnCD = maxSpawnCD;
                Invoke("NextWave", waitTime);
            }
        }

        //island deletion------------------------------------------------------------------------------------------
        if (intersection && boat.isSailing && canDestroy)
        {
            
           
            Invoke("IslandDeletion", 5);
            canDestroy = false;
        }
    }

    //methods------------------------------------------------------------------------------------------
    public void StartCheckup()
    {
        canFind = true;
        spawnNext = false;
        intersection = false;
        maps = GameObject.FindGameObjectsWithTag("Island");
    }

    public void StartWave()
    {
        spawnNext = true;
        maps = GameObject.FindGameObjectsWithTag("Island");
    }

    void NextWave()
    {
        spawnNext = true;
        if (slime > 20 + difficultyModif)
        {
            slime -= difficultyModif;
        }
        pathfinding.Scan();
        maps = GameObject.FindGameObjectsWithTag("Island");
    }

    void NextIsland()
    {
        boatInstantiationPoint = GameObject.FindGameObjectsWithTag("boatPos")[GameObject.FindGameObjectsWithTag("boatPos").Length - 1].transform;
        if (canSpawnIsland && boatInstantiationPoint != null)
        {
            Instantiate(boatPrefab, boatInstantiationPoint.position, Quaternion.identity);
            if (GameObject.FindGameObjectWithTag("boat"))
            {
                int islandValue = Random.Range(0, islands.Length);
                Instantiate(islands[islandValue], maps[maps.Length - 1].transform.position + new Vector3(xOffset, maps[maps.Length - 1].transform.position.y), Quaternion.identity);

                canSpawnIsland = false;
            }         
        }
    }

    void IslandDeletion()
    {
        Destroy(islandBuffer[0].gameObject);
        islandBuffer = new GameObject[maps.Length];
    }
}
