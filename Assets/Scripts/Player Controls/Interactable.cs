using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{

    public void InteractWithItem(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float range = 0.5f;
            Collider [] colliderArray = Physics.OverlapSphere(transform.position + transform.forward*2f, range);

            foreach(Collider collider in colliderArray)
            {

                if(collider.TryGetComponent(out ItemCore itemCore))
                {
                    itemCore.PickupItem(GameManager.instance.Master.itemMaster);
                }
                else if(collider.TryGetComponent(out WeaponCore weaponCore))
                {
                    weaponCore.pickUpWeapon();
                }
            }
        }

    }

}
