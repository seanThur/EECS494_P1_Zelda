using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour //Why is this a MonoBehaviour? Could it not be an object in PlayerController?
{
    private Queue<Weapon> availableAltWeapons;
    public Bow bow;
    public Boomarang boomerang;//bomb, 
    public int rupeeCount = 0;
    public int bombCount = 0;
    public int keyCount = 0;

    private void Start()
    {

        //bomb = GetComponent<Bomb>();
        //bow = 
        //boomerang = GetComponent<Boomarang>();
    }

    //ACQUIRE != EQUIP
    public void acquireBow()
    {
        bow = GetComponent<Bow>();
        availableAltWeapons.Enqueue(bow);
    }

    public void acquireBoomerang()
    {
        boomerang = GetComponent<Boomarang>();
        Debug.Log("Yo ");
        availableAltWeapons.Enqueue(boomerang);
    }

    /*
    //public void acquireBombs()
    //{
    //    bombs = GetComponent<Bombs>();
    //    availableAltWeapons.Enqueue(bombs);
    //}

    public void acquireWeapon(WeaponType w)
    {
        if(w.Equals(WeaponType.Bow))
        {
            availableAltWeapons.Enqueue(bow);
        }
        else if (w.Equals(WeaponType.Boomerang))
        {
            availableAltWeapons.Enqueue(bow);
        }
        else if (w.Equals(WeaponType.Bomb))
        {
            //availableAltWeapons.Enqueue(bomb);
        }
        else
        {
            Debug.Log("Tried to acquire invalid weapon" + w);
            return;
        }

        
    }*/

    public void toggleAltWeapon()
    {
        if(availableAltWeapons.Count == 0)
        {
            Debug.Log("No alt weapons available!");
            return;
        }

        //move current alt weapon to back of queue
        availableAltWeapons.Enqueue(availableAltWeapons.Dequeue());

        //set new altWeapon
        PlayerController.playerInstance.altWeapon = availableAltWeapons.Peek();
    }

    public void godMode()
    {
        //looked it up, these are the actual limits. my b
        rupeeCount = 255;
        bombCount = 16;
        keyCount = 255;
    }
    



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

        if (rupeeCount < 0 || rupeeCount > 999)
        {
            rupeeCount = 999;
        }
    }


    public void SetRupees(int num)
    {
        if (num < 0 || num > 999)
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
        if (bombCount < 0 || bombCount > 99)
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
        if (GameController.godMode)
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
        if (keyCount <= 0)
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
}
