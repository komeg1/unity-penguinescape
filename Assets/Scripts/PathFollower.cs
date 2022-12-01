using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{

    [SerializeField] private GameObject[] points;
    [SerializeField] private int startingPoint = 0;
    [SerializeField] private float moveSpeed = 1f;
    // Start is called before the first frame update
    private int currentPoint;
    private void Start()
    {
        currentPoint = startingPoint;
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, points[currentPoint].transform.position) < 0.1f)
            if (currentPoint + 1 > points.Length - 1)
                currentPoint = 0;
            else
                currentPoint += 1;
        transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].transform.position, moveSpeed * Time.deltaTime);
    }
}
