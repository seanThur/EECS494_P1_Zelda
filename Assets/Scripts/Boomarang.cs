using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomarang : Weapon
{
    public GameObject rang;

    new public void Use(int dir)
    {
        GameObject temp = Instantiate(rang, transform.position, Quaternion.identity);
        BoomarangController bc = temp.GetComponent<BoomarangController>();
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
