using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private bool followX = true;
    [SerializeField] private bool followY = true;
    [SerializeField] private bool followZ = false;

    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, 0f);

    [SerializeField] private Transform target;
    [SerializeField] private float followSpeed = 1f;
    [SerializeField] private bool smooth = true; // Gdy smooth = false to followSpeed=1 oznacza pod¹¿anie bez opóŸnieñ

    private delegate void deltaFuncDelegate(); // delegat i jego uzycie by przy starcie wybrac rodzaj funkcji i nie uzywac ifa co klatke
    deltaFuncDelegate deltaFunc = null;

    private void Awake()
    {
        if (smooth)
            deltaFunc = smoothDelta;
        else
            deltaFunc = flatDelta;
    }
    void Update()
    {
        if (smooth)
            smoothDelta();
        else
            flatDelta();

        // deltaFunc(); //Tymczasowo nieuzywany delegat zeby bylo latwiej testowac na biezaco
    }

    void smoothDelta()
    {
        if (Vector2.Distance(transform.position, target.position) > 1f)
        {
            Vector3 delta = (target.position - transform.position + offset) * followSpeed * Time.deltaTime; ;
            transform.position += new Vector3(delta.x, delta.y, 0);
        }
    }
    void flatDelta()
    {
        if (Vector2.Distance(transform.position, target.position) > 0f)
        {
            Vector3 newPos = Vector3.Lerp(transform.position, target.position, followSpeed);
            newPos = new Vector3(
                (followX ? newPos.x + offset.x : transform.position.x),
                (followY ? newPos.y + offset.y : transform.position.y),
                (followZ ? newPos.z + offset.z : transform.position.z));
            transform.position = newPos;
        }
    }
}
