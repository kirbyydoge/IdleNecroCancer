using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAI : MonoBehaviour
{
    private Unit target;
    private Vector3 targetPos;
    private bool targetValid;
    private float movementSpeed;
    private float damage;

    void Awake()
    {
        targetPos = Vector3.zero;
        targetValid = false;
        movementSpeed = 5f;
    }

    void Update()
    {
        if (!targetValid)
        {
            return;
        }
        Vector3 curPos = gameObject.transform.position;
        if ((curPos - targetPos).magnitude < 0.5f)
        {
            try
            {
                target.TakeDamage(damage);
            }
            catch (System.Exception e)
            { 
                // Target died.
            }
            Destroy(gameObject);
        }
        gameObject.transform.position = Vector3.MoveTowards(curPos, targetPos, movementSpeed * Time.deltaTime);
    }

    public void SkillSetup(float movementSpeed, float damage)
    {
        this.movementSpeed = movementSpeed;
        this.damage = damage;
    }

    public void SetTarget(GameObject target)
    {
        try
        {
            this.target = target.GetComponent<Unit>();
            this.targetPos = target.transform.position;
            targetValid = true;
        }
        catch (System.Exception e) // Target already died
        {    
            Destroy(gameObject);
        }
    }
}
