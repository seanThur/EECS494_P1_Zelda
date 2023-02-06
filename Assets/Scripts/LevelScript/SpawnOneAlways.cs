using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOneAlways : MonoBehaviour
{
    public GameObject guy;
    private GameObject currentGuy;
    // Start is called before the first frame update
    void Start()
    {
        currentGuy = Instantiate(guy);
        currentGuy.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!(currentGuy))
        {
            currentGuy = Instantiate(guy);
            currentGuy.transform.position = transform.position;
        }
    }
}
