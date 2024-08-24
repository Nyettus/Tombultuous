using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public CombatZone master;
    [SerializeField] private GameObject[] enemies;

    public void SpawnEnemy()
    {
        int chosenIndex = Random.Range(0, enemies.Length);
        GameObject chosenEnemy = enemies[chosenIndex];
        if (master != null)
        {
            chosenEnemy.GetComponent<EnemyCountHandler>().master = master;
        }
        else
        {
            Debug.LogError("Combat Zone not found on Enemy Spawner");
        }
        chosenEnemy = Instantiate(chosenEnemy, transform.position,transform.rotation);
        chosenEnemy.transform.parent = this.transform;
        chosenEnemy.SetActive(true);

    }


#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnEnemy();
        }
    }
#endif
}
