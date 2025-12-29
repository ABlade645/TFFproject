using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handTurn : MonoBehaviour
{
	bool facingRight;
	public float offset;
	public bool lookAtPlayer;
	public bool lookAtCursor;
	public bool lookAtObject;
	public GameObject Object;
	GameObject player;


	void Update()
	{
		if (player == null)
		{
            player = GameObject.FindGameObjectWithTag("Player");
        }

		if (lookAtObject)
		{
            Vector3 difference = Object.transform.position - transform.position;
            float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
        }

		if (lookAtCursor)
		{
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
        }

		if (lookAtPlayer)
		{
            Vector3 difference = player.transform.position - transform.position;
            float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
        }
    }

	public void Flip()
	{
		facingRight = !facingRight;
		Vector3 Scaler = transform.localScale;
		Scaler.x *= -1;
		transform.localScale = Scaler;
	}
}
