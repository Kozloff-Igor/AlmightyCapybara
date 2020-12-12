using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour
{
    public GrowInNumbers growInNumbers;
    public float speed = 4f;
    public float backSpeed = 3f;
    public float safeDistance = 3f;
    private Transform player;
    private int enemyColor;
    private bool needRotate;
    private int rotateDirection;
    private bool needBack;
    [SerializeField]
    private Transform check;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        growInNumbers.SetTargetMouseRotations();
    }

    // Update is called once per frame
    void Update()
    {
        if (needRotate)
        {
            transform.Rotate(0, 0, -90.0f * Time.deltaTime * rotateDirection);
        }

    }

    private void FixedUpdate()
    {
        if (player)
        {
            var heading = player.position - transform.position;
            var distance = heading.magnitude;
            if (distance < 10f)
            {
                var direction = heading / distance;
                needBack = distance < safeDistance;

                RaycastHit2D hit = Physics2D.Raycast(transform.position + direction * 2.5f, direction, 6f, LayerMask.GetMask("Mouse"));
                if (hit)
                {
                    if (hit.collider.GetComponentInParent<Run>())
                    {
                        var mouse = hit.collider.GetComponent<Mouse>();
                        //enemyColor = mouse.mouseColorId;
                        CheckMouses(mouse, direction);
                    }
                }
                else
                { //enemyColor = -1; 
                }
                if (needRotate && needBack)
                {
                    rb.velocity = -direction * backSpeed;
                }
                else
                {
                    rb.velocity = direction * speed;
                }
            }
            else
            {
                needRotate = false;
                rb.velocity = Vector2.zero;
            }
                
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Run>())
        {
            player = collision.transform.parent;
        }
    }

    void CheckMouses(Mouse enemyMouse, Vector3 direction)
    {
        var minDistance = 100f;
        Transform myMouse = null;
        foreach (var mouse in growInNumbers.activeMouses)
        {
            var dir = mouse.transform.TransformDirection(Vector3.up);

            var heading = direction - dir;
            var distance = heading.magnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                myMouse = mouse;
            }
        }
        var _needRotate = myMouse.GetComponent<Mouse>().mouseColorId == enemyMouse.mouseColorId;

        if (!needRotate && _needRotate)
        {
            var rd = Random.Range(0, 1);
            rotateDirection = rd == 0 ? -1 : 1;
        }

        needRotate = _needRotate;
    }
}
