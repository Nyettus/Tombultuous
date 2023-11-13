using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonPlayer : MonoBehaviour
{
    public Transform position;
    // Start is called before the first frame update
    void Start()
    {
        Summon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Summon()
    {
        var temp = Instantiate(GameManager._.playerPrefab, position);
        GameManager._.Master = temp.GetComponent<PlayerMaster>();
    }
}
