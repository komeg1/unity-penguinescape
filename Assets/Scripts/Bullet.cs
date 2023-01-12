using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed = 20f;
    [SerializeField] private Rigidbody2D bullet;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private SpriteRenderer sr;

    private bool emitOnce = true;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        mousePos.z = transform.position.z;
        Vector3 rotation = (mousePos - transform.position).normalized;
        Debug.Log("Shoot at " + rotation);
        bullet.velocity = rotation * speed;
        transform.position += rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("KillableDamageSource"))
        {
            collision.gameObject.GetComponent<SpriteDeathScript>().Death();
            GameManager.instance.IncreaseKilledEnemies();
        }
        if (emitOnce)
        {
            var emission = ps.emission;
            var duration = ps.duration;

            emission.enabled = true;
            ps.Play();
            emitOnce = false;
            Destroy(sr);
            Invoke(nameof(DestroyObj), duration);
        }
        
    }

    void DestroyObj()
    {
        Destroy(gameObject);
    }
}
