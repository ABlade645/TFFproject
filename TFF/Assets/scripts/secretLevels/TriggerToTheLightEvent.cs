using UnityEngine;

public class TriggerToTheLightEvent : MonoBehaviour
{
    public simpleDoor door;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("Player"))
            door.canOpen = true;
    }
}
