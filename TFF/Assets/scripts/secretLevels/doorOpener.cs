using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Playables;

public class doorOpener : MonoBehaviour
{
    [Header("General")]
    public PlayableDirector timeline;
    public ParticleSystem particle;
    public Light2D[] lightArr; 
    public Transform threshold;
    public float retSpeed;
    public float shakeForce;
    [Header("Sound")]
    public AudioSource sound;
    public float leftPitchborder;
    public float rightPitchborder;
    [Header("Other")]
    public GameObject info;
    public float lightFlickDif;
    bool caninterract;
    bool used;
    Vector3 thresholdStartPos;
    float bufferLight;

    HandEvent hand;

    void Start()
    {
        thresholdStartPos = threshold.localPosition;
        bufferLight = lightArr[0].intensity;
        hand = GameObject.Find("HandBorder").GetComponent<HandEvent>();
    }

    public void Trigger()
    {
        particle.Play();
        sound.pitch = Random.Range(leftPitchborder, rightPitchborder);
        sound.Play();
        threshold.localPosition += new Vector3(Random.Range(-1, 1) + Random.Range(-1, 1), Random.Range(-1, 1) + Random.Range(-1, 1), threshold.localPosition.z) * shakeForce;
        for (int i = 0; i < lightArr.Length; i++)
            if(lightArr[i].gameObject.activeSelf)
                lightArr[i].intensity = Random.Range((int)(lightFlickDif * 10), (int)((bufferLight + 0.1) * 10))/10.0f;
    }

    void Update()
    {
        if(caninterract && Input.GetKeyDown(KeyCode.Q) && !used)
        {
            used = true;
            info.SetActive(false);
            timeline.Play();
        }

        if (threshold.localPosition != thresholdStartPos)
            threshold.localPosition = Vector2.MoveTowards(threshold.localPosition, thresholdStartPos, retSpeed);

        if (used && timeline.state != PlayState.Playing && !hand.canTrigger)
            hand.canTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !used)
        {
            caninterract = true;
            info.SetActive(true);
        }         
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !used)
        {
            caninterract = true;
            info.SetActive(false);
        }
    }
}
