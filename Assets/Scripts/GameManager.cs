using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DialogueEditor;


public class GameManager : SingletonPersist<GameManager>
{
    public PlayerMaster Master;

    public PlayableCharacter playerCard;
    public GameObject playerPrefab;
    #region persistable items
    public Dictionary<ItemBase, ItemStack> itemList = new Dictionary<ItemBase, ItemStack>();
    public List<WeaponStorage> weaponStorage = new List<WeaponStorage>();
    public GoldManager goldManager;
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
        //Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);
        //Debug.Log("The state of the OnSceneLoaded: " + CheckMasterError());

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

    }

    public void lockMouse()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }

    public void PauseCommand(InputAction.CallbackContext context)
    {
        Debug.Log("Pause Pressed");
        if (Master == null || ConversationManager.Instance.IsConversationActive) return;

        if (context.performed)
        {
            paused = !paused;
            ShowMouse(paused);
            if (paused)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
    public void ShowMouse(bool state)
    {
        Cursor.visible = state;
        if (state)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }
    public bool ToggleInputs()
    {
        return (paused || ConversationManager.Instance.IsConversationActive);
    }



    public void TransitionScene(int id)
    {
        if (Master != null)
        {
            var currentWeapons = Master.weaponMaster.equippedGuns;
            weaponStorage.Clear();


            for (int i = 0; i < currentWeapons.Length; i++)
            {
                if (currentWeapons[i] == null) continue;
                var temp = currentWeapons[i];
                var tempWeaponStore = new WeaponStorage(temp.prefab, temp.specialTime - Time.time, 0);
                weaponStorage.Add(tempWeaponStore);
                Debug.Log("Looped");
            }
        }

        SceneManager.LoadScene(id);
    }

    public bool CheckMasterError()
    {
        if (Master == null)
        {
            //Debug.LogError("Master Absent");
            return true;
        }
        else return false;
    }


}
