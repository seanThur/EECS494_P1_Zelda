using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    public GameObject arrow;
    WeaponType weaponType = WeaponType.Bow;

    public void Use(int dir)
    {
        if (PlayerController.playerInstance.inventory.GetRupees() <= 0)
        {
            return;
        }
        PlayerController.playerInstance.inventory.SetRupees(PlayerController.playerInstance.inventory.GetRupees()-1);
        GameObject temp = Instantiate(arrow, transform.position, Quaternion.identity);
        temp.GetComponent<ArrowController>().damage = 1.0f;
        temp.GetComponent<Animator>().SetInteger("dir", dir);
        float speed = 6.0f;
        switch (dir)
        {
            case 1:
                temp.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 1.0f, 0.0f) * speed;
                break;
            case 2:
                temp.GetComponent<Rigidbody>().velocity = new Vector3(1.0f, 0.0f, 0.0f) * speed;
                break;
            case 3:
                temp.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, -1.0f, 0.0f) * speed;
                break;
            case 4:
                temp.GetComponent<Rigidbody>().velocity = new Vector3(-1.0f, 0.0f, 0.0f) * speed;
                break;
        }
    }
}
