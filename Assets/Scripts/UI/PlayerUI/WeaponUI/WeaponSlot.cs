using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField] private GameObject activeCanvas;
    [SerializeField] private GameObject idleImage;
    [SerializeField] private TextMeshProUGUI weaponName;
    public TextMeshProUGUI slotNo;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Init()
    {
        activeCanvas.SetActive(false);
    }

    public void SetName(bool state, string name = "")
    {
        activeCanvas.SetActive(state);
        idleImage.SetActive(!state);
        if (!state) return;
        weaponName.text = name;
    }
    public void WholeEnable(bool state)
    {
        gameObject.SetActive(state);
    }
}
