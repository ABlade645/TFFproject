using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class SettingsSave : MonoBehaviour
{
    [Header("General")]
    public bool loadOnAwake;

    [Header("Keys")]
    public KeyInput[] keyScripts;
    public KeyCode[] keysToSave;
    public string[] keyNames;
    Dictionary<string, KeyCode> activeKeys = new Dictionary<string, KeyCode>();
    bool canTransferKeys;
    KeySave keySave;
    string saveKeysJson;

    bool checkUp;
    bool saveFileExists;
    bool beginKeyLoad;
    bool setToDefault;

    private void Start()
    {
        canTransferKeys = false;
        beginKeyLoad = false;
        checkUp = true;
        setToDefault = false;
        keySave = new KeySave();

        keysToSave = new KeyCode[keyScripts.Length];

        keySave.savedKeys = new KeyCode[keyScripts.Length];
        keySave.savedKeyNames = new string[keyScripts.Length];
    }

    void Awake()
    {
        if (loadOnAwake)
        {
            Invoke("LoadKeys", 0.1f);
        }
    }

    private void Update()
    {
        if (checkUp && keysToSave[keysToSave.Length - 1] != keySave.savedKeys[keyScripts.Length - 1])
        {
            if (!Directory.Exists(Application.persistentDataPath + "/Saves/Data"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/Saves/Data");
            }

            if (!File.Exists(Application.persistentDataPath + "/Saves/Data/SettingsSave.json"))
            {
                File.Create(Application.persistentDataPath + "/Saves/Data/SettingsSave.json");
                checkUp = false;
                saveFileExists = true;
                setToDefault = true;
            }
            else if (File.Exists(Application.persistentDataPath + "/Saves/Data/SettingsSave.json"))
            {
                checkUp = false;
                saveFileExists = true;
            }
        }
        else if (keyScripts[keyScripts.Length - 1] == null)
        {
            for (int i = 0; i < keySave.savedKeys.Length; i++)
            {
                keysToSave[i] = keySave.savedKeys[i];
            }
        }

        if (keyScripts[0] != null)
        {
            for (int i = 0; i < keysToSave.Length; i++)
            {
                keysToSave[i] = keyScripts[i].key;
            }
        }
        
        //--------------Key Transfer to Constructor-----------
        if (canTransferKeys && saveFileExists)
        {
            for (int i = 0; i < keysToSave.Length; i++)
            {
                SaveKeys(keyNames[i], keysToSave[i], i);
                if (i == keysToSave.Length - 1)
                {
                    saveKeysJson = JsonUtility.ToJson(keySave);
                    WriteIn();
                    canTransferKeys = false;
                }
            }
        }

        //--------------Set To Default-------------
        if (setToDefault)
        {
            for (int i = 0; i < keyScripts.Length; i++)
            {
                keysToSave[i] = keyScripts[i].defaultKey;
                keyNames[i] = keyScripts[i].defaultKey.ToString();


                keySave.savedKeys[i] = keyScripts[i].defaultKey;
                keySave.savedKeyNames[i] = keyScripts[i].defaultKey.ToString();
                if (i == keysToSave.Length - 1)
                {
                    setToDefault = false;
                    ApplySettings();
                }
            } 
        }


        //--------------Key Load Cycle-------------
        if (beginKeyLoad)
        {
            for (int i = 0; i < keyNames.Length; i++)
            {
                keysToSave[i] = keySave.savedKeys[i];
                
                if (!activeKeys.ContainsKey(keyNames[i]))
                {
                    activeKeys[keyNames[i]] = keySave.savedKeys[i];
                }

                if (keyScripts[0] != null)
                {
                    keyScripts[i].text.text = ("\"" + keySave.savedKeys[i].ToString() + "\"");
                }

                if (i == keyNames.Length - 1)
                {
                    Debug.Log("Keys loaded from save succesfully, keys loaded: " + activeKeys.Count);
                    beginKeyLoad = false;
                  
                }
            }
        }
    }

    public KeyCode GetKeyFromCache(string name)
    {
        if (!beginKeyLoad)
        {
            if (activeKeys.ContainsKey(name))
            {
                return activeKeys[name];
            }
            else
            {
                Debug.LogError("The key does not exist: key set to NONE by default");
                return KeyCode.None;
            }
        }
        else
        {
            return KeyCode.None;
        }
    }

    void SaveKeys(string name, KeyCode Key, int counter)
    {
        keySave.savedKeys[counter] = Key;
        keySave.savedKeyNames[counter] = name;
    }

    public void SetDefaultKeys()
    {
        setToDefault = true;
    }

    public void WriteIn()
    {
        File.WriteAllText(Application.persistentDataPath + "/Saves/Data/SettingsSave.json", saveKeysJson);
    }

    public void LoadKeys()
    {
        keySave = JsonUtility.FromJson<KeySave>(File.ReadAllText(Application.persistentDataPath + "/Saves/Data/SettingsSave.json"));
        Debug.Log("Settings Save: data load successful");
        beginKeyLoad = true;
    }

    public void ApplySettings()
    {
        canTransferKeys = true;
    }
}


//save class----------------------------------------------
[Serializable]
public class KeySave
{
    public KeyCode[] savedKeys;
    public string[] savedKeyNames;
}
