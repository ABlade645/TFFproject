using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecretLevelEntryCheck : MonoBehaviour
{
    string textVal;
    int number;
    public string[] sceneNames; 

    void Start()
    {
        textVal = File.ReadAllText(Application.dataPath + "\\CoreExe\\CoreTxt.txt"); 

        if (int.TryParse(textVal, out number))
            int.TryParse(textVal, out number);
        else
            File.WriteAllText(Application.dataPath + "\\CoreExe\\CoreTxt.txt", "0");

        switch (number) 
        {
            case 1:
                RepeatingAction();
                SceneManager.LoadScene(sceneNames[number - 1]);              
                break;

            default:
                RepeatingAction();
                break;
        }
    }

    void RepeatingAction()
    {
        FileInfo fileInfo = new FileInfo(Application.dataPath + "\\CoreExe\\CoreTxt.txt");
        fileInfo.Attributes = FileAttributes.Normal;
        File.WriteAllText(Application.dataPath + "\\CoreExe\\CoreTxt.txt", "0");
        fileInfo.Attributes = FileAttributes.Hidden;
        number = 0;
    }
}
