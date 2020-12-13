using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hypnosis : MonoBehaviour
{
    List<Mouse> candidates = new List<Mouse>();

    public GameObject signal;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var mouse = collision.GetComponent<Mouse>();
        if (mouse && mouse.mouseType == Mouse.MouseType.free)
        {
            candidates.Add(mouse);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var mouse = collision.GetComponent<Mouse>();
        if (mouse && candidates.Contains(mouse))
        {
            candidates.Remove(mouse);
        }
    }

    public Mouse Hypnotize()
    {
        if (candidates.Count == 0)
            return null;

        FindObjectOfType<Puzzle>().StartPuzzle();
        Shards.instance.NumberOfUsed++;

        var mouse = candidates[candidates.Count - 1];
        candidates.RemoveAt(candidates.Count - 1);
        return mouse;
    }

    private void FixedUpdate()
    {
        signal.SetActive(candidates.Count > 0 && Shards.instance.NumberOfUnused>Shards.instance.NumberOfUsed);
    }
}
