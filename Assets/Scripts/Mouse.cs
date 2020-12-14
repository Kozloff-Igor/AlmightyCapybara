using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mouse : MonoBehaviour
{
    public enum MouseType { free, connected, runningAway, guard }

    public int mouseColorId;

    public MouseType mouseType = MouseType.free;

    Vector3 targetPointToRunAway;

    public GrowInNumbers growInNumbers;

    public bool isPlayer = false;
    public bool isAlly = false;
    public bool isEnemy = false;

    public void RunAway()
    {
        if (isPlayer)
        {
            if (Checkpoint.lastCheckpoint)
            {
                Checkpoint.lastCheckpoint.Reburn(GetComponentInParent<GrowInNumbers>());
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        }
        else
        {
            GetComponent<Collider2D>().enabled = false;
            mouseType = MouseType.runningAway;
            isAlly = false;
            isEnemy = false;
            transform.SetParent(null);
            targetPointToRunAway = transform.position + transform.up * 99999f;//Vector3.ProjectOnPlane(Random.onUnitSphere, Vector3.forward).normalized * 9999f;
        }
    }

    void Start()
    {

    }

    void Update()
    {
        if (mouseType == MouseType.runningAway)
        {
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetPointToRunAway, Vector3.back), 270f * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, targetPointToRunAway, 15f * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isAlly)
        {
            Mouse hittedMouse = collision.collider.gameObject.GetComponent<Mouse>();
            if (hittedMouse)
            {
                if (hittedMouse.isEnemy)
                {
                    if (mouseColorId == hittedMouse.mouseColorId)
                    {
                        GrowInNumbers kingGrow = hittedMouse.growInNumbers;
                        if (kingGrow)
                        {
                            kingGrow.RemoveMouse(hittedMouse.transform);
                            if (kingGrow.activeMouses.Count == 0)
                            {
                                FindObjectOfType<Run>().FinishGame();
                            }
                        }
                    }
                    else
                    {
                        growInNumbers.RemoveMouse(transform);
                    }
                }
            }
        }
    }

}
