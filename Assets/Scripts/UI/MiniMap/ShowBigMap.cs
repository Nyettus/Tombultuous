using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShowBigMap : MonoBehaviour
{

    [SerializeField] private UITrackLocation UITracker;

    [SerializeField] private Image mapBackground;
    [SerializeField] private RawImage bigRenderTexture;
    [SerializeField] private RawImage smallRenderTexture;
    [SerializeField] private RenderTexture[] mapTextures = new RenderTexture[2];


    [SerializeField] private Camera mapCamera;

    // Start is called before the first frame update
    void Start()
    {
        UITracker = GetComponent<UITrackLocation>();
    }


    [SerializeField] private bool toggleMap = false;
    public void ToggleBigMap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            toggleMap = !toggleMap;
            int state = toggleMap ? 1 : 0;
            mapCamera.targetTexture = mapTextures[state];
            transform.position = new Vector3(0, transform.position.y, 0);
            UITracker.pauseTrack = toggleMap;

            smallRenderTexture.enabled = !toggleMap;
            if (toggleMap)
            {
                mapCamera.orthographicSize = 400;

                if (RoomManager._ != null)
                    mapCamera.transform.position = RoomManager._.worldMidpoint;
            }
            else mapCamera.orthographicSize = 100;
            bigRenderTexture.enabled = toggleMap;
            mapBackground.enabled = toggleMap;
            Debug.Log(mapCamera.targetTexture.name);
        }
    }


}
