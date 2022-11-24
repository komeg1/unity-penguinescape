using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
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
            Vector3 delta = (target.position - transform.position) * followSpeed * Time.deltaTime; ;
            transform.position += new Vector3(delta.x, delta.y, 0);
        }
    }
    void flatDelta()
    {
        if (Vector2.Distance(transform.position, target.position) > 0f)
        {
            Vector3 newPos = Vector3.Lerp(transform.position, target.position, followSpeed);
            transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        }
    }
}
