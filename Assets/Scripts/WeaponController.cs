using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class WeaponController : MonoBehaviour {
    Weapon equippedWeapon;

    private void Awake() {
        equippedWeapon = GetComponent<Weapon>();
    }

    public void OnTriggerHold() {
        // Potential to add a charge here?
    }

    public void Aim(Vector2 aimPoint) {
        if (equippedWeapon) {
            equippedWeapon.Aim(aimPoint);
        }
    }
}
