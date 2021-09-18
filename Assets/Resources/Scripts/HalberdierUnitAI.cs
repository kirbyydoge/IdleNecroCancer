using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalberdierUnitAI : Unit
{

    public override void Act(GameObject[] allies, GameObject[] enemies)
    {
        Attack(allies, enemies);
    }

    public override void Attack(GameObject[] allies, GameObject[] enemies)
    {
        GameObject target = null;
        Vector3 curPos = gameObject.transform.position;
        Vector3 targetPos = Vector3.zero;
        float minDistance = float.PositiveInfinity;
        foreach (GameObject enemy in enemies)
        {
            Vector3 enemyPos = enemy.transform.position;
            float enemyDistance = (enemyPos - curPos).magnitude;
            if (enemyDistance < minDistance)
            {
                targetPos = enemyPos;
                minDistance = enemyDistance;
                target = enemy;
            }
        }
        Vector3 intendedPos = Vector3.MoveTowards(curPos, targetPos, movementSpeed * Time.deltaTime);
        bool isAttacking = false;
        if (minDistance <= attackRange)
        {
            isAttacking = true;
        }
        spriteAnimator.SetBool("IsAttacking", isAttacking);
    }

    public override IEnumerator AutoAttack(GameObject enemy)
    {
        throw new System.NotImplementedException();
    }

    public override void Move(GameObject[] allies, GameObject[] enemies)
    {
        Vector3 curPos = gameObject.transform.position;
        Vector3 targetPos = Vector3.zero;
        float minDistance = float.PositiveInfinity;
        foreach (GameObject enemy in enemies)
        {
            Vector3 enemyPos = enemy.transform.position;
            float enemyDistance = (enemyPos - curPos).magnitude;
            if (enemyDistance < minDistance)
            {
                targetPos = enemyPos;
                minDistance = enemyDistance;
            }
        }
        Vector3 intendedPos = Vector3.MoveTowards(curPos, targetPos, movementSpeed * Time.deltaTime);
        bool isWalking = false;
        if (minDistance > attackRange * attackExtension)
        {
            isWalking = movementController.MoveTo(gameObject, intendedPos);
        }
        spriteAnimator.SetBool("IsWalking", isWalking);
    }
}
