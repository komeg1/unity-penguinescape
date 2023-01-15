using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    ParticleSystem snowBurstInstance;

    void Start()
    {
        sphereRigidBody = GetComponent<Rigidbody2D>();
    }

    private void burst()
    {
        if (bursted)
            return;

        snowBurstInstance = Instantiate(snowBurst, transform);
        //ps.emission.SetBurst(0, new ParticleSystem.Burst(0, burstParticlesAmount, 0, 0));
        snowBurstInstance.Emit(burstParticlesAmount);

        snowBurstInstance.Play();
        bursted = true;
        GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(delayedDestroy());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("KillableDamageSource"))
        {
            collision.gameObject.GetComponent<SpriteDeathScript>().Death();
            GameManager.instance.IncreaseKilledEnemies();
        }
        if (sphereRigidBody.mass <= 1.5f)
            burst();
    }
    IEnumerator delayedDestroy()
    {
        sphereRigidBody.freezeRotation = true;
        sphereRigidBody.drag = 10f;
        //sphereRigidBody.simulated = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        //snowBurstInstance.transform.SetParent(null, true);
        snowBurstInstance.transform.SetParent(null, true);
        snowBurstInstance.transform.localScale = Vector3.one;


        Destroy(this.gameObject);
    }

}
