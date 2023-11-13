using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class GameManager : SingletonPersist<GameManager>
{
    public PlayerMaster Master;

    public GameObject playerPrefab;
    #region persistable items
    public Dictionary<ItemBase, ItemStack> itemList = new Dictionary<ItemBase, ItemStack>();
    #endregion
    //Basic menu
    public bool paused = false;
    private void Awake()
    {

        Startup(this);
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
    }


    protected override void RunOnce()
    {
        base.RunOnce();

        Application.targetFrameRate = 99999;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (paused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed){
            paused = !paused;
            if (paused)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
            }
        }
    }

}
