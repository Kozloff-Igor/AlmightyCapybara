using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScene : MonoBehaviour
{
    Image image;
    public Text text;

    int aaa = -1;

    void Start()
    {
        image = GetComponent<Image>();
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Close());
            //Invoke("GoNext", 0.6f);
            text.gameObject.SetActive(false);
        }
        else
        {
            if(text.color.a == 1f)
            {
                aaa = -1;
            }
            if (text.color.a == 0f)
            {
                aaa = 1;
            }
            text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Clamp(text.color.a + Time.deltaTime * aaa, 0f,1f));
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

