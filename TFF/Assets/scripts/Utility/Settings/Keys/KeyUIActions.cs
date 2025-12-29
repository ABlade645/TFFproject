using UnityEngine;

public class KeyUIActions : MonoBehaviour
{
    public KeyInput[] keyScripts;
    public KeyCode[] keys;
    bool startCheck;
    bool beginCancel;
    SettingsSave save;

    private void Start()
    {
        keys = new KeyCode[keyScripts.Length];
        startCheck = true;
        beginCancel = false;
        save = FindObjectOfType<SettingsSave>();
    }

    private void Update()
    {
        if (keys[keys.Length - 1] != keyScripts[keyScripts.Length - 1].key && startCheck)
        {
            for (int i = 0; i < keys.Length; i++) 
            {
                keys[i] = keyScripts[i].key;
            }
        }
        else
        {
            startCheck = false;
        }

        if (beginCancel)
        {
            for(int i = 0;i < keys.Length; i++)
            {
                keyScripts[i].key = keys[i];
                keyScripts[i].text.text = keys[i].ToString();

                if (i == keys.Length - 1)
                {
                    beginCancel = false;
                }
            }
        }
    }

    public void Cancel()
    {
        beginCancel = true;
    }

    public void Apply()
    {
        save.ApplySettings();
    }
}
