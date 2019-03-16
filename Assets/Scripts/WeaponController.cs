using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class WeaponController : MonoBehaviour {
    Weapon equippedWeapon;

    private void Awake() {
        equippedWeapon = GetComponent<Weapon>();
    }

    public void Attack(Vector2 facing) {
        // Potential to do charged holddown attacks as well as regular attacks
        equippedWeapon.Attack(facing);
    }

    public void Aim(Vector2 aimPoint) {
        if (equippedWeapon) {
            equippedWeapon.Aim(aimPoint);
        }
    }
}
