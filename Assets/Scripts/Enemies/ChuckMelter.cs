using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuckMelter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDestroy()
    {
        PlayerController.playerInstance.gameObject.GetComponent<SnowballThrower>().feelinMelty = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Frozen"))
        {
            other.GetComponent<EnemyController>().unfreeze();
            if (other.GetComponent<BoxCollider>().isTrigger == false) {
                other.GetComponent<EnemyController>().unfreeze();
            }
        }
        else if(other.CompareTag("PlayerProjectile"))
        {
            if(other.GetComponent<SnowballController>())
            {
                other.GetComponent<SnowballController>().getMeltedIdiot();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        OnTriggerEnter(other);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
