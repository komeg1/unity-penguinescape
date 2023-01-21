using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WinScreenBoatScript : MonoBehaviour
{

    [SerializeField] float r = 0.1f;
    [SerializeField] float angle = 0f;
    [SerializeField] float speed = 1f;

    Vector3 delta;
    Vector3 original;
    private void Start()
    {
        original = transform.localPosition;
    }
    void Update()
    {
        delta = new Vector3(Mathf.Sin(angle) * r, Mathf.Cos(angle) * r, 0f);
        transform.localPosition = original + delta;
        angle += speed * Time.deltaTime;
    }
}
