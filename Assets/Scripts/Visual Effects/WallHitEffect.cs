using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHitEffect : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] ps;
    private float spawnTime;
    // Start is called before the first frame update
    private void OnEnable()
    {
        ps = GetComponents<ParticleSystem>();

    }

    private void OnDisable()
    {
        foreach(ParticleSystem i in ps)
        {
        i.Clear();
        }

    }

    public void Establish()
    {
        foreach (ParticleSystem i in ps)
        {
            i.Play();
        }

        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnTime+ps[0].time > spawnTime + 1)
        {
            gameObject.SetActive(false);
        }
    }
}
