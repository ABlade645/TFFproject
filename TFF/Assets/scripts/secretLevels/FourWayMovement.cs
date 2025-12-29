using UnityEngine;

public class FourWayMovement : MonoBehaviour
{
    public float moveInputX;
    public float moveInputY;
    public float movementSpeed;
    Rigidbody2D rb;
    public bool anim;

    private void Start()
    {
        anim = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInputX = Input.GetAxis("Horizontal");
        moveInputY = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(moveInputX * movementSpeed, moveInputY * movementSpeed);
    }
}
