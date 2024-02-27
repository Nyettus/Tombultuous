using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireFlicker : MonoBehaviour
{
    Light fire;
    private float initInt;
    private float initRan;
    [SerializeField]
    private float intRange = 2f;
    private float ranRange = 1f;
    [SerializeField]
    private float flickerTime = 1;
    [SerializeField]
    private float moveSpeed = 10;
    private Vector3 movement = new Vector3(0,0,0);
    [SerializeField]
    private float speedRandomness = 1f;

    // Start is called before the first frame update
    void Start()
    {
        fire = GetComponent<Light>();
        initInt = fire.intensity;
        initRan = fire.range;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Flicker();
    }


    private void Flicker()
    {
        float randomTime = Random.Range(-speedRandomness, speedRandomness);
        fire.intensity = intRange * Mathf.Sin((Time.time+randomTime) * flickerTime) + initInt;
        fire.range = ranRange * Mathf.Cos((Time.time + randomTime) * flickerTime) + initRan;
        movement.x = Mathf.Sin(Time.time*moveSpeed);
        movement.z = Mathf.Cos(Time.time*moveSpeed);
        transform.position += (movement*0.0005f);
    }
}
