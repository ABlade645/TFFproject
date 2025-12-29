using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class DeathScreenPopUp : MonoBehaviour
{
    public PlayableDirector timeline;
    public GameObject holder;
    public string toMenuString;

    public float slowMotionSpeed;
    bool canSlowMotion;

    void Update()
    {
        if(canSlowMotion && Time.timeScale > 0.05f)
        {
            Time.timeScale -= Time.unscaledDeltaTime * slowMotionSpeed;
        }

        if (Time.timeScale < 0.05f)
        {
            Time.timeScale = 0.05f;
            canSlowMotion = false;
        }
    }

    public void DeathScreen()
    {
        if (!holder.activeSelf)
        {
            holder.SetActive(true);
            timeline.Play();
            canSlowMotion = true;
        }
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(toMenuString);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
}
