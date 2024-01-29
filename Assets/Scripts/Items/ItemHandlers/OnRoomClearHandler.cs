using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRoomClearHandler : MonoBehaviour
{
    public ItemMaster itemMaster;
    // Update is called once per frame
    void FixedUpdate()
    {
        
    }



    #region Crystaline Madness
    public CrystalineMadness madnessCard;
    public float madnessModifier = 0;

    private void CalculateMadness()
    {
        int madnessCount = itemMaster.GetItemCount(madnessCard);
        if(madnessCount == 0 ) return;
        int sign = Random.Range(0, 2) * 2 - 1;
        madnessModifier += sign * madnessCard.statChange;
        if (sign > 0)
        {
            UIManager._.WriteToNotification("Madness <b>increased</b> your damage by " + madnessCard.statChange * 100 + "%");
        }
        else
        {
            UIManager._.WriteToNotification("Madness <b>decreased</b> your damage by " + madnessCard.statChange * 100 + "%");
        }
    }


    #endregion




    public void OnRoomClear()
    {
        CalculateMadness();
    }

}
