using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Sword, Bow, Bomb, Boomerang, Empty };

public class Weapon : MonoBehaviour
{
    public WeaponType weaponType;
    public bool equipped = false;

    void Equip()
    {
        equipped = true;
    }

    void Unequip()
    {
        equipped = false;
    }

    public void Use(int dir)
    {
        Debug.Log("Base weapon Use Called");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Sword : Weapon
{
    
}

public class Bomb : Weapon
{
    WeaponType weaponType = WeaponType.Bomb;
}

public class Boomerang : Weapon
{
    WeaponType weaponType = WeaponType.Boomerang;
}