using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomDropper : MonoBehaviour
{
    public Sprite iceSprite;
    public static CustomDropper instance;
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
