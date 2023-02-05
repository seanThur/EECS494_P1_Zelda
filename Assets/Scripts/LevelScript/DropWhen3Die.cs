using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWhen3Die : MonoBehaviour
{
    public GameObject one;
    public GameObject two;
    public GameObject three;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!(one) && !(two) && !(three))
        {
            GetComponent<BoxCollider>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
