using UnityEngine;
using UnityEngine.UI;

public class StyleUI : MonoBehaviour
{
    [Header("General")]
    public Image meter;
    public Text scoreboardTxt;
    public Text scoreboardModeTxt;
    public GameObject whatToDisable;
    [HideInInspector]
    public int styleState;

    [Header("Timers")]
    //timers-----------------------
    float maxTimeBtwSwitch = 0.01f;
    float timeBtwSwitch;

    public float maxStyleTime;
    float styleTime;

    public float fadeSpeed;
    //-----------------------------
    bool isAuto;

    SaveSystem save;

    void Start()
    {
        save = FindObjectOfType<SaveSystem>();
        scoreboardTxt.text = "";
        styleState = save.styleState;
    }

    void Update()
    {
        if (meter.fillAmount != styleTime)
        {
            meter.fillAmount = styleTime;
        }
        if (styleTime > 0)
        {
            styleTime -= Time.deltaTime * fadeSpeed;
        }

        //flip flop switch------------------------------------------------------------------
        if (timeBtwSwitch > 0)
        {
            timeBtwSwitch -= Time.deltaTime;
        }

        if (timeBtwSwitch <= 0)
        {
            if (styleState < 2 && Input.GetKeyDown(KeyCode.Tab))
            {
                styleState++;
                timeBtwSwitch = maxTimeBtwSwitch;
            }
            if (styleState == 2 && Input.GetKeyDown(KeyCode.Tab))
            {
                styleState = 0; 
                timeBtwSwitch = maxTimeBtwSwitch;
            }
        }

        switch (styleState)
        {
            case 0:
                whatToDisable.SetActive(false);
                isAuto = false;
                save.styleState = styleState;
                scoreboardModeTxt.text = "mode: disabled";
                break;
            case 1:
                whatToDisable.SetActive(true);
                isAuto = false;
                save.styleState = styleState;
                scoreboardModeTxt.text = "mode: enabled";
                break;
            case 2:
                whatToDisable.SetActive(true);
                isAuto = true;
                save.styleState = styleState;
                scoreboardModeTxt.text = "mode: auto";
                break;

            default:
                Debug.LogError("nonexistent style scoreboard mode");
                break;
        }
        //-----------------------------------------------------------------------------------


        //Auto mode---------------------------------------
        if (isAuto)
        {
            if (styleTime < 0)
            {
                whatToDisable.SetActive(false);
            }
        }
        //------------------------------------------------
    }

    public void InvokeStyleMeter(string stuntName)
    {
        styleTime = maxStyleTime;
        if (isAuto)
        {
            whatToDisable.SetActive(true);
        }
        scoreboardTxt.text += ("\n-" + stuntName);
    }
}
