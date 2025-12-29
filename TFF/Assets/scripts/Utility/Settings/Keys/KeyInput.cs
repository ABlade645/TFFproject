using UnityEngine;
using UnityEngine.UI;

public class KeyInput : MonoBehaviour
{
    public KeyCode key;
    public KeyCode defaultKey;

    [HideInInspector]
    public Text text;
    bool canChangeKey;

    private void Start()
    {
        canChangeKey = false;
        text = GetComponentInChildren<Text>();
    }

    public void ChangeKey()
    {
        canChangeKey = true;
        text.fontSize = 80;
        text.text = "\"Press any\" key";
    }

    private void OnGUI()
    {
        if(canChangeKey)
        {
            Event ev = Event.current;

            if (ev.isKey && ev.type == EventType.KeyDown)
            {
                key = ev.keyCode;
                text.fontSize = 120;
                text.text = "\"" + ev.keyCode.ToString() + "\"";
                canChangeKey = false;
            }
        }
    }
}
