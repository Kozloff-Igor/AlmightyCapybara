using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Star : MonoBehaviour, IPointerEnterHandler
{
    
    public void OnPressed()
    {
        FindObjectOfType<LineController>().StartLine(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<LineController>().AddStar(this);
    }


}
