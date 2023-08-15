using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : SingletonPersist<RoomManager>
{
    // Start is called before the first frame update
    void Awake()
    {
        Startup(this);
    }

    protected override void RunOnce()
    {
        base.RunOnce();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
            
        }
    }
}
