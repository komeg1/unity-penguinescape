using UnityEngine;

public class PathFollower : MonoBehaviour
{

    [SerializeField] private GameObject[] points;
    [SerializeField] private int startingPoint = 0;
    [SerializeField] private float moveSpeed = 1f;
    // Start is called before the first frame update
    private int currentPointIndex;
    public GameObject currentPoint;
    private void Start()
    {
        currentPointIndex = startingPoint;
        currentPoint = points[currentPointIndex];
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, points[currentPointIndex].transform.position) < 0.1f)
            if (currentPointIndex + 1 > points.Length - 1)
                currentPointIndex = 0;
            else
                currentPointIndex += 1;
        currentPoint = points[currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, points[currentPointIndex].transform.position, moveSpeed * Time.deltaTime);
    }
}
