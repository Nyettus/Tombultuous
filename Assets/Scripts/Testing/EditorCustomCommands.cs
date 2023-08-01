using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class EditorCustomCommands : MonoBehaviour
{
#if UNITY_EDITOR


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {

            GameManager._.Master.itemMaster.onKillItemHandler.OnKill();
        }
    }
#endif
}
