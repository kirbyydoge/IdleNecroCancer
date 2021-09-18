using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBattleController : MonoBehaviour
{

    // Teams
    private GameObject[] teamLeft;
    private GameObject[] teamRight;

    void Start()
    {
        teamLeft = GameObject.FindGameObjectsWithTag("Hero");
        teamRight = GameObject.FindGameObjectsWithTag("Monster");
    }

    void RemoveUnitFromBattle(GameObject obj)
    {
        int indexToRemove = -1;
        for (int i = 0; i < teamLeft.Length; i++)
        {
            if (teamLeft[i].Equals(obj))
            {
                indexToRemove = i;
            }
        }
        if (indexToRemove > -1)
        {
            Destroy(teamLeft[indexToRemove]);
            for (int i = indexToRemove; i < teamLeft.Length; i++)
            {
                teamLeft[indexToRemove] = teamLeft[indexToRemove + 1];
            }
            return;
        }
        for (int i = 0; i < teamRight.Length; i++)
        {
            if (teamRight[i].Equals(obj))
            {
                indexToRemove = i;
            }
        }
        if (indexToRemove > -1)
        {
            Destroy(teamRight[indexToRemove]);
            for (int i = indexToRemove; i < teamRight.Length; i++)
            {
                teamRight[indexToRemove] = teamRight[indexToRemove + 1];
            }
        }
    }

    public void DamageUnit(GameObject defender, float damage)
    {
        Unit unit = defender.GetComponent<Unit>();
        unit.TakeDamage(damage);
        if (unit.getCurrentHP() == 0)
        {
            RemoveUnitFromBattle(defender);
        }
    }

}
