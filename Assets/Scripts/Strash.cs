using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strash : MonoBehaviour
{
    private bool blocked;
    private Transform player;
    public Transform Guard;
    public LayerMask Stop;

    public SpriteRenderer[] myCones;

    public ContactFilter2D contactFilter;

    public Color calm = Color.green;
    public Color rage = Color.red;

    bool foundPlayer = false;
    float rageFactor = 0f;
    float rageFactorGrowSpeed = 0.4f;
    float oldRageFactor = -5f;

    float patrolSpeed = 5f;
    float chaseSpeed = 10f;
    float maxPause = 10f;
    float rotatingSpeed = 180f;

    public Transform[] patrolPoints;
        

    private void Update()
    {
        foundPlayer = false;
        CheckRaycasts();
       
        if (foundPlayer)
        {
            rageFactor += rageFactorGrowSpeed * Time.deltaTime;
            ChasePlayer();
        }
        else
        {
            rageFactor -= rageFactorGrowSpeed * Time.deltaTime;
        }
        rageFactor = Mathf.Clamp(rageFactor, 0f, 1f);
        if (rageFactor != oldRageFactor)
        {
            ColorizeCones();
            oldRageFactor = rageFactor;
        }        
    }

    void CheckRaycasts()
    {
        for (int q = 0; q < myCones.Length; q++)
        {
            RaycastHit2D hit = Physics2D.Raycast(Guard.position, myCones[q].transform.up, 6f);
            if (hit)
            {
                float dist = Vector3.Magnitude(transform.position - new Vector3(hit.point.x, hit.point.y, 0f));
                myCones[q].transform.localScale = Vector3.one * dist * 0.156f; //Слава волшебным цифрам в коде!                
                Mouse hitMouse = hit.transform.GetComponent<Mouse>();
                if (hitMouse)
                {
                    if (hitMouse.isAlly)
                    {
                        foundPlayer = true;
                    }
                }
            }
            else
            {
                myCones[q].transform.localScale = Vector3.one;
            }
        }
    }

    void ChasePlayer()
    {
        transform.up = Vector3.Lerp(transform.up, (player.position - transform.position), Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
    }


    void Patrol()
    {

    }

    void ColorizeCones()
    {
        for (int q=0;q<myCones.Length;q++)
        {
            myCones[q].color = Color.Lerp(calm, rage, rageFactor);
        }
    }

    /*void OnTriggerStay2D(Collider2D myCollider)
    {
        Debug.Log(myCollider.name, myCollider.gameObject);
        Mouse mouse = myCollider.GetComponent<Mouse>();
        if (mouse)
        {
            if (mouse.isPlayer)
            {
                Debug.Log("ASDFASDFASDF");
            }
        }

        if (myCollider.tag == ("Player"))
        {
            Luch();
        }
    }*/

    private void Start()
    {
        player = FindObjectOfType<Run>().transform;
    }

    void Luch()
    {
        Debug.Log("asdfasd");
        player = FindObjectOfType<Run>().transform;
        Vector3 difference = player.position - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Physics2D.Raycast(Guard.position, Vector2.right * transform.localScale.z);
    }
}

