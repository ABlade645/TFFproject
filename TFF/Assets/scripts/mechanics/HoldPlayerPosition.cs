using UnityEngine;

public class HoldPlayerPosition : MonoBehaviour
{
    public Vector2 vector;

    public float maxInterval;
    float interval;
    playercontroller script;

    void Start()
    {
        script = GameObject.FindGameObjectWithTag("Player").GetComponent<playercontroller>();
        interval = maxInterval;
        vector = gameObject.transform.position;
    }

    void Update()
    {
        if (interval > 0)
            interval -= Time.deltaTime;

        if (interval <= 0 && script.isGrounded)
        {
            vector = gameObject.transform.position;
            interval = maxInterval;
        }
    }
}
