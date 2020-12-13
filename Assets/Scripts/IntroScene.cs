using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScene : MonoBehaviour
{
    Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Close());
            //Invoke("GoNext", 0.6f);
        }
    }
    public void GoNext()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator Close()
    {
        while (image.color.a < 1f)
        {
            yield return new WaitForEndOfFrame();
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + Time.deltaTime);
        }
        GoNext();
    }
}

