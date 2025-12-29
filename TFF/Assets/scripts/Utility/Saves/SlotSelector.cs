using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SlotSelector : MonoBehaviour
{
    public int slotNum;
    public PlayableDirector timeline;
    GameObject saveSys;
    public Buttons buttons;
    Text text;

    void Start()
    {
        saveSys = GameObject.Find("SaveSystem");
        text = GetComponentInChildren<Text>();
    }

    public void SlotSelection()
    {
        SaveSystem save = saveSys.GetComponent<SaveSystem>();
        saveSys.GetComponent<SaveSystem>().currentSlotIndex = slotNum;
        if (saveSys.GetComponent<SaveSystem>().model != null)
        {
            saveSys.GetComponent<SaveSystem>().LoadData();
            text.text = ("Score: " + save.playerPowerPoints + " Endless score: " + save.playerEndlessHighScore);
        }
        else
        {           
            saveSys.GetComponent<SaveSystem>().SaveData();
            text.text = ("Score: " + save.playerPowerPoints + " Endless score: " + save.playerEndlessHighScore);
        }
        
        timeline.Play();
    }
}
