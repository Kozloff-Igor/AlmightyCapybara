using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPointer : MonoBehaviour
{
    public List<Transform> mouses;
    public List<Transform> shards;

    public Transform arrowToMouses;
    public Transform arrowToShard;
    
    public Transform closestMouse;
    public Transform closestShard;

    void Update()
    {
        closestMouse = ClosestInList(mouses);
        if (closestMouse)
        {            
            arrowToMouses.up = Vector3.Lerp(arrowToMouses.up, closestMouse.position + Vector3.up * 1.3f - transform.position, Time.deltaTime);
        } else
        {
            arrowToMouses.gameObject.SetActive(false);
        }
        closestShard = ClosestInList(shards);
        if (closestShard)
        {
            arrowToShard.up = Vector3.Lerp(arrowToShard.up, closestShard.position - transform.position, Time.deltaTime);
        } else
        {
            arrowToShard.gameObject.SetActive(false);
        }
    }

    public void ShardCollected(Transform shard)
    {
        shards.Remove(shard);
    }

    public void MouseHypnotized(Transform mouse)
    {
        mouses.Remove(mouse);
    }

    Transform ClosestInList(List<Transform> transforms)
    {
        float dist = 9999999f;
        int closestId = -1;
        float tempDist;
        for (int q = 0; q < transforms.Count; q++)
        {
            if (transforms[q]) {
                tempDist = Vector3.SqrMagnitude(transforms[q].position - transform.position);
                if (tempDist < dist)
                {
                    closestId = q;
                    dist = tempDist;
                }
            }
        }
        if (closestId > -1) { return transforms[closestId]; }
        return null;
    }

}
