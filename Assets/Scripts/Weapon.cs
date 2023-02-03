using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Sword, Bow, Bomb, Boomerang, Empty };

public class Weapon : MonoBehaviour
{
    public WeaponType weaponType = WeaponType.Empty;
    public bool equipped = false;

    void Equip()
    {
        equipped = true;
        PlayerController.playerInstance.inventory.altWeapon = weaponType;
    }

    void Unequip()
    {
        equipped = false;
    }

    public void Use(int dir)
    {
        Debug.Log("Base weapon Use Called");
    }
}
