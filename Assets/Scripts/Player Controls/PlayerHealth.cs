using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    public ItemMaster itemMaster;
    public DeathManager deathMaster;
    public int lastDamageInstance;
    public int flesh;
    public int fleshHealthMax => itemMaster.Master.health + itemMaster.M_Health;
    public int totalHealth => flesh + itemMaster.M_OverHealth + itemMaster.M_DecayHealth;

    // Start is called before the first frame update
    void Start()
    {
        ResetFlesh();
        OnHealthChangeEvent();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HealthDecay();
    }

    public delegate void UpdateHealth();
    public static event UpdateHealth OnUpdateHealth;
    public void OnHealthChangeEvent()
    {

        if (OnUpdateHealth != null)
            OnUpdateHealth();
    }



    #region Damage

    public void takeDamage(int amount, Vector3 direction, float magnitude)
    {
        if (GameManager._.Master.invuln)
        {
            lastDamageInstance = amount;
            return;
        }

        if (itemMaster.M_OverHealth + itemMaster.M_DecayHealth > 0)
        {

            //Damage decay health
            int overflow = DamageStep(amount, ref itemMaster.M_DecayHealth);
            //leftovers damage overhealth
            overflow = DamageStep(overflow, ref itemMaster.M_OverHealth);
            //remaining goes into flesh health
            flesh -= overflow;
        }
        else
            flesh -= amount;


        if (flesh <= 0)
        {
            flesh = 0;
            Death();
        }
        float shakeAmount = Mathf.Clamp(amount*0.5f, 0, 5);
        GameManager._.Master.cameraEffects.DashShake(shakeAmount);
        GameManager._.Master.movementMaster.KnockBack(direction, magnitude);
        OnHealthChangeEvent();

    }

    public int DamageStep(int amount, ref int healthpool)
    {
        healthpool -= amount;
        if (healthpool <= 0)
        {
            int temp = Mathf.Abs(healthpool);
            healthpool = 0;
            return temp;
        }
        else
            return 0;
    }
    public void Death()
    {
        deathMaster.Initialise();
    }
    #endregion

    #region Heal Health
    public void HealFlesh(int amount)
    {
        if (fleshHealthMax - flesh < amount)
            flesh = fleshHealthMax;
        else
            flesh += amount;
        OnHealthChangeEvent();
    }
    public void ResetFlesh()
    {
        flesh = fleshHealthMax;
        itemMaster.MIN_Health = -itemMaster.Master.health+1;
    }

    #endregion

    #region Decay Health
    float decayTime = 0;
    float decayDelay = 0.2f;
    private void HealthDecay()
    {

        if (itemMaster.M_DecayHealth > 0 && Time.time > decayTime)
        {
            decayTime = Time.time + decayDelay;
            itemMaster.M_DecayHealth -= 1;
            OnHealthChangeEvent();
        }

    }
    #endregion


    #region Healing Pot
    public int healingCharges
    {
        get { return GameManager._.healingCharges; }
        set { GameManager._.healingCharges = value; }
    }
    public int healingChargeAmount = 50;
    public void UseHealingPot(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (healingCharges > 0 && flesh != fleshHealthMax)
        {
            HealFlesh(healingChargeAmount);
            healingCharges--;
        }
        UIManager._.ChangeHealthPot();

    }

    #endregion

}
