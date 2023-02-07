using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballThrower : Weapon
{
    public GameObject snowball;
    bool feelinMelty = false;
    // Start is called before the first frame update
    private void Start()
    {
        weaponType = WeaponType.Snowball;
    }
    new public void Use(int dir)
    {
        if (feelinMelty)
            return;
        GameObject temp = Instantiate(snowball, transform.position, Quaternion.identity);
        SnowballController bc = temp.GetComponent<SnowballController>();
        switch (dir)
        {
            case 1:
                bc.throwInDir(new Vector3(0.0f, 1.0f, 0.0f));
                break;
            case 2:
                bc.throwInDir(new Vector3(1.0f, 0.0f, 0.0f));
                break;
            case 3:
                bc.throwInDir(new Vector3(0.0f, -1.0f, 0.0f));
                break;
            case 4:
                bc.throwInDir(new Vector3(-1.0f, 0.0f, 0.0f));
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ChuckMelter"))
        {
            feelinMelty = true;
        }
        else
        {
            feelinMelty = false;
        }
    }
}
