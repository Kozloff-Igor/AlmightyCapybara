using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour
{
    Vector3 littleOffset = new Vector3(0.01f, 0.011f, 0);
    Rigidbody2D rb;
    Transform tr;
    Vector3 direction;
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
        if (growInNumbers.activeMouses.Count == 1)
        {
            if (direction != Vector3.zero)
            {
                transform.up = Vector3.Lerp(transform.up, direction + littleOffset, Time.deltaTime * 10f);
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            hypnosis.Hypnotize();
            //Transform randMouse = growInNumbers.RandomFreeMouse();
            //if (randMouse)
            //{
            //    growInNumbers.AddNewMouse(randMouse);
            //}
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (growInNumbers.activeMouses.Count > 1)
            {
                growInNumbers.RemoveMouse(growInNumbers.activeMouses[Random.Range(1, growInNumbers.activeMouses.Count)]);
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            FinishGame();
        }
           
#endif
        if (gameIsFinished)
        {
            CutSceneFinal();
        }
    }

    bool gameIsFinished = false;
    public void FinishGame()
    {
        foreach (Transform mouse in growInNumbers.activeMouses)
        {
            mouse.GetComponent<Collider2D>().enabled = false;
        }
        gameIsFinished = true;
    }

    void CutSceneFinal()
    {
        transform.localScale += Vector3.one * Time.deltaTime * 5f;
        transform.Rotate(0f, 0f, 180f * Time.deltaTime);
    }

    void reloadlevel() { Application.LoadLevel(Application.loadedLevel); }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Vertical") * 5f, rb.velocity.y);
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * 5f, rb.velocity.x);
        direction = rb.velocity.normalized;
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