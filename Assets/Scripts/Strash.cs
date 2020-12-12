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
    float oldRageFactor = 0f;

    private void Update()
    {
        foundPlayer = false;
        List<RaycastHit2D> raycastHit2D = new List<RaycastHit2D>();
        for (int q = 0; q < myCones.Length; q++)
        {            
            //Physics2D.Raycast(Guard.position, myCones[q].transform.up, contactFilter, raycastHit2D, 15f);
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
        if (foundPlayer)
        {
            rageFactor += rageFactorGrowSpeed * Time.deltaTime;
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

    void Luch()
    {
        Debug.Log("asdfasd");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 difference = player.position - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Physics2D.Raycast(Guard.position, Vector2.right * transform.localScale.z);
    }
}

