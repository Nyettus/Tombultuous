using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulBox;

public class DamageIndicatorMaster : MonoBehaviour
{
    private List<DamageIndicatorAlign> DIList = new List<DamageIndicatorAlign>();
    [SerializeField] private GameObject DamageIndicatorObject;
    [SerializeField] private int maxIncrements;
    private int increment = 0;

    private void Start()
    {
        SpawnDI();
    }

    private void SpawnDI()
    {
        for (int i = 0; i < maxIncrements; i++)
        {
            var holding = Instantiate(DamageIndicatorObject, this.transform);
            DIList.Add(holding.GetComponent<DamageIndicatorAlign>());
        }
    }

    public void SpawnIncrement(int damage, Vector3 position)
    {
        DIList[increment].Initialise(damage, position);
        increment = PsychoticBox.WrapIndex(increment + 1, maxIncrements);
    }
}
