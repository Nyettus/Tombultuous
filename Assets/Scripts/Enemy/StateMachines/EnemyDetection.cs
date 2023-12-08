using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] private string parameterName;

    [SerializeField] private EnemyComponentMaster CM;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            CM.SetAnimBool(parameterName, true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            CM.SetAnimBool(parameterName, false);
    }
}
