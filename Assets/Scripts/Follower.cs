using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float followSpeed = 1f;

    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > 1f)
        {
            Vector3 delta = (target.position - transform.position) * followSpeed * Time.deltaTime;
            transform.position += new Vector3(delta.x, delta.y, 0);
        }
    }
}
