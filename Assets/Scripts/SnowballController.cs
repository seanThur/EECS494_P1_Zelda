using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballController : MonoBehaviour
{
   
    Rigidbody rb;
    public float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().freeze();
            Destroy(gameObject);
        }
        else if(other.CompareTag("wall"))
        {
            Destroy(gameObject);
        }

        if(!(other.isTrigger))
        {
            if(!(other.CompareTag("Player")) && !(other.CompareTag("Frozen")))
            {
                Destroy(gameObject);
            }
        }
    }

    public void throwInDir(Vector3 v)
    {
        if(!(rb))
        {
            rb = GetComponent<Rigidbody>();
        }
        rb.velocity = v * speed;
    }
}
