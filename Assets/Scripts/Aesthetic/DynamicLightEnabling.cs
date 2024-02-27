using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLightEnabling : MonoBehaviour
{
    //Instead of lights this uses the full gameobject which would include a emissive material
    [SerializeField] private GameObject[] lights;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        SetLights(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") return;
        SetLights(false);
    }




    private void SetLights(bool state)
    {
        foreach(GameObject light in lights)
        {
            light.SetActive(state);
        }
    }
}
