using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class TheCatInvoker : MonoBehaviour
{
    public Image imageToSave;
    public PlayableDirector timeline;
    public string imgName = "TheCat.png";
    public GameObject objectsToActivate;
    public GameObject objectsToDeactivate;
    string defaultHash = "8f9ff3a24a311be49b5862b0149a5097"; 
    public string hash = "0";
    public bool OhNo = false;

    public void SaveSpriteImage()
    {
        Sprite sprite = imageToSave.sprite;
        Texture2D sourceTexture = sprite.texture;

        Rect rect = sprite.textureRect;
        Texture2D newTex = new Texture2D((int)rect.width, (int)rect.height);

        Color[] pixels = sourceTexture.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
        newTex.SetPixels(pixels);
        newTex.Apply();

        byte[] bytes = newTex.EncodeToPNG();
        string fullPath = Path.Combine(Application.dataPath, imgName);
        File.WriteAllBytes(fullPath, bytes);

        Destroy(newTex);
    }


    //-----------------HASH CHECK----------------------
    public static string GetImageHash(string filePath)
    {
        if (!File.Exists(filePath)) 
        {
            return null;
        }

        try
        {
            using (var md5 = MD5.Create())
            using (var stream = File.OpenRead(filePath))
            {
                byte[] bytes = md5.ComputeHash(stream);
                return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error computing hash at: " + filePath);
            return null;
        }
    }

    private void Start()
    {
        hash = GetImageHash(Path.Combine(Application.dataPath, imgName));
    }

    private void Update()
    {
        if ((hash != defaultHash || !File.Exists(Path.Combine(Application.dataPath, imgName))) && !OhNo && hash != "0")
        {
            OhNo = true;
            StartCoroutine("DoomCoroutine");
            Debug.LogAssertion("OhNo: Invalid hash or unexisting file");
        }

        if (OhNo)
        {
            SaveSpriteImage();
            OhNo = false;
        }
    }

    IEnumerator DoomCoroutine()
    {
        objectsToDeactivate.SetActive(false);      

        yield return new WaitForSecondsRealtime(3);
        objectsToActivate.SetActive(true);
        timeline.Play();
        yield return new WaitForSecondsRealtime((float)timeline.duration);
        Application.Quit();
        Debug.Log("Application Quit");
    }
}
