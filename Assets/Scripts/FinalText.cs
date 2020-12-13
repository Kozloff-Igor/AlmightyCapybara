using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalText : MonoBehaviour
{
    public UnityEngine.UI.Image image;
    public UnityEngine.UI.Image image2;

    public static bool isEnd;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isEnd)
        {
            while (image.color.a < 1f)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + Time.deltaTime * 0.01f);
                image2.color = new Color(image2.color.r, image2.color.g, image2.color.b, image2.color.a + Time.deltaTime * 0.01f);
            }
        }
    }
}
