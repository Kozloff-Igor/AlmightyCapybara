using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour
{

    Rigidbody2D rb;
    Transform tr;

    GrowInNumbers growInNumbers;
    Hypnosis hypnosis;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        growInNumbers = GetComponent<GrowInNumbers>();
        tr = transform;
        hypnosis = GetComponent<Hypnosis>();
    }

    void Update()
    {
        rb.velocity = new Vector2(0f, 0f);
        transform.Rotate(0, 0, 0);


        if (Input.GetKeyDown(KeyCode.X))
        {
            var newMouse = hypnosis.Hypnotize();
            if (newMouse)
            {
                growInNumbers.AddNewMouse(newMouse.transform);
            }
            //Transform randMouse = growInNumbers.RandomFreeMouse();
            //if (randMouse)
            //{
            //    growInNumbers.AddNewMouse(randMouse);
            //}
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (growInNumbers.activeMouses.Count > 1)
            {
                growInNumbers.RemoveMouse(growInNumbers.activeMouses[Random.Range(1, growInNumbers.activeMouses.Count)]);
            }
        }

    }

    void reloadlevel() { Application.LoadLevel(Application.loadedLevel); }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Vertical") * 5f, rb.velocity.y);
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * 5f, rb.velocity.x);

        // tr.Translate(new Vector2(Input.GetAxis("Vertical") * 5f, Input.GetAxis("Horizontal") * 5f) * Time.deltaTime);

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, 0, -90.0f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, 0, 90.0f * Time.deltaTime);
        }
    }
}