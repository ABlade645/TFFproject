using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class SaveSystem : MonoBehaviour
{
    public int playerPowerPoints;
    public int styleState;
    public int playerEndlessHighScore;
    public bool[] levelsUnlocked;
    public int currentSlotIndex; // 3 indexes representing the saveSlots

    PowerPointsSystem points;

    [HideInInspector]
    public SaveSlotIndexer indexer;
    [HideInInspector]
    public SavingModel model;

    string indexJson;
    string saveJson;

    bool checkUp = true;

    public bool loadOnAwake;

    void Start()
    {
        if (loadOnAwake)
        {
            LoadData();
        }
        points = GetComponentInChildren<PowerPointsSystem>();
    }

    void Awake()
    {
        if (loadOnAwake)
        {
            LoadData();
        }
    }

    void Update()
    {
        if (checkUp)
        {
            if (!Directory.Exists(Application.persistentDataPath + "/Saves"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/Saves");
            }

            for (int i = 0; i < 3; i++)
            {
                if (!File.Exists(Application.persistentDataPath + "/Saves/Slot" + (i + 1) + ".json"))
                {
                    File.Create(Application.persistentDataPath + "/Saves/Slot" + (i + 1) + ".json");                    
                    if (i == 3)
                    {
                        checkUp = false;
                    }
                }

                if (File.Exists(Application.persistentDataPath + "/Saves/Slot" + (i + 1) + ".json") && i == 3)
                {
                    checkUp = false;
                }
            }

            if (!File.Exists(Application.persistentDataPath + "/Saves/Data/Indexer.json"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/Saves/Data");
                File.Create(Application.persistentDataPath + "/Saves/Data/Indexer.json");
            }            
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadIndex();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            SaveIndex();
        }
    }

    //Save player data------------------------------------------------------------
    public void SaveData()
    {
        model = new SavingModel();
        model.powerPoints = playerPowerPoints;
        model.endlessWaveHighScore = playerEndlessHighScore;
        model.levels = levelsUnlocked;
        model.styleMeterState = styleState;

        saveJson = JsonUtility.ToJson(model);
        if (Directory.Exists(Application.persistentDataPath + "/Saves"))
        {
            if (File.Exists(Application.persistentDataPath + "/Saves/Slot" + currentSlotIndex + ".json"))
            {
                WriteIn();
            }
            else
            {
                File.Create(Application.persistentDataPath + "/Saves/Slot" + currentSlotIndex + ".json");
                Invoke("WriteIn", 0.5f);
            }            
        }
        else
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Saves");
            Invoke("WriteInSaveDirectory", 0.5f);
        }        
    }
    //Load player data-----------------------------------------------------------------------
    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/Saves/Slot" + currentSlotIndex + ".json")) 
        {
            model = JsonUtility.FromJson<SavingModel>(File.ReadAllText(Application.persistentDataPath + "/Saves/Slot" + currentSlotIndex + ".json"));
        }
        else
        {
            Debug.Log("Load error: file does not exist");
        }

        playerPowerPoints = model.powerPoints;
        playerEndlessHighScore = model.endlessWaveHighScore;
        levelsUnlocked = model.levels;

        Debug.Log("Load results:");
        Debug.Log("Points: " + model.powerPoints);
        Debug.Log("Endless highscore: " + model.endlessWaveHighScore);
        Debug.Log("Unlocked levels: " + model.levels);
    }
    //Save slot index-------------------------------------------------------------------------
    public void SaveIndex()
    {
        indexer = new SaveSlotIndexer();
        indexer.slotIndex = currentSlotIndex;

        indexJson = JsonUtility.ToJson(indexer);
        if (Directory.Exists(Application.persistentDataPath + "/Saves/Data"))
        {
            if (File.Exists(Application.persistentDataPath + "/Saves/Data/Indexer.json"))
            {
                File.WriteAllText(Application.persistentDataPath + "/Saves/Data/Indexer.json", indexJson);
                Debug.Log("Saving slot index at: " + Application.persistentDataPath + "/Saves/Data");
            }
            else
            {
                File.Create(Application.persistentDataPath + "/Saves/Data/Indexer.json");
                Invoke("WriteIndex", 0.5f);
            }
        }
        else
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Saves/Data");
            Invoke("WriteInDirectory", 0.5f);
        }
        StartCoroutine("SaveCoroutine");       
    }
    //Load slot index-------------------------------------------------------------------------
    public void LoadIndex()
    {
        indexer = JsonUtility.FromJson<SaveSlotIndexer>(File.ReadAllText(Application.persistentDataPath + "/Saves/Data/Indexer.json"));
        indexer.slotIndex = currentSlotIndex;

        Debug.Log(indexer.slotIndex);
        LoadData();
    }

    //Writing in--------------------------------------------------------------------------------
       //Save Slots-----------------------------------------------------------------------------
    public void WriteIn()
    {
        File.WriteAllText(Application.persistentDataPath + "/Saves/Slot" + currentSlotIndex + ".json", saveJson);
        Debug.Log("Saving slot" + currentSlotIndex + " at: " + Application.persistentDataPath + "/Saves");
    }

    public void WriteInSaveDirectory()
    {
        if (File.Exists(Application.persistentDataPath + "/Saves"))
        {
            WriteIn();
        }
        else
        {
            File.Create(Application.persistentDataPath + "/Saves");
            Invoke("WriteIn", 0.5f);
        }
    }

      //Index-----------------------------------------------------------------------------------
    public void WriteIndex()
    {
        File.WriteAllText(Application.persistentDataPath + "/Saves/Data/Indexer.json", indexJson);
        Debug.Log("Saving slot index at: " + Application.persistentDataPath + "/Saves/Data");
    }

    public void WriteInDirectory()
    {
        if (File.Exists(Application.persistentDataPath + "/Saves/Data/Indexer.json"))
        {
            WriteIndex();
        }
        else
        {
            File.Create(Application.persistentDataPath + "/Saves/Data/Indexer.json");
            Invoke("WriteIndex", 0.5f);
        }
    }

    IEnumerator SaveCoroutine()
    {
        yield return new WaitForEndOfFrame();
        points.Save();
        yield return new WaitForSeconds(0.1f);
        SaveData();       
    }
}

[Serializable]
public class SavingModel
{
    public int powerPoints;
    public int endlessWaveHighScore;
    public bool[] levels;
    public int styleMeterState;
}

[Serializable]
public class SaveSlotIndexer
{
    public int slotIndex;
}