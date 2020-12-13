using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowInNumbers : MonoBehaviour
{

    public List<Transform> activeMouses;

    public List<Quaternion> targetRotations;

    public bool isPlayer;

    void Start()
    {
        
    }

    public void AddNewMouse(Transform newMouse)
    {
        activeMouses.Add(newMouse);
        targetRotations.Add(new Quaternion());
        //newMouse.tag = "Player";
        newMouse.GetComponent<Mouse>().mouseType = Mouse.MouseType.connected;   
        if (isPlayer)
        {
            newMouse.GetComponent<Collider2D>().enabled = true;
            newMouse.GetComponent<Mouse>().isAlly = true;
            newMouse.GetComponent<Mouse>().growInNumbers = this;
        }
        newMouse.SetParent(transform);
        SetTargetMouseRotations();
    }
        
    public void RemoveMouse(Transform deadMouse)
    {
        int id = activeMouses.IndexOf(deadMouse);
        targetRotations.RemoveAt(id);
        activeMouses.RemoveAt(id);        
        //deadMouse.tag = "Untagged"; //от тагов отказаться потом
        deadMouse.GetComponent<Mouse>().RunAway();
        SetTargetMouseRotations();
    }

    void Update()
    {
        MoveMousesInPositions();         

    }

    public Transform RandomFreeMouse()
    {
        Mouse[] mouses = FindObjectsOfType<Mouse>();
        for (int q=0; q < mouses.Length; q++)
        {
            if (mouses[q].mouseType == Mouse.MouseType.free)
            {
                return mouses[q].transform;
            }
        }
        return null;
    }

    public void SetTargetMouseRotations()
    {
        for (int q = 0; q < activeMouses.Count; q++)
        {
            targetRotations[q] = Quaternion.Euler(0f, 0f, q * 360f / activeMouses.Count);
        }
    }

    void MoveMousesInPositions()
    {
        if (activeMouses.Count == 1)
        {
            activeMouses[0].localRotation = Quaternion.RotateTowards(activeMouses[0].localRotation, targetRotations[0], 180f * Time.deltaTime);
            activeMouses[0].localPosition = Vector3.MoveTowards(activeMouses[0].localPosition, Vector3.down * 1.5f, 15f * Time.deltaTime);
        }
        else
        {
            for (int q = 0; q < activeMouses.Count; q++)
            {
                activeMouses[q].localRotation = Quaternion.RotateTowards(activeMouses[q].localRotation, targetRotations[q], 180f * Time.deltaTime);
                activeMouses[q].localPosition = Vector3.MoveTowards(activeMouses[q].localPosition, Vector3.zero, 15f * Time.deltaTime);
            }
        }
    }
}
