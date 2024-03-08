using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public SerialKeyValuePair<string, bool>[] unlockedItems;

    public PlayerData Clone()
    {
        PlayerData data = new();
        ///TODO assign
        data.unlockedItems = unlockedItems;
        return data;
    }
}


public class SaveManager : SingletonPersist<SaveManager>
{

    string savePath;
    string settingsPath;

    [SerializeField]
    private PlayerData playerDataInspector = null;
    public PlayerData defaultPlayerData;

    public Dictionary<string, bool> unlockedItems = new();
    public ItemPools masterPool;

    private void Awake()
    {
        Startup(this);
    }
    protected override void RunOnce()
    {
        base.RunOnce();
        savePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        playerDataInspector = LoadUserData();
        playerDataInspector ??= defaultPlayerData.Clone();
        LoadSaveDataIntoCache(playerDataInspector);
    }
    /// <summary>
    /// Save all DTO props into SingletonData
    /// </summary>
    /// <param name="playerData"></param>
    public void LoadSaveDataIntoCache(PlayerData playerData)
    {
        if (playerData.unlockedItems.Length==0) return;
        unlockedItems = playerData.unlockedItems.ToDict();
        //Loop Through all ItemBases and set unlocked
        var masterItemList = ReturnMasterList();
        foreach (var item in masterItemList)
        {
            if (unlockedItems.TryGetValue(item.ID, out bool isChecked))
            {
                item.unlocked = isChecked;
            }
        }
    }

    /// <summary>
    /// Save current values into inspector DTO
    /// </summary>
    public void SaveCachedDataIntoInspector()
    {
        playerDataInspector.unlockedItems = unlockedItems.ToCereal();
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        SaveCachedDataIntoInspector();
#endif
    }

    public void SaveUserDataToFile()
    {
        Debug.Log("Saving usersettings at " + savePath);
        SaveCachedDataIntoInspector();
        string json = JsonUtility.ToJson(playerDataInspector);
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }

    public void DeleteSaveData()
    {
        var masterItemList = ReturnMasterList();
        foreach (var item in masterItemList)
        {
            if (unlockedItems.TryGetValue(item.ID, out bool isChecked))
            {
                item.unlocked = !isChecked;
            }
        }
        string emptyJson = "{}";
        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(emptyJson);

    }

    private PlayerData LoadUserData()
    {
        try
        {
            using StreamReader reader = new StreamReader(savePath);
            string json = reader.ReadToEnd();
            return JsonUtility.FromJson<PlayerData>(json);
        }
        catch
        {
            return null;
        }

    }

    private IEnumerable<ItemBase> ReturnMasterList()
    {
        var returnList = masterPool.tier1.Concat(masterPool.tier2).Concat(masterPool.tier3).Concat(masterPool.tier4);
        return returnList;
    }

    public void BuyItem(ItemBase ItemToBuy)
    {
        ItemToBuy.unlocked = true;
        unlockedItems.Add(ItemToBuy.ID, true);
    }
}
