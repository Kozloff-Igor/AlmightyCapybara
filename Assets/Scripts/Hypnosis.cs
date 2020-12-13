using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hypnosis : MonoBehaviour
{
    List<Mouse> allMouses;
    List<Mouse> candidates = new List<Mouse>();
    Transform mTransform;

    public GameObject signal;
    public float hypnosisDistance = 4f;

    public AudioClip clip;

    private void Start()
    {
        mTransform = transform;
        allMouses = FindObjectsOfType<Mouse>().Where(m => m.mouseType == Mouse.MouseType.free).ToList();
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    var mouse = collision.GetComponent<Mouse>();
    //    if (mouse && mouse.mouseType == Mouse.MouseType.free)
    //    {
    //        candidates.Add(mouse);
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    var mouse = collision.GetComponent<Mouse>();
    //    if (mouse && candidates.Contains(mouse))
    //    {
    //        candidates.Remove(mouse);
    //    }
    //}

    public void Hypnotize()
    {
        if (candidates.Count == 0)
            return;

        FindObjectOfType<Puzzle>().StartPuzzle();

    }

    public void CompleteHypnosis()
    {
        var mouse = candidates[candidates.Count - 1];
        candidates.RemoveAt(candidates.Count - 1);
        if (mouse)
        {
            Shards.instance.NumberOfUsed++;
            GetComponent<GrowInNumbers>().AddNewMouse(mouse.transform);
            GetComponent<AudioSource>().clip = clip;
            GetComponent<AudioSource>().Play();
        }

    }

    private void FixedUpdate()
    {
        foreach (var mouse in allMouses)
        {
            if (mouse.mouseType == Mouse.MouseType.free && Vector3.Distance(mouse.transform.position, mTransform.position) < hypnosisDistance)
            {
                if (!candidates.Contains(mouse))
                    candidates.Add(mouse);
            }
            else if (candidates.Contains(mouse))
            {
                candidates.Remove(mouse);
            }
        }

        signal.SetActive(candidates.Count > 0 && Shards.instance.NumberOfUnused > Shards.instance.NumberOfUsed);
    }
}
