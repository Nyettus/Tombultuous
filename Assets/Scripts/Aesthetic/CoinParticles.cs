using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem system;
    // Start is called before the first frame update
    
    public void Initialise(int moneyCount)
    {
        var em = system.emission;
        em.enabled = true;
        var burst = new ParticleSystem.Burst(0, moneyCount);
        em.SetBurst(0, burst);
        transform.LookAt(GameManager._.Master.transform);

        system.Play();
    }


    private void OnDisable()
    {
        system.Stop();
        system.Clear();
    }
}
