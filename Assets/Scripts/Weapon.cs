using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Sword, Bow, Bomb, Boomerang, Empty };

public class Weapon : MonoBehaviour //Why is this a monobeviour?
{
    public Weapon weapon; //What purpose does this serve?
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

    void Use()
    {

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

public class Bow : Weapon
{
     WeaponType weaponType = WeaponType.Bow;
}

public class Bomb : Weapon
{
    WeaponType weaponType = WeaponType.Bomb;
}

public class Boomerang : Weapon
{
    WeaponType weaponType = WeaponType.Boomerang;
}