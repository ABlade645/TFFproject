using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEditor;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public static bool GameIsPaused = false;
    bool canBeResumed;

    Texture2D tex;
    public GameObject pauseObj;

    public string menu;

    public GameObject pauseUI;

    public PlayableDirector zoomOut;
    public PlayableDirector zoomIn;
    public PlayableDirector settings;
    public PlayableDirector settingsRev;
    public PlayableDirector audio;
    public PlayableDirector audioRev;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                StartCoroutine(CaptureScreenshotCoroutine());
            }
        }

        if (zoomIn.state != PlayState.Playing && canBeResumed)
        {
            pauseUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
            canBeResumed = false;
        }
    }

    public void Resume()
    {
        zoomIn.Play();
        canBeResumed = true;
    }

    void menuPause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        zoomOut.Play();
        pauseObj.transform.localScale = new Vector2(Screen.width, Screen.height);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(menu);
        Debug.Log("3");
        Time.timeScale = 1f;
    }

    public void Settings()
    {
        settings.Play();
    }

    public void SettingsBack()
    {
        settingsRev.Play();
    }

    public void Audio()
    {
        audio.Play();
    }

    public void AudioBack()
    {
        audioRev.Play();
    }

    IEnumerator CaptureScreenshotCoroutine()
    {
        yield return new WaitForEndOfFrame();

        int width = Screen.width;
        int height = Screen.height;

        tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
        tex.Apply();

        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        pauseObj.GetComponent<Image>().sprite = sprite;

        menuPause();
    }
}
