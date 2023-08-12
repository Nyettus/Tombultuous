using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHitEffect : MonoBehaviour
{
    ParticleSystem ps;
    float spawnTime;
    // Start is called before the first frame update
    private void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();

    }

    private void OnDisable()
    {

        ps.Clear();


    }

    public void Establish()
    {

        ps.Play();


        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnTime+ps.time > spawnTime + 1)
        {
            gameObject.SetActive(false);
        }
    }
}
