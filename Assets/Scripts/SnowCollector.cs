using System.Collections.Generic;
using UnityEngine;

public class SnowCollector : MonoBehaviour
{
    [SerializeField] ParticleSystem snowParticles;
    [SerializeField] private Transform player;


    List<ParticleSystem.Particle> particlesList = new();

    private void Start()
    {
        snowParticles = GetComponent<ParticleSystem>();

        //GetChild(1) = SnowForceField
        player.GetChild(1).gameObject.SetActive(false);
    }

    private void OnParticleTrigger()
    {
        int triggeredParticles = snowParticles.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particlesList);
        if (Input.GetKey(KeyCode.E) && player.GetComponent<PlayerItems>().pickedSnowAmount < 100)
        {
            player.GetChild(1).gameObject.SetActive(true);
            for (int i = 0; i < triggeredParticles; i++)
            {

                ParticleSystem.Particle p = particlesList[i];
                p.remainingLifetime = 0;
                player.GetComponent<PlayerItems>().UpdateSnowAmount();
                Debug.Log("Snow amount: " + player.GetComponent<PlayerItems>().pickedSnowAmount);
                particlesList[i] = p;
            }

            snowParticles.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particlesList);
        }
        else
        {
            player.GetChild(1).gameObject.SetActive(false);
        }
    }
}
