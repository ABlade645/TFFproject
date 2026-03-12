using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;


public class CodeTerminal : MonoBehaviour
{ 
    public Text Code;
    char[] numbers = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
    public string answer;

    bool canType;
    bool canInterract;

    public GameObject Panel;
    public GameObject InterractTxt;

    public void One()
    {
        if (canType)
            Code.text += numbers[0];
    }

    public void Two()
    {
        if (canType)
            Code.text += numbers[1];
    }

    public void Three()
    {
        if (canType)
            Code.text += numbers[2];
    }

    public void Four()
    {
        if (canType)
            Code.text += numbers[3];
    }

    public void Five()
    {
        if (canType)
            Code.text += numbers[4];
    }

    public void Six()
    {
        if (canType)
            Code.text += numbers[5];
    }

    public void Seven()
    {
        if (canType)
            Code.text += numbers[6];
    }

    public void Eight()
    {
        if (canType)
            Code.text += numbers[7];
    }

    public void Nine()
    {
        if (canType)
            Code.text += numbers[8];
    }

    public void Zero()
    {
        if (canType)
            Code.text += numbers[9];
    }

    public void Remove()
    {
        Code.text = "";
    }

    public void Enter()
    {
        if (Code.text == answer)
        {
            System.Diagnostics.Process.Start(Path.Combine(Application.dataPath, "StreamingAssets\\A_Message\\A_Message.exe"));
            SceneManager.LoadScene(0);
        }
           
    }

    void Close()
    {
        Code.text = "";
        Panel.SetActive(false);
    }

    private void Update()
    {
        if (Code.text.Length > 4 && canType)
            canType = false;

        if (Code.text.Length < 5 && !canType)
            canType = true;


        if (Input.GetKeyDown(KeyCode.Q) && canInterract && !Panel.activeSelf)
            Panel.SetActive(true);
        else if (Input.GetKeyDown(KeyCode.Q) && canInterract && Panel.activeSelf)
            Close();

        if (Panel.activeSelf && !canInterract)
            Close();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canInterract = true;
            InterractTxt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canInterract = false;
            InterractTxt.SetActive(false);
        }
    }
}
