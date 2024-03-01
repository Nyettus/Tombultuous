using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StructurePairs
{
    public GameObject available = null;
    public GameObject unavailable = null;
    public void SetState(bool state)
    {
        available.SetActive(state);
        unavailable.SetActive(!state);
    }
    public bool CheckIncomplete()
    {
        bool returnState = (available == null || unavailable == null);
        return returnState;
    }

    public StructurePairs(GameObject avail, GameObject unavail)
    {
        available = avail;
        unavailable = unavail;
    }

}
public class HubManager : MonoBehaviour
{
    [SerializeField] private StructurePairs HatShop;

    void Start()
    {
        EnableStructures();
    }

    private void EnableStructures()
    {
        if (!HatShop.CheckIncomplete())
        {
            bool Hat = PlayerPrefs.GetInt("NPC_Hat", 0) == 1;
            HatShop.SetState(Hat);
        }



    }


    public Canvas VaultShop;
    public void VaultKeeperShop(bool state)
    {
        VaultShop.enabled = state;
    }

}
