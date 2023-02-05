using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDropBox : MonoBehaviour
{
    public static enemyDropBox instance;

    public GameObject heartPickup;
    public GameObject rupeePickup;
    public GameObject bombPickup;
    // Start is called before the first frame update
    void Start()
    {
        if(instance)
        {
            Destroy(this);
        } else
        {
            instance = this;
        }
    }
}
