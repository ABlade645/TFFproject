using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class Buttons : MonoBehaviour
{
    [Header("Layers")]
    public int state;
    public int layer;
    public int sLayer;

    [Header("Transitions")]
    Dictionary<string, PlayableDirector> TransitionDict = new Dictionary<string, PlayableDirector>();
    public PlayableDirector[] Transitions;
    string[] TransitionNames;

    [Header("Scene reference")]
    public string start;
    public string sandbox;

    [Header("General")]
    public Vector2[] poses;
    public LevelSelect[] levels;
    //Selection roll
    [HideInInspector]
    public Vector2 currentPos;
    [HideInInspector]
    public int currentIndex;

    public GameObject camera;
    public float speed;
    public bool cameraToggle;
    public float maxCDtime;
    float CDtime;
    GameObject selUI;

    SaveSystem save;
    levelSelectToMenu returnSc;
    bool canCacheObjects = true;

    void Start()
    {
        state = 1;
        save = FindObjectOfType<SaveSystem>();
        returnSc = FindObjectOfType<levelSelectToMenu>();
        layer = 0;
        TransitionNames = new string[Transitions.Length];

        currentPos = poses[0];
        currentIndex = 0;
        selUI = GameObject.Find("Selection UI");
        selUI.SetActive(false);
    }

    void Update()
    {
        if (CDtime > 0)      
            CDtime -= Time.deltaTime;
        

        if (canCacheObjects && TransitionDict.Count != Transitions.Length)
        {
            for (int i = 0; i < TransitionNames.Length; i++)
            {
                if (TransitionNames[i] != Transitions[i].name)                
                    TransitionNames[i] = Transitions[i].name;            
            }

            AddToCache();
        }
        else        
            canCacheObjects = false;
        

        if (camera.transform.position != new Vector3(currentPos.x, currentPos.y))
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, new Vector3(currentPos.x, currentPos.y, camera.transform.position.z), speed * Time.deltaTime);
            float sqrDist = (currentPos - (Vector2)camera.transform.position).sqrMagnitude;         
        }

        if (currentIndex >= 1)
        {
            if (Input.GetKeyDown(KeyCode.A) && currentIndex > 1)
            {
                currentPos = poses[currentIndex - 1];
                currentIndex--;
            }

            if (Input.GetKeyDown(KeyCode.D) && currentIndex < poses.Length - 1)
            {
                currentPos = poses[currentIndex + 1];
                currentIndex++;
            }

            if((Vector2)camera.transform.position == poses[1] && !selUI.activeSelf)
                selUI.SetActive(true);

            if(Input.GetKeyDown(KeyCode.Escape) && selUI.activeSelf)
                selUI.SetActive(false);

            for (int i = 0; i < levels.Length; i++)
            {
                if (i == currentIndex - 1)
                    if (i == levels.Length - 1)
                        break;
                    else
                        i++;

                levels[i].selected = false;
            }
        }

        switch (currentIndex)
        {
            case 1:
                levels[0].Up();

                break;

            case 2:
                levels[1].Up();
                break;

            case 3:
                levels[2].Up();
                break;
        }

        if (Input.GetKeyDown(KeyCode.Return))
            switch (currentIndex)
            {
                case 1:
                    SceneManager.LoadScene(1);
                    break;

                case 2:
                    EnterSandbox();
                    break;

                case 3:
                    SceneManager.LoadScene("Endless");
                    break;
            }

        switch (state) 
        {
            case 1:
                if (Input.GetKeyDown(KeyCode.Escape) && layer == 1 && returnSc.CDtime <= 0)
                {
                    PlayRev();
                    returnSc.CDtime = returnSc.maxCDtime;
                }
                break;

            case 2:
                if (Input.GetKeyDown(KeyCode.Escape) && sLayer != 0 && CDtime <= 0)
                {
                    switch (sLayer)
                    {
                        case 1:
                            settingsBack();
                            break;

                        case 2:
                            revControlls();
                            break;

                        case 3:
                            revSetKeys();
                            break;
                    }
                    
                    CDtime = maxCDtime;
                }
                break;
        }
    }

    //Cache timeline references----------------------------------
    public void AddToCache()
    {
        for (int i = 0; i < Transitions.Length; i++)
        {
            if (!TransitionDict.ContainsKey(TransitionNames[i]))
                TransitionDict[TransitionNames[i]] = Transitions[i];                     
        }
    }
    
    public PlayableDirector GetFromCache(string key)
    {
        if (TransitionDict.ContainsKey(key))       
            return TransitionDict[key];     
        else      
            Debug.LogError("Transition error: Key \"" + key + "\" not found");
            return null;       
    }

    public void Play()
    {
        GetFromCache("PlayAnim").Play();
        layer++;
        
        state = 1;
        CDtime = maxCDtime;
    }

    public void PlayRev()
    {
        if (CDtime <= 0)
        {
            CDtime = maxCDtime;
            GetFromCache("PlayAnimRev").Play();
            
            layer--;
            
        }   
    }

    public void Exit()
    {
        Application.Quit();
    }

    //Settings---------------------------------------
    public void Settings()
    {
        if (CDtime <= 0)
        {
            CDtime = maxCDtime;
            GetFromCache("SettingsAnim").Play();
            sLayer++;
            state = 2;
        }
    }

    public void settingsBack()
    {
        if (CDtime <= 0)
        {
            CDtime = maxCDtime;
            GetFromCache("SettingsAnimRev").Play();
            sLayer--;
        }
    }

    //Settings Keys -----------------------------------
    public void Controlls()
    {
        if (CDtime <= 0)
        {
            CDtime = maxCDtime;
            GetFromCache("ControllsAnim").Play();
            sLayer++;
        }
    }

    public void revControlls()
    {
        if (CDtime <= 0)
        {
            CDtime = maxCDtime;
            GetFromCache("ControllsAnimRev").Play();
            sLayer--;
        }
    }

    public void SetKeys()
    {
        if (CDtime <= 0)
        {
            CDtime = maxCDtime;
            GetFromCache("SetKeysAnim").Play();
            sLayer++;
        }
    }

    public void revSetKeys()
    {
        if (CDtime <= 0)
        {
            CDtime = maxCDtime;
            GetFromCache("SetKeysAnimRev").Play();
            sLayer--;
        }
    }

    //Campaign load------------------------------
    public void StartGame()
    {
        SceneManager.LoadScene(start);
    }

    public void EnterSandbox()
    {
        SceneManager.LoadScene(sandbox);
    }

    //Slots--------------------------------------------
    public void SlotF()
    {
        save.currentSlotIndex = 1;
        PlayTransition();
    }

    public void SlotS()
    {
        save.currentSlotIndex = 2;
        PlayTransition();
    }

    public void SlotT()
    {
        save.currentSlotIndex = 3;
        PlayTransition();
    }

    void PlayTransition()
    {
        GetFromCache("SlotSelected").Play();
        
        returnSc.canReturn = true;
        layer++;

        currentPos = poses[1];
        currentIndex = 1;
    }
}
