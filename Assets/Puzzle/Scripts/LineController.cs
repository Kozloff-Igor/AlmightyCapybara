using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public event Action onClose;
    private LineRenderer lineRenderer;
    private bool isDrawing;
    private List<string> stars = new List<string>();
    private int starAmount;

    public GameObject[] Task;
    [SerializeField]
    private TMPro.TextMeshProUGUI Text;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        StartPuzzle();
    }
    public void StartLine(Star star)
    {
        if (!isDrawing)
        {
            starAmount = Task.Length;//GetComponentsInChildren<Star>().Length;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, new Vector3(star.transform.position.x, star.transform.position.y, 0));
            isDrawing = true;
            stars.Add(star.gameObject.name);
        }
    }

    public void AddStar(Star star)
    {
        if (isDrawing)
        {
            stars.Add(star.gameObject.name);
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(star.transform.position.x, star.transform.position.y, 0));
            if (starAmount == stars.Count)
            {
                isDrawing = false;
                CompletePuzzle();
            }
            else
            {
                lineRenderer.positionCount++;
            }
        }
        else
        {
            StartLine(star);
        }
    }

    private void Update()
    {
        if (isDrawing)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(mousePosition.x, mousePosition.y, 0));
        }
    }

    public void StartPuzzle()
    {
        StartCoroutine(Flashing());
    }

    public void CompletePuzzle()
    {
        for (var i = 0; i < Task.Length; i++)
        {
            var child = Task[i];
            if (stars[i]!= child.name)
            {
                Lose();
                return;
            }
        }
        Win();
    }

    void Lose()
    {
        //Text.gameObject.SetActive(true);
        //Text.text = "Lose";

        stars.Clear();
        lineRenderer.positionCount = 0;
        isDrawing = false;

        StartPuzzle();
    }

    void Win()
    {
        //Text.gameObject.SetActive(true);
        //Text.text = "Win";

        for (var i = 0; i < transform.childCount; i++)
        {
            StartCoroutine(FlashingOne(i));
        }
        Invoke("Close", 0.5f);
    }

    IEnumerator Flashing()
    {
        yield return new WaitForSeconds(1f);

        for (var i = 0; i < Task.Length; i++)
        {
            var child = Task[i].transform;
            var time = 0.3f;
            while (time > 0)
            {
                yield return new WaitForEndOfFrame();
                child.localScale = new Vector3(Mathf.Clamp(child.localScale.x + Time.deltaTime * 5, 1f, 5f), Mathf.Clamp(child.localScale.y + Time.deltaTime * 5, 1f, 5f), 1);
                time -= Time.deltaTime;
            }

            time = 0.3f;
            while (time > 0)
            {
                yield return new WaitForEndOfFrame();
                child.localScale = new Vector3(Mathf.Clamp(child.localScale.x - Time.deltaTime * 5, 1f, 5f), Mathf.Clamp(child.localScale.y - Time.deltaTime * 5, 1f, 5f), 1);
                time -= Time.deltaTime;
            }


        }
    }

    IEnumerator FlashingOne(int i)
    {
        var child = transform.GetChild(i);
        var time = 0.3f;
        while (time > 0)
        {
            yield return new WaitForEndOfFrame();
            child.localScale = new Vector3(Mathf.Clamp(child.localScale.x + Time.deltaTime * 5, 1f, 5f), Mathf.Clamp(child.localScale.y + Time.deltaTime * 5, 1f, 5f), 1);
            time -= Time.deltaTime;
        }

        time = 0.3f;
        while (time > 0)
        {
            yield return new WaitForEndOfFrame();
            child.localScale = new Vector3(Mathf.Clamp(child.localScale.x - Time.deltaTime * 5, 1f, 5f), Mathf.Clamp(child.localScale.y - Time.deltaTime * 5, 1f, 5f), 1);
            time -= Time.deltaTime;
        }
    }

    void Close()
    {
        if (onClose != null)
        {
            onClose.Invoke();
        }
        Destroy(transform.parent.gameObject);
    }
}
