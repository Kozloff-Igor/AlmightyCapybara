using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public GameObject[] lines;
    private int count = -1;
    public GameObject AAAAAAA;

    public void StartPuzzle()
    {
        count++;
        var line = lines[Mathf.Min(count, lines.Length - 1)];
        var lineController = Instantiate(line, transform);
        lineController.GetComponentInChildren<LineController>().onClose += onClose;
        AAAAAAA.SetActive(true);
    }

    void onClose()
    {
        AAAAAAA.SetActive(false);
    }
}
