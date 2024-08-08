using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private ItemPools pool;
    // Start is called before the first frame update
    void Start()
    {
        CombatZone.OnCombatEnded += SpawnItem;
    }

    private void OnDisable()
    {
        CombatZone.OnCombatEnded -= SpawnItem;
    }

    private void SpawnItem(RoomGrid unimportant)
    {
        ItemBase itemToSpawn = pool.ReturnItem(pool.defaultChance);
        Instantiate(itemToSpawn.prefab, transform.position, transform.rotation);
        Destroy(this.gameObject);

    }

}
