using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strash : MonoBehaviour
{
    private bool blocked;
    private Transform player;
    public Transform Guard;
    public LayerMask Stop;

    void OnTriggerStay2D(Collider2D myCollider)
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

       /* if (myCollider.tag == ("Player"))
        {
            Luch();
        }*/
    }

    void Luch()
    {
        Debug.Log("asdfasd");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 difference = player.position - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Physics2D.Raycast(Guard.position, Vector2.right * transform.localScale.z);
    }
}

