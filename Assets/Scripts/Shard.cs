using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponentInParent<Run>())
        {
            Shards.instance.NumberOfUnused++;
            Destroy(gameObject);
        }
    }
}
