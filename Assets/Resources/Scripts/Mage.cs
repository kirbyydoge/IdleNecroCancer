using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Unit
{

    [Header("Power Setup")]
    public float fireballSpeed;
    public float fireballDamage;

    public override void Act(GameObject[] allies, GameObject[] enemies)
    {
        throw new System.NotImplementedException();
    }

    public override void Attack(GameObject[] allies, GameObject[] enemies)
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator AutoAttack(GameObject enemy)
    {
        throw new System.NotImplementedException();
    }

    public override void Move(GameObject[] allies, GameObject[] enemies)
    { 
        
    }

    protected override void AbilitySetup()
    {
        ability = gameObject.AddComponent<AbilityFireball>();
        ability.SetCooldown(EffectiveCooldown(baseAbilityCooldown));
        ((AbilityFireball)ability).AbilitySetup(fireballSpeed, fireballDamage);
    }

}
