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

    #region Freeze inputs
    public bool inMenu = false;
    public Canvas whichMenu;
    public bool isDead = false;
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

    public void PauseCommand(InputAction.CallbackContext context)
    {
        bool convoBool = false;
        if (ConversationManager.Instance != null) convoBool = ConversationManager.Instance.IsConversationActive;
        if (inMenu)
        {
            CloseMenu();
            return;
        }
        if (Master == null || convoBool) return;


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
        if (ConversationManager.Instance == null)
        {
            return paused;
        }
        else
        {
            return (paused || ConversationManager.Instance.IsConversationActive || inMenu || isDead);
        }

    }
    public void CloseMenu()
    {
        if (!inMenu) return;
        Debug.Log("CloseMenuTriggered");
        whichMenu.enabled = false;
        inMenu = false;
        ShowMouse(false);
    }

    public void EndGame(bool win)
    {
        var transitionGold = goldManager.FinalGold(win);
        Debug.Log(transitionGold);
        var newTotal = PlayerPrefs.GetInt("PermGold",0)+transitionGold;
        PlayerPrefs.SetInt("PermGold",newTotal);
        if (win)
        {
            int victoryCount = PlayerPrefs.GetInt("WinCount", 0) + 1;
            PlayerPrefs.SetInt("WinCount", victoryCount);
        }
        else
        {
            int LossCount = PlayerPrefs.GetInt("LossCount", 0) + 1;
            PlayerPrefs.SetInt("LossCount", LossCount);
        }
        TransitionScene((int)Scenes.Hub,false);
    }

    public void TransitionScene(int id, bool saveWeapons = true)
    {
        if (Master != null)
        {
            var currentWeapons = Master.weaponMaster.equippedGuns;
            weaponStorage.Clear();


            for (int i = 0; i < currentWeapons.Length; i++)
            {
                if (currentWeapons[i] == null) continue;
                if (!saveWeapons) continue;
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
