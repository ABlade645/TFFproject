using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dash : MonoBehaviour
{
    public Image Bar;
    public float Stamina;
    public float staminaRegain;
    public float staminaUsage;
    float fill;

    public Rigidbody2D rb;

    public bool canDash;
    public bool isDashing;
    public float dashingPower;
    public float dashingTime;
    public float dashingCoolDown;

    public float maxVariantTime;
    public float variantTime;

    public TrailRenderer trail;
    public Gradient[] colorKey;

    public GameObject cursor;
    GameObject player;
    SettingsSave settings;

    void Start()
    {
        Stamina = 100;
        Bar.fillAmount = fill;
        player = GameObject.FindGameObjectWithTag("Player");
        settings = FindObjectOfType<SettingsSave>();
    }

    void Update()
    {
        fill = Stamina;
        Bar.fillAmount = fill / 100;

        if (player.GetComponent<playercontroller>().isGrounded == true && !canDash)                  
            canDash = true;                      

        if (Stamina >= staminaUsage)
        {
            if (Input.GetKeyDown(settings.GetKeyFromCache("Dash")) && canDash)
            {
                StartCoroutine(dash());               
                Stamina -= staminaUsage;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))            
                variantTime = maxVariantTime;           
        }            

        if (variantTime > 0)       
            variantTime -= Time.deltaTime;       

        if (Stamina < 100)      
            Stamina += staminaRegain * Time.deltaTime;       

        if (Stamina > 100)      
            Stamina = 100;      
    }
    
    IEnumerator dash()
    {
        trail.colorGradient = colorKey[0];
        canDash = false;
        isDashing = true;
        trail.emitting = true;
        float gravity = rb.gravityScale;
        rb.gravityScale = 0;

        if (variantTime > 0)
        {
            rb.velocity = new Vector2(cursor.transform.position.x - player.transform.position.x, cursor.transform.position.y - player.transform.position.y).normalized * dashingPower;
            trail.colorGradient = colorKey[1];
        }
        else
        {
            if (player.GetComponent<playercontroller>().moveInput > 0)           
                rb.velocity = new Vector2(1 * dashingPower, 0);
            
            if (player.GetComponent<playercontroller>().moveInput < 0)            
                rb.velocity = new Vector2(-1 * dashingPower, 0);           
        }
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = gravity;
        isDashing = false;
              
        yield return new WaitForSeconds(dashingCoolDown);
        trail.emitting = false;
    }
}
