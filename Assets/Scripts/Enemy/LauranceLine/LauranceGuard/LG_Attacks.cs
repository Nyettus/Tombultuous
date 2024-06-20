using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LG_Attacks : BaseEnemyAttacks
{
    private bool isSpinning = false;
    private EnemyDamage[] spinHitboxes = new EnemyDamage[2];

    private void LateUpdate()
    {
        ResetSpin();
    }

    #region Neutral kick chain
    public void LG_NeutralKick_ON()
    {
        GenericAttack_ON(0, 0);

    }

    public void LG_NeutralKick_OFF()
    {
        GenericAttack_OFF(0);
    }

    public void LG_ShoulderBash_ON()
    {
        GenericAttack_ON(1, 1);
    }

    public void LG_ShoulderBash_OFF()
    {
        GenericAttack_OFF(1);
    }

    #endregion

    #region Clap chain
    public void LG_Clap_ON()
    {
        EnemyDamage[] hitboxes = new EnemyDamage[2];
        hitboxes[0] = GenericAttack_ON(2, 2);
        hitboxes[1] = GenericAttack_ON(3, 2);
        //Reassign knockback duration to be directly up
        var damagePair = damageValues.damageArray[2];
        Vector3 direction = (GameManager._.Master.transform.position - transform.position + Vector3.up * 10).normalized;
        foreach (EnemyDamage hitbox in hitboxes)
        {
            hitbox.AssignValues(damagePair, direction);
        }
    }

    public void LG_Clap_OFF()
    {
        GenericAttack_OFF(2);
        GenericAttack_OFF(3);
    }

    public void LG_ClapFollowup_ON()
    {
        GenericAttack_ON(2, 3);
    }

    public void LG_ClapFollowup_OFF()
    {
        GenericAttack_OFF(2);
    }

    #endregion

    #region drop kick
    public void LG_DropKick_ON()
    {
        var hitbox = GenericAttack_ON(0, 4);
        var damagePair = damageValues.damageArray[4];
        Vector3 direction = (GameManager._.Master.transform.position - transform.position + Vector3.up * 2).normalized;
        hitbox.AssignValues(damagePair, direction);

    }

    public void LG_DropKickPart2_ON()
    {
        var hitbox = GenericAttack_ON(0, 5);
        var damagePair = damageValues.damageArray[5];
        hitbox.AssignValues(damagePair,Vector3.up);
    }

    public void LG_DropKick_OFF()
    {
        GenericAttack_OFF(0);
    }





    #endregion

    #region shpin
    //This one might get a little silly depending on how it works
    public void LG_Spin_ON()
    {
        spinHitboxes = new EnemyDamage[2];
        spinHitboxes[0] = GenericAttack_ON(2, 6);
        spinHitboxes[1] = GenericAttack_ON(3, 6);
        isSpinning = true;
    }
    
    public void LG_Spin_OFF()
    {
        LG_Clap_OFF();
        isSpinning = false;
    }

    private void ResetSpin()
    {
        if (!isSpinning) return;
        foreach(EnemyDamage hitbox in spinHitboxes)
        {
            if (hitbox.canHit) continue;
            hitbox.canHit = true;
        }
    }
    #endregion



}
