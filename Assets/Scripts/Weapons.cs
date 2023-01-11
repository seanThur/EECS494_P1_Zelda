using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public GameObject weapon;
    public bool equipped = false;

    void Equip()
    {
        equipped = true;
    }

    void Unequip()
    {
        equipped = false;
    }

    void Use()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        //weapon = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Sword : Weapons
{
    void Start()
    {
       
    }
}