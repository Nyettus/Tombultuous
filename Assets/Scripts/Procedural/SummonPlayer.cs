using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SummonPlayer : MonoBehaviour
{
    public Transform spawnPosition;

    public PlayerMaster holding;
    // Start is called before the first frame update
    void Start()
    {
        Summon();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        GameManager._.Master = holding;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager._.CheckMasterError())
        {
            Debug.Log("Attempted to set master again");
            GameManager._.Master = holding;
        }
        else
            Destroy(this);
        Debug.Log("Im alive");
    }


    public void Summon()
    {
        var temp = Instantiate(GameManager._.playerPrefab);
        holding = temp.transform.GetChild(0).GetComponent<PlayerMaster>();
        holding.movementMaster.rb.Move(spawnPosition.position, Quaternion.identity);
    }
}
