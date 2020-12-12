using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shards : MonoBehaviour
{
    public static Shards instance = null;

    private int numberOfUsed;
    public int NumberOfUsed
    {
        get => numberOfUsed;
        set
        {
            numberOfUsed = Mathf.Clamp(value, 0, 11);
            SetShards(numberOfUsed, UsedParent);
        }
    }
    private int numberOfUnused;
    public int NumberOfUnused
    {
        get => numberOfUnused;
        set
        {
            numberOfUnused = Mathf.Clamp(value, 0, 11);
            SetShards(numberOfUnused, UnusedParent);
        }
    }

    [SerializeField]
    private Transform UnusedParent;

    [SerializeField]
    private Transform UsedParent;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }


    }


    public void SetShards(int value, Transform parent)
    {
        for (var i = 0; i < parent.childCount; i++)
        {
            var child = parent.GetChild(i);
            child.gameObject.SetActive(i < value);
        }
    }
}
