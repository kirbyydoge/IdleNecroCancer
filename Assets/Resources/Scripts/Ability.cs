using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Ability : MonoBehaviour {

	private float cooldown;
	private float cooldownTimer;

	abstract public bool Use(GameObject self, GameObject[] allies, GameObject[] enemies);

	public void LateUse() {
		cooldownTimer = cooldown;
	}

	public void Tick(float deltaTime) {
		if(cooldownTimer > 0) {
			cooldownTimer -= deltaTime;
		}
		else {
			cooldownTimer = 0;
		}
	}

	public void SetCooldown(float cooldown) {
		this.cooldown = cooldown;
	}

	public bool IsReady() {
		return cooldownTimer == 0;
	}

}
