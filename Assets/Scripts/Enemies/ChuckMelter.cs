using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuckMelter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Frozen"))
        {
            other.GetComponent<EnemyController>().unfreeze();
        }
        else if(other.CompareTag("PlayerProjectile"))
        {
            if(other.GetComponent<SnowballController>())
            {
                Destroy(other.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
