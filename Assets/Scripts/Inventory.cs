using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    public float hearts = 3.0f;
    private Weapon activeWeaponA;
    private Weapon activeWeaponB;
    private int rupeeCount = 0;
    private int bombCount = 0;
    private int keyCount = 0;

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

    public int GetRupees()
    {
        return rupeeCount;
    }

    public void SetRupees(int num)
    {
        if(num < 0 || num > 999)
        {
            num = 999;
        }

        rupeeCount = num;
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

    public int GetBombs()
    {
        return bombCount;
    }

    public void SetBombs(int num)
    {
        if (num < 0 || num > 99)
        {
            num = 99;
        }

        bombCount = num;
    }

    public void AddKeys(int num)
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

    public int GetKeys()
    {
        return keyCount;
    }

    public void SetKeys(int num)
    {
        if (num < 0 || num > 99)
        {
            num = 99;
        }

        keyCount = num;
    }

    public Weapon GetActiveWeaponA()
    {
        return activeWeaponA;
    }

    public Weapon GetActiveWeaponB()
    {
        return activeWeaponB;
    }

    public void SetActiveWeaponA(Weapon w)
    {
        activeWeaponA = w;
    }

    public void SetActiveWeaponB(Weapon w)
    {
        activeWeaponB = w;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetActiveWeaponA(new Sword());
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void GodMode()
    {
        hearts = 3;
        rupeeCount = 999;
        bombCount = 99;
        keyCount = 99;
    }
}
