using System.Collections.Generic;
using UnityEngine;

public class SnowCollector : MonoBehaviour
{
    ParticleSystem snowParticles;
    private Transform player;

    List<ParticleSystem.Particle> particlesList = new();
    PlayerItems playerItems;
    private void Start()
    {
        snowParticles = GetComponent<ParticleSystem>();
        player = GameObject.Find("Player").transform;
        //GetChild(1) = SnowForceField
        player.GetChild(1).gameObject.SetActive(false);
        snowParticles.trigger.AddCollider(player);
        playerItems = player.GetComponent<PlayerItems>();
    }

    private void OnParticleTrigger()
    {
        int triggeredParticles = snowParticles.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particlesList);
        
        if (Input.GetKey(KeyCode.E) && playerItems.pickedSnowAmount < playerItems.maxSnowAmount)
        {
            player.GetChild(1).gameObject.SetActive(true);
            for (int i = 0; i < triggeredParticles; i++)
            {

                ParticleSystem.Particle p = particlesList[i];
                p.remainingLifetime = 0;
                player.GetComponent<PlayerItems>().UpdateSnowAmount();
                //Debug.Log("Snow amount: " + player.GetComponent<PlayerItems>().pickedSnowAmount);
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
