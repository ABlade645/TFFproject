using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public class WorldShatter : MonoBehaviour, IDamagable
{
    [Header("General")]
    public float health;
    public Sprite[] spritePool;
    public float[] healthChangeAmount;
    public float maxTimeBtwAttack;
    public GameObject[] objectsToDisable;
    public GameObject shatterPrefab;
    public GameObject worldPrefab;
    public ParticleSystem particle;
    public ParticleSystem particleBurst;

    [Header("Camera")]
    public float cameraOffset;
    public float cameraSpeed;
    public TextAnim text;
    public TextAnim textS;

    float timeBtwAttack;

    SpriteRenderer sprite;
    Animator lightAnim;
    bool coroutineStarted;
    bool disableObjects;
    bool moveCamera;
    bool canSkip;
    Camera cam;
    GameObject player;
    GameObject cinemachine;
    float yCamPos;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = spritePool[0];
        lightAnim = GetComponent<Animator>();
        coroutineStarted = false;
        disableObjects = false;
        cam = FindObjectOfType<Camera>();
        cinemachine = GameObject.Find("CM vcam1");
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    void Update()
    {
        if (timeBtwAttack > 0)      
            timeBtwAttack -= Time.deltaTime;       

        if (health == healthChangeAmount[0] && sprite.sprite != spritePool[0])
        {
            sprite.sprite = spritePool[0];
            lightAnim.CrossFade("WorldShatter_F", 0);
        }

        if (health == healthChangeAmount[1] && sprite.sprite != spritePool[1])
        {
            sprite.sprite = spritePool[1];
            lightAnim.CrossFade("WorldShatter_S", 0);
        }

        if (health == healthChangeAmount[2] && sprite.sprite != spritePool[2])
        {
            sprite.sprite = spritePool[2];
            lightAnim.CrossFade("WorldShatter_T", 0);
        }

        if (health <= 0 && objectsToDisable[objectsToDisable.Length - 1].activeSelf)
        {
            if (disableObjects)            
                for (int i = 0; i < objectsToDisable.Length; i++)             
                    objectsToDisable[i].SetActive(false);                
            
            if (!coroutineStarted)
            {
                coroutineStarted = true;
                StartCoroutine("ShatterCoroutine");
            }
        }

        if (cam.transform.position.y < yCamPos + cameraOffset && moveCamera)      
            cam.transform.position += Vector3.up * cameraSpeed * Time.deltaTime;       

        if (canSkip && Input.GetKeyUp(KeyCode.N))     
            SceneManager.LoadScene("Menu");
        
    }

    IEnumerator ShatterCoroutine()
    {
        yield return new WaitForEndOfFrame();        
        Instantiate(shatterPrefab, new Vector2(transform.position.x, cam.transform.position.y), Quaternion.identity);
        GetComponent<SpriteRenderer>().sprite = null;
        GetComponent<Light2D>().enabled = false;
        GetComponent<Animator>().enabled = false;
        player.GetComponent<playercontroller>().enabled = false;
        player.GetComponent<PlayerAnimation>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        cinemachine.SetActive(false);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1;
        Destroy(GameObject.Find("TotalWorldShatter(Clone)"));
        Instantiate(worldPrefab, new Vector2(player.transform.position.x, player.transform.position.y - 6.5f), Quaternion.identity);
        disableObjects = true;
        particle.Play();
        yield return new WaitForSeconds(5);
        yCamPos = cam.transform.position.y;
        moveCamera = true;       
        yield return new WaitForSeconds(3);
        text.startCoroutine();
        yield return new WaitForSeconds(1);
        textS.startCoroutine();
        canSkip = true;
    }

    public void TakeDamagePhysical(float damage)
    {
        if (timeBtwAttack <= 0)
        {
            health -= damage;
            timeBtwAttack = maxTimeBtwAttack;
            particleBurst.Play();
        }       
    }

    public void TakeDamageRanged(float damage)
    {

    }

    public void TakeDamagePit(float damage)
    {
        health -= damage;
    }
}
