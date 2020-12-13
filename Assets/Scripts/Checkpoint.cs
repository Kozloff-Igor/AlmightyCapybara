using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Checkpoint lastCheckpoint;

    public Transform[] activeMouses;
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Run>())
        {
            lastCheckpoint = this;
            activeMouses = new Transform[collision.GetComponentInParent<GrowInNumbers>().activeMouses.Count];
            collision.GetComponentInParent<GrowInNumbers>().activeMouses.CopyTo(activeMouses);
        }
    }

    public void Reburn(GrowInNumbers player)
    {
        foreach (var mouse in activeMouses)
        {
            if (!player.activeMouses.Contains(mouse))
            {
                player.AddNewMouse(mouse);
            }
        }

        player.transform.position = transform.position;

        var guards = FindObjectsOfType<Strash>();
        foreach(var guard in guards)
        {
            guard.Reload();
        }

        FindObjectOfType<King>().Reload();
    }
}
