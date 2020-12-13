using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strash : MonoBehaviour
{
    private bool blocked;
    private Transform player;
    public Transform Guard;
    public LayerMask Stop;

    public SpriteRenderer[] myCones;

    public ContactFilter2D contactFilter;

    public Color calm = Color.green;
    public Color rage = Color.red;

    bool foundPlayer = false;
    float rageFactor = 0f;
    float rageFactorGrowSpeed = 0.4f;
    float oldRageFactor = -5f;

    float patrolSpeed = 1f;
    float chaseSpeed = 3f;
    float minPause = 7f;
    float maxPause = 10f;
    float rotatingSpeed = 120f;

    public enum Status { moving, lookingAround, chase }
    public Status status;
    float timer;

    float lookingAroundTimer;
    float minLookAroundTime = 3f;
    float maxLookAroundTime = 5f;

    public Transform[] patrolPoints;
    int currentPatrolPointId;

    SkeletonAnimation animation;
    bool alarm;

    Vector3 startPosition;
    private void Update()
    {
        foundPlayer = false;
        CheckRaycasts();

        if (foundPlayer)
        {
            rageFactor += rageFactorGrowSpeed * Time.deltaTime;
            ChasePlayer();
        }
        else
        {
            rageFactor -= rageFactorGrowSpeed * Time.deltaTime;
            if (rageFactor < 0) { Patrol(); }
        }
        CheckLose();
        rageFactor = Mathf.Clamp(rageFactor, 0f, 1f);
        if (rageFactor != oldRageFactor)
        {
            ColorizeCones();
            oldRageFactor = rageFactor;
        }
        if (!alarm && foundPlayer)
        {
            GetComponentInParent<AudioSource>().Play();
        }
        if (alarm && !foundPlayer)
        {
            GetComponentInParent<AudioSource>().Stop();
        }
        alarm = foundPlayer;
        CheckAnimations();
    }

    void CheckRaycasts()
    {
        if (Vector3.SqrMagnitude(transform.position - player.position) < 800f)
        {
            for (int q = 0; q < myCones.Length; q++)
            {
                RaycastHit2D hit = Physics2D.Raycast(Guard.position, myCones[q].transform.up, 6f);
                if (hit)
                {
                    float dist = Vector3.Magnitude(transform.position - new Vector3(hit.point.x, hit.point.y, 0f));
                    myCones[q].transform.localScale = Vector3.one * dist * 0.15f; //Слава волшебным цифрам в коде!                
                    if (hit.transform == player)
                    {
                        foundPlayer = true;
                        /*Mouse hitMouse = hit.transform.GetComponent<Mouse>();
                        if (hitMouse)
                        {
                            if (hitMouse.isAlly)
                            {
                                foundPlayer = true;
                            }
                        }*/
                    }
                }
                else
                {
                    myCones[q].transform.localScale = Vector3.one;
                }
            }
        }
    }

    void ChasePlayer()
    {
        transform.up = Vector3.Lerp(transform.up, (player.position - transform.position), Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
    }


    void Patrol()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (status == Status.lookingAround) { status = Status.moving; } else { status = Status.lookingAround; }
            timer = Random.Range(minPause, maxPause);
        }
        if (status == Status.lookingAround)
        {
            lookingAroundTimer -= Time.deltaTime;
            if (lookingAroundTimer < 0)
            {
                lookingAroundTimer = Random.Range(minLookAroundTime, maxLookAroundTime);
                rotatingSpeed = -rotatingSpeed;
            }
            transform.Rotate(0f, 0f, rotatingSpeed * Time.deltaTime);
        }
        else
        {
            if (status == Status.moving)
            {
                transform.up = Vector3.Lerp(transform.up, patrolPoints[currentPatrolPointId].position - transform.position, Time.deltaTime * 0.8f);
                transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPatrolPointId].position, patrolSpeed * Time.deltaTime);
                if (Vector3.SqrMagnitude(transform.position - patrolPoints[currentPatrolPointId].position) < 0.1f) currentPatrolPointId++;
                if (currentPatrolPointId >= patrolPoints.Length) currentPatrolPointId = 0;
            }
        }
    }

    void ColorizeCones()
    {
        for (int q = 0; q < myCones.Length; q++)
        {
            myCones[q].color = Color.Lerp(calm, rage, rageFactor);
        }
    }

    /*void OnTriggerStay2D(Collider2D myCollider)
    {
        Debug.Log(myCollider.name, myCollider.gameObject);
        Mouse mouse = myCollider.GetComponent<Mouse>();
        if (mouse)
        {
            if (mouse.isPlayer)
            {
                Debug.Log("ASDFASDFASDF");
            }
        }

        if (myCollider.tag == ("Player"))
        {
            Luch();
        }
    }*/

    private void Start()
    {
        player = FindObjectOfType<Run>().transform;
        animation = GetComponentInChildren<SkeletonAnimation>();
        startPosition = transform.position;
    }

    void Luch()
    {
        Debug.Log("asdfasd");
        player = FindObjectOfType<Run>().transform;
        Vector3 difference = player.position - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Physics2D.Raycast(Guard.position, Vector2.right * transform.localScale.z);
    }

    void CheckAnimations()
    {
        if (alarm)
        {
            if (animation.AnimationName != "ALERT!")
                animation.AnimationName = "ALERT!";
        }
        else
        {
            if (status == Status.moving && animation.AnimationName != "walk")
            {
                animation.AnimationName = "walk";
            }
            if (status == Status.lookingAround && animation.AnimationName != "idle")
            {
                animation.AnimationName = "idle";
            }
        }
    }

    public void Reload()
    {
        currentPatrolPointId = 0;
        transform.position = startPosition;
    }

    void CheckLose()
    {
        if (rageFactor >= 1f)
        {
            if(Checkpoint.lastCheckpoint)
            Checkpoint.lastCheckpoint.Reburn(player.GetComponent<GrowInNumbers>());
        }
    }
}

