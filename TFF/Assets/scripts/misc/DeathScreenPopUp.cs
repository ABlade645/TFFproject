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
        if(canSlowMotion && Time.timeScale != 0)       
            Time.timeScale = 0; //Time.timeScale -= Time.unscaledDeltaTime * slowMotionSpeed;
        

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
        Time.timeScale = 1;
        SceneManager.LoadScene(toMenuString);        
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
    }
}
