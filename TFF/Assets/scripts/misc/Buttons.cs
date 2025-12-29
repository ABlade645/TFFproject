using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using Cinemachine;

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
    public GameObject player;
    public GameObject camera;
    public GameObject cinemachine;
    [HideInInspector]
    public bool canFollow;
    public float speed;
    public float stoppingDist;
    public bool cameraToggle;
    public float maxCDtime;
    float CDtime;

    SaveSystem save;
    levelSelectToMenu returnSc;
    playercontroller controller;
    GameObject playerSoundManager;
    bool canCacheObjects = true;
    bool beginCaching;

    void Start()
    {
        state = 1;
        canFollow = false;
        save = FindObjectOfType<SaveSystem>();
        returnSc = FindObjectOfType<levelSelectToMenu>();
        layer = 0;
        controller = FindObjectOfType<playercontroller>();
        controller.canMove = false;
        playerSoundManager = GameObject.Find("SoundManager");
        TransitionNames = new string[Transitions.Length];
    }

    void Update()
    {
        if (CDtime > 0)
        {
            CDtime -= Time.deltaTime;
        }

        if (canCacheObjects && TransitionDict.Count != Transitions.Length)
        {
            for (int i = 0; i < TransitionNames.Length; i++)
            {
                if (TransitionNames[i] != Transitions[i].name)
                {
                    TransitionNames[i] = Transitions[i].name;
                }

                if (i == TransitionNames.Length - 1)
                {
                    beginCaching = true;
                }
            }
            if (beginCaching)
            {
                AddToCache();
            }

        }
        else
        {
            canCacheObjects = false;
        }


        if (canFollow == true && camera.transform.position != new Vector3(player.transform.position.x, player.transform.position.y, -10) && cameraToggle == true)
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -10), speed * Time.deltaTime);
            float sqrDist = (player.transform.position - camera.transform.position).sqrMagnitude;
            if (sqrDist <= stoppingDist * stoppingDist)
            {
                canFollow = false;
                cinemachine.GetComponent<CinemachineBrain>().enabled = true;
            }
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

    public void AddToCache()
    {
        for (int i = 0; i < Transitions.Length; i++)
        {
            if (!TransitionDict.ContainsKey(TransitionNames[i]))
            {
                TransitionDict[TransitionNames[i]] = Transitions[i];
            }
            if (i == Transitions.Length - 1)
            {
                beginCaching = false;
            }
        }
    }

    public PlayableDirector GetFromCache(string key)
    {
        if (TransitionDict.ContainsKey(key))
        {
            return TransitionDict[key];
        }
        else
        {
            Debug.LogError("Transition error: Key \"" + key + "\" not found");
            return null;
        }
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

            if (cameraToggle)
            {
                canFollow = false;
            }
            layer--;
        }   
    }

    public void Exit()
    {
        Application.Quit();
    }

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

        if (cameraToggle)
        {
            canFollow = true;
        }
        returnSc.canReturn = true;
        layer++;
        controller.canMove = true;
        playerSoundManager.SetActive(true);
    }
}
