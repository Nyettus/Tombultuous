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
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            GameManager._.Master.healthMaster.takeDamage(10,Vector3.zero,0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Debug.Log(GameManager._.Master.itemMaster.hasteEquation(0));
        }
    }
#endif
}
