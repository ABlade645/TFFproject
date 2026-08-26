using UnityEngine;

public class DynamicMusic : MonoBehaviour
{
    public GameObject calm;
    public GameObject dynamic;
    public float speed;
    AudioSource calmVol;
    AudioSource dynamicVol;
    GameObject enemy;
    
    void Start()
    {
        calmVol = calm.GetComponent<AudioSource>();
        dynamicVol = dynamic.GetComponent<AudioSource>();
    }

    void Update()
    {
        if(enemy == null)
            enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (enemy == null && dynamicVol.volume > 0)
        {
            calmVol.volume += Time.deltaTime * speed;
            dynamicVol.volume -= Time.deltaTime * speed;
        }

        if (enemy != null && dynamicVol.volume < 1)
        {
            calmVol.volume -= Time.deltaTime * speed;
            dynamicVol.volume += Time.deltaTime * speed;
        }       
    }
}
