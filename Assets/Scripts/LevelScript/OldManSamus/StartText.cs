using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartText : MonoBehaviour
{
    bool gone = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(!(gone) && other.CompareTag("Player"))
        {
            gone = true;
            TextSpriteDisplay.next++;
        }
    }
}
