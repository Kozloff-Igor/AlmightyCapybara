using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSound : MonoBehaviour
{
    public AudioClip main;
    public AudioClip boss;

    public void PlayBossTheme(bool play)
    {
        if (play)
        {
            GetComponent<AudioSource>().clip = boss;
        }
        else
        {
            GetComponent<AudioSource>().clip = main;
        }
        GetComponent<AudioSource>().Play();
    }
}
