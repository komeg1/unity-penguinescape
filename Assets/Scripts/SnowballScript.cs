using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEngine.ParticleSystem;

public class SnowballScript : MonoBehaviour
{
    // Start is called before the first frame update

    public ParticleSystem snowBurst;
    Rigidbody2D sphereRigidBody;
    bool bursted = false;
    public int burstParticlesAmount = 0;

    void Start()
    {
        sphereRigidBody = GetComponent<Rigidbody2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (sphereRigidBody.velocity.sqrMagnitude < 1)
            return;
        if (collision.gameObject.CompareTag("KillableDamageSource"))
        {
            collision.gameObject.GetComponent<SpriteDeathScript>().Death();
            GameManager.instance.IncreaseKilledEnemies();
        }
        if (!bursted)
        {
            ParticleSystem ps = Instantiate(snowBurst);
            ps.emission.SetBurst(0, new ParticleSystem.Burst(0, burstParticlesAmount, 0, 0));
            ps.Play();
            bursted = true;
        }

    }


}
