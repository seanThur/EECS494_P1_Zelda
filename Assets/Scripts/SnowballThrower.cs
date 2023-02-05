using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballThrower : Weapon
{
    public GameObject snowball;
    // Start is called before the first frame update
    new public void Use(int dir)
    {
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
}
