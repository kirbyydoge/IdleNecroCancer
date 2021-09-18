using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Unit : MonoBehaviour {

    [Header("Unit Abilities")]
    public Ability ability;

    //Unit Stats
    [Header("Unit Stats")]
    public float maxHP;
    public float movementSpeed;
    public float haste;
    public float baseAbilityCooldown;
    public float baseDamage;
    public float attackRange;
    public float attackExtension;

    //Unit State
    private float currentHP;
    private float currentMP;
    private bool canAct;

    //Team State
    private GameObject[] allies;
    private GameObject[] enemies;

    //Battle Controllers
    [Header("Battle Handler")]
    public GameObject battleHandler;
    [HideInInspector]
    public UnitMovementController movementController;
    [HideInInspector]
    public UnitBattleController battleController;
    [HideInInspector]
    public Animator spriteAnimator;

    protected virtual void Start()
    {
        baseAbilityCooldown = 1f;
        currentHP = maxHP;
        canAct = true;
        movementController = battleHandler.GetComponent<UnitMovementController>();
        battleController = battleHandler.GetComponent<UnitBattleController>();
        spriteAnimator = gameObject.GetComponent<Animator>();
        AbilitySetup();
    }

    //Overridable AI methods
    public abstract void Move(GameObject[] allies, GameObject[] enemies);

    public abstract void Act(GameObject[] allies, GameObject[] enemies);

    public abstract void Attack(GameObject[] allies, GameObject[] enemies);

    public abstract IEnumerator AutoAttack(GameObject enemy);

    public virtual void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(0, currentHP);
    }

    public virtual void Heal(float heal)
    {
        currentHP += heal;
        currentHP = Mathf.Max(maxHP, currentHP);
    }

    protected virtual void AbilitySetup()
    {
        ability = gameObject.AddComponent<AbilityFireball>();
        ability.SetCooldown(EffectiveCooldown(baseAbilityCooldown));
    }

    protected virtual float EffectiveCooldown(float value)
    {
        return value * 100 / (haste + 100);
    }

    //Getters Setters
    public float getCurrentHP()
    {
        return this.currentHP;
    }

    public void setCurrentHP(float value)
    {
        this.currentHP = value;
    }
    public float getCurrentMP()
    {
        return this.currentMP;
    }

    public void setCurrentMP(float value)
    {
        this.currentMP = value;
    }

    public bool CanAct()
    {
        return canAct;
    }

    private GameObject[] RemoveSelf(GameObject self, GameObject[] array)
    {
        if (array.Length == 1) {
            return null;
        }
        GameObject[] result = new GameObject[array.Length - 1];
        int counter = 0;
        for (int i = 0; i < array.Length; i++) {
            if (self != array[i]) {
                result[counter++] = array[i];
            }
        }
        return result;
    }

}

