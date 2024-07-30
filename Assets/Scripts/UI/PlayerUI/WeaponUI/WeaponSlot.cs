using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField] private GameObject activeCanvas;
    [SerializeField] private GameObject idleImage;
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private Image smallThumbnail;
    [SerializeField] private Image largeThumbnail;
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
        if (largeThumbnail.sprite == null)
            weaponName.text = name;
        else
            weaponName.text = "";
    }
    public void SetThumbnail(Sprite small, Sprite large)
    {
        if (small != null)
        {
            smallThumbnail.sprite = small;
            smallThumbnail.color = Color.white;
        }
        else
        {
            smallThumbnail.color = Color.clear;
        }
        if (large != null)
        {
            largeThumbnail.sprite = large;
            largeThumbnail.color = Color.white;
        }
        else
        {
            largeThumbnail.color = Color.clear;
        }


    }
    public void WholeEnable(bool state)
    {
        gameObject.SetActive(state);
    }
}
