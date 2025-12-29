using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelMarking : MonoBehaviour
{
    public int sec = 0;
    public int min = 0;
    public int hour = 0;
    public TMP_Text _TimerText;
    [SerializeField] private int delta = 1;


    // Start is called before the first frame update
    void Start()
    {
        _TimerText = GameObject.Find("TimerText").GetComponent<TMP_Text>();
        StartCoroutine(ITimer());
    }

    IEnumerator ITimer()
    {
        while(true)
        {
            if (sec == 59)
            {
                sec = -1;
                min++;
            }

            if (min == 60)
            {
                min = 0;
                hour++;
            }

            sec += delta;
            _TimerText.text = hour.ToString("D2") + ":" + min.ToString("D2") + ":" + sec.ToString("D2");
            yield return new WaitForSeconds(1);
        }
    }
}
