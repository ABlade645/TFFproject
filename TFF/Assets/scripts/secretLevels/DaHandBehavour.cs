using UnityEngine;

public class DaHandBehavour : MonoBehaviour
{
    Vector2 target;

    public float lookAtOffset;
    public float speedMultiplier;
    public float triggerDistance;
    public bool isFollowing = true;

    bool hasSpawned;
    keyDoor door;

    void Update()
    {
        if (!hasSpawned)
            Setup();

        if(target != null && isFollowing)
        {
            Vector3 difference = (target - (Vector2)transform.position).normalized;
            float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + lookAtOffset);

            Vector3 vec = target - (Vector2)transform.position;

            transform.position += speedMultiplier * vec.normalized * Time.deltaTime;

            if (triggerDistance > vec.magnitude)
            {
                door.hasKey = true;
                door.InfoC = Color.white;
                door.InfoS = "A card is needed";
                door.Info.SetActive(false);
                gameObject.SetActive(false);
            }             
        }
    }

    void Setup()
    {
        door = GameObject.Find("doorBlue").GetComponent<keyDoor>();
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
    }    
}
