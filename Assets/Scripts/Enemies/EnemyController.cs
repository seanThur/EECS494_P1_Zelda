using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float contactDamage = 0.0f;
    public Animator an;
    private Health health;
    public bool diesOnBoomerangHit = false;
    // Start is called before the first frame update
    void Start()
    {
        an = GetComponent<Animator>();
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

    IEnumerator bringOutYourDead(float x)
    {
        yield return (new WaitForSeconds(x));

        Destroy(gameObject);
    }

    void die()
    {
        an.SetTrigger("dead");
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        StartCoroutine(bringOutYourDead(4.0f/6.0f));
    }


    public void takeDamage(float damage)
    {
        an.SetTrigger("OhOuchOuchyOffYouch");
        health.takeDamage(damage);
        if(health.hearts <= 0.0f)
        {
            die();
        }
        joltFromPlayer();
    }

    void joltFromPlayer()
    {
        if(GetComponent<MoveOnGrid>())
        {
            GetComponent<MoveOnGrid>().enemyJolt(transform.position - PlayerController.playerInstance.transform.position);
        }
    }

    public void boomerangHit()
    {
        if (diesOnBoomerangHit)
            Destroy(gameObject);


        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        StartCoroutine(unboomerang());
    }

    IEnumerator unboomerang()
    {
        yield return (new WaitForSeconds(3.5f));
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }
}
