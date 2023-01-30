using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDropper : Weapon
{
    public GameObject bomb;
    //WeaponType weaponType = WeaponType.Bow;

    public void Use(int dir)
    {
        if (PlayerController.playerInstance.inventory.GetBombs() <= 0)
        {
            return;
        }
        PlayerController.playerInstance.inventory.SetBombs(PlayerController.playerInstance.inventory.GetBombs() - 1);
        GameObject temp = Instantiate(bomb, transform.position, Quaternion.identity);
        switch (dir)
        {
            case 1:
                temp.transform.position += Vector3.up;
                break;
            case 2:
                temp.transform.position += Vector3.right;
                break;
            case 3:
                temp.transform.position += Vector3.down;
                break;
            case 4:
                temp.transform.position += Vector3.left;
                break;
        }
    }
}
