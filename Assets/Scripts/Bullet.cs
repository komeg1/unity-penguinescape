using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed = 20f;
    [SerializeField] private Rigidbody2D bullet;

    // Start is called before the first frame update
    void Start()
    {
        bullet.velocity = transform.right * speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
