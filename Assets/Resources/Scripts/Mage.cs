using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Unit
{

    [Header("Power Setup")]
    public float fireballSpeed;
    public float fireballDamage;

    protected override void Move() { 
    
    }

    protected override void AbilitySetup()
    {
        ability = gameObject.AddComponent<AbilityFireball>();
        ability.SetCooldown(abilityCooldown);
        ((AbilityFireball)ability).AbilitySetup(fireballSpeed, fireballDamage);
    }

}
