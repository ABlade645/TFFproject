using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Climbing : MonoBehaviour
{
    public Collider2D objectToClimb;
    public Transform ts;
    public float range;
    public LayerMask whatCanClimb;
    bool canLetGo;
    public GameObject climber;
    public GameObject hand1;
    public GameObject hand2;
    GameObject player;
    public bool isClimbing;

    public float swingForce;

    public float staminaUsage;

    public float maxRestTime;
    float restTime;

    private void Start()
    {
        canLetGo = false;
        climber.GetComponent<DistanceJoint2D>().enabled = false;
        restTime = 0;
    }

    private void Update()
    {
        if (restTime > 0)
        {
            restTime -= Time.deltaTime;
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (isClimbing && GetComponent<Dash>().Stamina >= 0.5f)
        {
            GetComponent<Dash>().Stamina -= staminaUsage;
            
            if (player.transform.position.x < Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized.x * swingForce, Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized.y * swingForce);
            }

            if (player.transform.position.x > Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized.x * -swingForce, Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized.y * swingForce);
            }
        }       

        if (Input.GetKeyDown(KeyCode.Mouse1) && restTime <= 0)
        {
            objectToClimb = Physics2D.OverlapCircle(ts.position, range, whatCanClimb);

            if (objectToClimb != null && objectToClimb.GetComponent<TilemapCollider2D>())
            {
                hand1.GetComponent<handTurn>().lookAtCursor = false;
                hand1.GetComponent<handTurn>().lookAtObject = true;
                hand1.GetComponent<handTurn>().Object = climber;
                hand2.GetComponent<handTurn>().lookAtCursor = false;
                hand2.GetComponent<handTurn>().lookAtObject = true;
                hand2.GetComponent<handTurn>().Object = climber;
                isClimbing = true;
                climber.transform.position = objectToClimb.ClosestPoint(transform.position);
                climber.GetComponent<DistanceJoint2D>().enabled = true;
                canLetGo = true;               
            }
        }   

        if (canLetGo == true && Input.GetKeyUp(KeyCode.Mouse1))
        {
            hand1.GetComponent<handTurn>().lookAtCursor = true;
            hand1.GetComponent<handTurn>().lookAtObject = false;
            hand1.GetComponent<handTurn>().Object = null;
            hand2.GetComponent<handTurn>().lookAtCursor = true;
            hand2.GetComponent<handTurn>().lookAtObject = false;
            hand2.GetComponent<handTurn>().Object = null;
            climber.GetComponent<DistanceJoint2D>().enabled = false;
            objectToClimb = null;
            canLetGo = false;
            isClimbing = false;
        }

        if (canLetGo == true && GetComponent<Dash>().Stamina < 0.5f)
        {
            hand1.GetComponent<handTurn>().lookAtCursor = true;
            hand1.GetComponent<handTurn>().lookAtObject = false;
            hand1.GetComponent<handTurn>().Object = null;
            hand2.GetComponent<handTurn>().lookAtCursor = true;
            hand2.GetComponent<handTurn>().lookAtObject = false;
            hand2.GetComponent<handTurn>().Object = null;
            climber.GetComponent<DistanceJoint2D>().enabled = false;
            objectToClimb = null;
            canLetGo = false;
            isClimbing = false;
            restTime = maxRestTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(ts.position, range);
        Gizmos.color = Color.green;
    }
}
