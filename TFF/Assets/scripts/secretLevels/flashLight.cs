using UnityEngine;

public class flashLight : MonoBehaviour
{
    public bool hasFlash;
    public bool active;

    public AudioSource sound;

    public GameObject flash;

    public float maxCD;
    float CD;

    // Update is called once per frame
    void Update()
    {

        if (hasFlash == true)
        {
            if (CD > 0)
            {
                CD -= Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.F) && CD <= 0 && active == false)
            {                
                flash.SetActive(true);
                sound.Play();
                active = true;
                CD = maxCD;          
            }

            if (Input.GetKeyDown(KeyCode.F) && CD <= 0 && active == true)
            {              
                flash.SetActive(false);
                active = false;
                CD = maxCD;               
            }
        }
    }
}
