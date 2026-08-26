using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecretLevelEntryCheck : MonoBehaviour
{
    public string textVal;
    int number;
    public string[] sceneNames; 

    void Start()
    {
        if (File.Exists(Application.dataPath + "\\CoreExe\\CoreTxt.txt"))
            textVal = File.ReadAllText(Application.dataPath + "\\CoreExe\\CoreTxt.txt");
        else
            RepeatingAction();

        if (int.TryParse(textVal, out number))
            int.TryParse(textVal, out number);
        else
            File.WriteAllText(Application.dataPath + "\\CoreExe\\CoreTxt.txt", "0");      
    }

    void Update()
    {
        switch (number)
        {
            case 0:

                break;

            case 1:
                RepeatingAction();
                SceneManager.LoadScene(sceneNames[0]);
                FindObjectOfType<FileCreator>().Check();
                break;

            default:
                RepeatingAction();
                break;
        }
    }

    void RepeatingAction()
    {
        if(File.Exists(Application.dataPath + "\\CoreExe\\CoreTxt.txt"))
        {
            FileInfo fileInfo = new FileInfo(Application.dataPath + "\\CoreExe\\CoreTxt.txt");
            fileInfo.Attributes = FileAttributes.Normal;
            File.WriteAllText(Application.dataPath + "\\CoreExe\\CoreTxt.txt", "0");
            fileInfo.Attributes = FileAttributes.Hidden;
            number = 0;
        }
        else
        {
            File.Create(Application.dataPath + "\\CoreExe\\CoreTxt.txt");
            RepeatingAction();
        }    
    }
}
