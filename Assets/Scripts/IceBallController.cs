using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBallController : MonoBehaviour
{
    Rigidbody rb;
    Animator an;
    public float speed = 15.0f;
    public bool markedToDie = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        an = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(markedToDie)
        {
            Destroy(gameObject);
        }
    }

    public void shatter()
    {
        an.SetTrigger("Shatter");
        rb.velocity = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!(other.isTrigger) && other.CompareTag("Water") == false)// || other.gameObject.GetComponent<ChuckMelter>())
        {
            shatter();
        }
        if(other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().die();
        }

    }

    public void launch(Vector3 heading)
    {
        if (!(rb))
        {
            Start();
        }
        rb.velocity = heading * speed;
    }
}
