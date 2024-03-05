using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Structures
{
    public GameObject[] versions;
    public void SetState(int state)
    {
        int clampedVersion = Mathf.Clamp(state, 0, versions.Length);
        foreach (GameObject version in versions)
        {
            version.SetActive(false);
        }
        versions[clampedVersion].SetActive(true);
    }


    public Structures(GameObject[] versions)
    {
        this.versions = versions;
    }

}
public class HubManager : MonoBehaviour
{
    [SerializeField] private Structures HatShop;


    void Start()
    {
        EnableStructures();
    }

    private void EnableStructures()
    {

        int Hat = PlayerPrefs.GetInt("NPC_Hat", 0);
        HatShop.SetState(Hat);




    }


    public Canvas VaultShop;
    public void VaultKeeperShop(bool state)
    {
        VaultShop.enabled = state;
    }

}
