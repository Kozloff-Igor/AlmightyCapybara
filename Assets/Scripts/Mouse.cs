using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public enum MouseType { free, connected, runningAway}

    public MouseType mouseType = MouseType.free;

    Vector3 targetPointToRunAway;

    public bool isPlayer = false;

    public void RunAway()
    {
        mouseType = MouseType.runningAway;
        targetPointToRunAway = transform.position + transform.up * 99999f;//Vector3.ProjectOnPlane(Random.onUnitSphere, Vector3.forward).normalized * 9999f;
    }

    void Start()
    {
        
    }
        
    void Update()
    {
        if (mouseType == MouseType.runningAway)
        {
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetPointToRunAway, Vector3.back), 270f * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, targetPointToRunAway, 40f * Time.deltaTime);
        }
        
    }
}
