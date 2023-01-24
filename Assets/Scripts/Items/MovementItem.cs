using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementItem : ItemCore
{
    public BasicStatChange card;
    // Start is called before the first frame update
    public override void PickupItem()
    {
        base.PickupItem();
        GameManager.instance.Master.movementMaster.GetBuff(card.statRef, card.statChange);
    }

    protected override void Start()
    {
        base.Start();
        itemName.text = card.itemName;
        itemDesc.text = card.itemDesc;

    }

    protected override void Update()
    {
        base.Update();
    }


}
