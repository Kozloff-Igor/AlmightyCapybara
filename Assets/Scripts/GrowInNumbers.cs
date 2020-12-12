using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowInNumbers : MonoBehaviour
{

    public List<Transform> activeMouses;

    public List<Quaternion> targetRotations;

    void Start()
    {
        
    }

    void AddNewMouse(Transform newMouse)
    {
        activeMouses.Add(newMouse);
        targetRotations.Add(new Quaternion());
        //newMouse.tag = "Player";
        newMouse.GetComponent<Mouse>().mouseType = Mouse.MouseType.connected;
        newMouse.SetParent(transform);
        SetTargetMouseRotations();
    }
        
    void RemoveMouse(Transform deadMouse)
    {
        int id = activeMouses.IndexOf(deadMouse);
        targetRotations.RemoveAt(id);
        activeMouses.RemoveAt(id);
        deadMouse.SetParent(null);
        //deadMouse.tag = "Untagged"; //от тагов отказаться потом
        deadMouse.GetComponent<Mouse>().RunAway();
        SetTargetMouseRotations();
    }

    void Update()
    {
        MoveMousesInPositions();    
        if (Input.GetKeyDown(KeyCode.X))
        {
            Transform randMouse = RandomFreeMouse();
            if (randMouse)
            {
                AddNewMouse(randMouse);
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (activeMouses.Count > 1)
            {
                RemoveMouse(activeMouses[Random.Range(1, activeMouses.Count)]);
            }
        }
    }

    Transform RandomFreeMouse()
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

    void SetTargetMouseRotations()
    {
        for (int q = 1; q < activeMouses.Count; q++)
        {
            targetRotations[q] = Quaternion.Euler(0f, 0f, q * 360f / activeMouses.Count);
        }
    }

    void MoveMousesInPositions()
    {
        for (int q = 0; q < activeMouses.Count; q++)
        {
            activeMouses[q].localRotation = Quaternion.RotateTowards(activeMouses[q].localRotation, targetRotations[q], 180f * Time.deltaTime);
            activeMouses[q].localPosition = Vector3.MoveTowards(activeMouses[q].localPosition, Vector3.zero, 15f * Time.deltaTime);
        }
    }
}
