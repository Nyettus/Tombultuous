using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCore : MonoBehaviour
{
    public ItemPools card;
    public Transform location;

    public int whichPool;
    private int whichItem;
    private GameObject[] items;


    // Start is called before the first frame update
    void Start()
    {
        FindPool();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FindPool()
    {
        switch (whichPool)
        {
            case 0:
                items = card.basicPool;
                break;
            default:
                Debug.Log("Pool out of range");
                break;
        }
        whichItem = Random.Range(0, items.Length);
    }



    private bool once = true;
    public void SpawnItem()
    {
        if (once)
        Instantiate(items[whichItem], location.position, location.rotation);
        once = false;
    }
}
