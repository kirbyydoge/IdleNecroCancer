using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityFireball : Ability
{

    private GameObject fireballPrefab;
    private float speed;
    private float damage;

    void Start()
    {
        fireballPrefab = (GameObject)Resources.Load("Prefabs/Fireball", typeof(GameObject));
    }

    public override bool Use(GameObject self, GameObject[] allies, GameObject[] enemies)
    {
        if (enemies == null)
        {
            return false;
        }
        float lowestHP = float.MaxValue;
        GameObject lowerHPUnit = null;
        foreach (GameObject enemy in enemies)
        {
            Unit currentUnit = enemy.GetComponent<Unit>();
            if (currentUnit.getCurrentHP() < lowestHP)
            {
                lowestHP = currentUnit.getCurrentHP();
                lowerHPUnit = enemy;
            }
        }
        GameObject fireball = Instantiate(fireballPrefab, self.transform.position, Quaternion.identity);
        fireball.GetComponent<FireballAI>().skillSetup(speed, damage);
        fireball.GetComponent<FireballAI>().setTarget(lowerHPUnit);
        LateUse();
        return true;
    }

    public void AbilitySetup(float speed, float damage) {
        this.speed = speed;
        this.damage = damage;
    }

}
