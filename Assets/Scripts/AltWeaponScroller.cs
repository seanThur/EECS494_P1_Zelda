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

    private void Start()
    {
        blank = new Weapon();
        blank.weaponType = WeaponType.Empty;
        boomarang = GetComponent<Boomarang>();
        bow = GetComponent<Bow>();
        bombDropper = GetComponent<BombDropper>();
        currentWeapon = boomarang;
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
        }
    }

    public void scrollAltWeapon()
    {
        Debug.Log("SCROLLING: HAS " + currentWeapon.weaponType);
        switch(currentWeapon.weaponType)
        {
            case WeaponType.Empty:
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
                break;
            case WeaponType.Boomerang:
                if (hasBow())
                {
                    currentWeapon = bow;
                }
                else if (hasBombDropper())
                {
                    currentWeapon = bombDropper;
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
                else
                {
                    currentWeapon = blank;
                }
                break;
            case WeaponType.Bomb:
                currentWeapon = blank;
                break;
        }
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


}
