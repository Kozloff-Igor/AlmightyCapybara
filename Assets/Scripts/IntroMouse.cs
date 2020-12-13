using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;


public class IntroMouse : MonoBehaviour
{
    public float rotateSpeed = 500f;
    public float scaleSpeed = 2f;
    bool isFinish;

    public GameObject player;
    public void Update()
    {
        if (!isFinish && transform.localScale.x > 1f)
        {
            transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
            var scale = transform.localScale;
            transform.localScale = new Vector3(scale.x - scaleSpeed * Time.deltaTime, scale.y - scaleSpeed * Time.deltaTime, 1);
        }
        else if (!isFinish)
        {
            isFinish = true;
            player.SetActive(true);
            Destroy(gameObject);
        }
    }
}
