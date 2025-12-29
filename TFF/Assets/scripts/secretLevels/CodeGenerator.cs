using UnityEngine;
using UnityEngine.UI;

public class CodeGenerator : MonoBehaviour
{
    public int code;
    public GameObject[] writeTo;

    CodeTerminal script;

    private void Start()
    {
        script = GetComponent<CodeTerminal>();
        Generate();
    }

    void Generate()
    { 
        code = Random.Range(10000, 99999);
        script.answer = code.ToString();
        int numValue = 10000;
        for (int i = 0; i < 5; i++)
        {
            writeTo[i].SetActive(true);
            writeTo[i].GetComponent<Text>().text += code / numValue % 10;
            numValue /= 10;
            writeTo[i].SetActive(false);
        }
        
    }
}
