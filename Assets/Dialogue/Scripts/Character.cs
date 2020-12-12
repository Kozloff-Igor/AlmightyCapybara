using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField]
    private GameObject TextObject;
    [SerializeField]
    private TextMeshProUGUI Text;

    public void ShowText(string text)
    {
        TextObject.SetActive(true);
        Text.text = text;
    }

    public void HideText()
    {
        TextObject.SetActive(false);
    }
}
