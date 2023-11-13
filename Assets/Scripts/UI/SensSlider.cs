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
    private CameraControls cameraControl;
    // Start is called before the first frame update
    void Start()
    {
        //SetSensitity();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetSensitity()
    {
        cameraControl = GameManager._.Master.GetComponentInChildren<CameraControls>();
        sensSlider.onValueChanged.AddListener((v) =>
        {
            text.text = v.ToString("0.00");
            cameraControl.sens = v;
        });
    }

}
