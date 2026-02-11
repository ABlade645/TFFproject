using UnityEngine;

public class DaHandBehavour : MonoBehaviour
{
    Vector2 target;

    public float lookAtOffset;
    public float speedMultiplier;
    public float triggerDistance;
    public bool isFollowing = true;

    bool hasSpawned;

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

            transform.position += vec.magnitude * speedMultiplier * vec.normalized;

            if (triggerDistance > vec.magnitude)
                gameObject.SetActive(false);
        }
    }

    void Setup()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
    }    
}
