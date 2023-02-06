using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLevelDrop : MonoBehaviour
{
    public Sprite icePNG;
    public static CustomLevelDrop instance;
    // Start is called before the first frame update
    void Start()
    {
        if(!(instance))
        {
            instance = this;
        } 
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
