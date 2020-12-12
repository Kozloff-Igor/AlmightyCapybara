using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour
{

	Rigidbody2D rb;


	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		rb.velocity = new Vector2(0f, 0f);
		transform.Rotate(0, 0, 0);
	}

	void reloadlevel() { Application.LoadLevel(Application.loadedLevel); }

	void FixedUpdate()
	{
		rb.velocity = new Vector2(Input.GetAxis("Vertical") * 5f, rb.velocity.y);
		rb.velocity = new Vector2(Input.GetAxis("Horizontal") * 5f, rb.velocity.x);

		if (Input.GetKey("e"))
		{
			transform.Rotate(0, 0, -1.0f);
		}
		if (Input.GetKey("q"))
		{
			transform.Rotate(0, 0, 1.0f);
		}
	}
}