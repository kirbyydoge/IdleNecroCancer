using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTeleport : Ability {

    public float range;

    public override bool Use(GameObject self, GameObject[] allies, GameObject[] enemies)
    {
        if (enemies == null)
        {
            return false;
        }
        float lowestHP = float.MaxValue;
        GameObject lowerHPUnit = null;
        foreach(GameObject enemy in enemies) {
            Unit currentUnit = enemy.GetComponent<Unit>();
            if (currentUnit.getCurrentHP() < lowestHP) {
                lowestHP = currentUnit.getCurrentHP();
                lowerHPUnit = enemy;
            }
        }
        if (lowerHPUnit)
        {
            self.transform.position = Vector3.MoveTowards(self.transform.position, lowerHPUnit.transform.position, range);
        }
        LateUse();
        return true;
    }

}
