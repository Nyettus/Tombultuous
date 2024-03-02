using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SensSlider : MonoBehaviour
{
    [SerializeField]
    private Slider sensSlider;
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private CameraEffects cameraControl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetSensitity();
    }

    private void SetSensitity()
    {
        cameraControl = GameManager._.Master.cameraEffects;
        sensSlider.onValueChanged.AddListener((v) =>
        {
            text.text = v.ToString("0.00");
            cameraControl.lookSens = v/10;
        });
    }

}
