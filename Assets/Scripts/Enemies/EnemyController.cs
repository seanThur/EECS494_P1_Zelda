using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float contactDamage = 0.0f;
    private Health health;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health.hearts <= 0.05f)
        {
            die();
        }
    }

    void die()
    {
        Destroy(gameObject);
    }


    public void takeDamage(float damage)
    {
        health.takeDamage(damage);
        if(health.hearts <= 0.0f)
        {
            die();
        }
        joltFromPlayer();
    }

    void joltFromPlayer()
    {

    }
}
