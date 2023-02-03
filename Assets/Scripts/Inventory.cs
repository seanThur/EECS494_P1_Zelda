using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour //Why is this a MonoBehaviour? Could it not be an object in PlayerController?
{
    //Queue<WeaponType> activeAltWeapons;
    public WeaponType altWeapon;
    public int rupeeCount = 0;
    public int bombCount = 0;
    public int keyCount = 0;

    public int GetRupees()
    {
        return rupeeCount;
    }

    public void AddRupees(int num)
    {
        //prevents losing rupees
        if (GameController.godMode)
        {
            return;
        }

        rupeeCount += num;

        if(rupeeCount < 0 || rupeeCount > 999)
        {
            rupeeCount = 999;
        }
    }

    
    public void SetRupees(int num)
    {
        if(num < 0 || num > 999)
        {
            num = 999;
        }

        rupeeCount = num;
    }

    public int GetBombs()
    {
        return bombCount;
    }

    public void AddBombs(int num)
    {
        if (GameController.godMode)
        {
            return;
        }
        bombCount += num;
        if(bombCount < 0 || bombCount > 99)
        {
            bombCount = 99;
        }
    }


    public void SetBombs(int num)
    {
        if (num < 0 || num > 99)
        {
            num = 99;
        }

        bombCount = num;
    }

    public void addKeys(int num)
    {
        if(GameController.godMode)
        {
            return;
        }
        keyCount += num;
        if (keyCount < 0 || keyCount > 99)
        {
            keyCount = 99;
        }
    }

    public void useKey()
    {
        if(keyCount <= 0)
        {
            Debug.Log("Invalid key count: " + keyCount);
        }
        else
        {
            keyCount--;
        }
    }

    public void setKeys(int num)
    {
        if (num < 0 || num > 99)
        {
            num = 99;
        }

        keyCount = num;
    }

    public void setAltWeapon(WeaponType w)
    {
        altWeapon = w;
    }

    public void godMode()
    {
        rupeeCount = 999;
        bombCount = 99;
        keyCount = 99;
    }

}
