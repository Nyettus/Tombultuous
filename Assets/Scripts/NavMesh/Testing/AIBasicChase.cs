using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBasicChase : MonoBehaviour
{

    private NavMeshAgent agent;
    public AnimationCurve m_Curve;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = GameManager._.Master.gameObject.transform.position;
    }
}
