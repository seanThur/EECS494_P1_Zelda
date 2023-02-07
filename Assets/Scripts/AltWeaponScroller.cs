using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltWeaponScroller : MonoBehaviour
{
    private Boomarang boomarang;
    private Bow bow;
    private BombDropper bombDropper;
    private Weapon blank;
    public Weapon currentWeapon;

    public bool customMode = false;
    private SnowballThrower sbt;

    private void Start()
    {
        blank = new Weapon();
        blank.weaponType = WeaponType.Empty;
        boomarang = GetComponent<Boomarang>();
        bow = GetComponent<Bow>();
        bombDropper = GetComponent<BombDropper>();
        if(GetComponent<SnowballThrower>())
        {
            customMode = true;
            sbt = GetComponent<SnowballThrower>();
        }
        currentWeapon = boomarang;
        Debug.Log("Am I in a custom level? " + customMode);
        currentWeapon = bombDropper;
        Displayer.instance.displayAltWeapon();
    }

    public void useItem()
    {
        switch (currentWeapon.weaponType)
        {
            case WeaponType.Boomerang:
                boomarang.Use(GetComponent<InputToAnimator>().lastDirection);
                break;
            case WeaponType.Bow:
                bow.Use(GetComponent<InputToAnimator>().lastDirection);
                break;
            case WeaponType.Bomb:
                bombDropper.Use(GetComponent<InputToAnimator>().lastDirection);
                break;
            case WeaponType.Snowball:
                sbt.Use(GetComponent<InputToAnimator>().lastDirection);
                break;
        }
    }

    public void scrollAltWeapon()
    {
        Debug.Log("SCROLLING: HAS " + currentWeapon.weaponType);
        switch(currentWeapon.weaponType)
        {
            
            case WeaponType.Boomerang:
                if (hasBow())
                {
                    currentWeapon = bow;
                }
                else if (hasBombDropper())
                {
                    currentWeapon = bombDropper;
                }
                else if (hasSnowball())
                {
                    currentWeapon = sbt;
                }
                else
                {
                    currentWeapon = blank;
                }
                break;
            case WeaponType.Bow:
                if (hasBombDropper())
                {
                    currentWeapon = bombDropper;
                }
                else if (hasSnowball())
                {
                    currentWeapon = sbt;
                }
                else
                {
                    currentWeapon = blank;
                }
                break;
            case WeaponType.Bomb:
                if (hasSnowball())
                {
                    currentWeapon = sbt;
                }
                else
                {
                    currentWeapon = blank;
                }
                break;
            case WeaponType.Snowball:
                currentWeapon = blank;
                break;
            default:
                if (hasBoomerang())
                {
                    currentWeapon = boomarang;
                }
                else if (hasBow())
                {
                    currentWeapon = bow;
                }
                else if (hasBombDropper())
                {
                    currentWeapon = bombDropper;
                }
                else if (hasSnowball())
                {
                    currentWeapon = sbt;
                }
                else
                {
                    currentWeapon = blank;
                }
                break;
        }
        Debug.Log("Weapon = " + currentWeapon.weaponType);
        Displayer.instance.displayAltWeapon();
    }

    public bool hasBoomerang()
    {
        return (boomarang.equipped);
    }

    public bool hasBow()
    {
        return (bow.equipped);
    }

    public bool hasBombDropper()
    {
        return (bombDropper.equipped);
    }

    public bool hasSnowball()
    {
        if(!(customMode))
        {
            return (false);
        }
        return (sbt.equipped);
    }

    //give link all weapons (for debug)
    public void godMode()
    {
        boomarang.equipped = true;
        bow.equipped = true;
        bombDropper.equipped = true;

        if(customMode)
        {
            sbt.equipped = true;
        }
    }
}
