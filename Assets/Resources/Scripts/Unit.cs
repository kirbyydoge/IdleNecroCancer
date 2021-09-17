using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    [Header("Unit Abilities")]
    protected Ability ability;

    //Unit Stats
    public float maxHP;
    public float maxMP;
    public float movementSpeed;
    public float attackSpeed;
    public float abilityCooldown;

    //Unit State
    private float currentHP;
    private float currentMP;

    //Team State
    private GameObject[] allies;
    private GameObject[] enemies;

    protected virtual void Start() {
        currentHP = maxHP;
        currentMP = maxMP;
        TeamSetup();
        AbilitySetup();
    }

    protected void Update() {
        TeamSetup();    // Implement better teams later
        Move();
        Act();
    }

    protected virtual void Move() { 
        
    }

    protected virtual void Act()
    {
        if (ability.IsReady())
        {
            ability.Use(gameObject, null, enemies);
        }
        else
        {
            ability.Tick(Time.deltaTime);
        }
    }

    protected virtual void TeamSetup()
    {
        GameObject[] heroes = GameObject.FindGameObjectsWithTag("Hero");
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        if (gameObject.tag == "Hero")
        {
            allies = RemoveSelf(gameObject, heroes);
            enemies = monsters;
        }
        else
        {
            allies = RemoveSelf(gameObject, monsters);
            enemies = heroes;
        }
    }

    protected virtual void AbilitySetup() {
        ability = gameObject.AddComponent<AbilityFireball>();
        ability.SetCooldown(abilityCooldown);
    }


    public void TakeDamage(float damage) {
        currentHP -= damage;
        currentHP = Mathf.Max(0, currentHP);
        if (currentHP == 0) {
            Destroy(gameObject);    
        }
    }

    public void Heal(float heal) {
        currentHP += heal;
        currentHP = Mathf.Max(maxHP, currentHP);
    }

    //Getters Setters
    public float getCurrentHP() {
        return this.currentHP;
    }

    public void setCurrentHP(float value) {
        this.currentHP = value;
    }
    public float getCurrentMP() {
        return this.currentMP;
    }

    public void setCurrentMP(float value) {
        this.currentMP = value;
    }

    private GameObject[] RemoveSelf(GameObject self, GameObject[] array) {
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

