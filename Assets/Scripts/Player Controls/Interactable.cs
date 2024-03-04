using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{

    /// <summary>
    /// Press e on object to interact on it, done through new input system
    /// </summary>
    /// <param name="context"></param>
    public void InteractWithItem(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float range = 0.5f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position + transform.forward * 2f, range);

            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out ItemCore itemCore))
                {
                    foreach (var baseItem in itemCore.baseItems)
                    {
                        GameManager._.Master.itemMaster.GetItem(baseItem);
                        itemCore.OnPickup();
                    }

                }
                else if (collider.TryGetComponent(out WeaponCore weaponCore))
                {
                    weaponCore.pickUpWeapon();
                }
                else if(collider.TryGetComponent(out DialogueTrigger diagtrigger))
                {
                    diagtrigger.StartConvo();
                }
                else if (collider.TryGetComponent(out NextLevel nextLevel))
                {
                    nextLevel.GotoNextLevel();
                }
                else if(collider.TryGetComponent(out ChangePlayer playerChange))
                {
                    playerChange.EquipNewCharacter();
                }
                
            }
        }
    }



    public void Recycle(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (!GameManager._.Master.persistentManager.canRecycle) return;
        float range = 0.5f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position + transform.forward * 2f, range);
        foreach(Collider collider in colliderArray)
        {
            if(collider.TryGetComponent(out ItemCore itemcore))
            {
                itemcore.OnRecycle();
            }
        }
    }
}
