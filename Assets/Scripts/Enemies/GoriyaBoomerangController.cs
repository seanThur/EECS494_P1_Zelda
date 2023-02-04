using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoriyaBoomerangController : EnemyProjectile
{
    public Rigidbody rb;
    public float timeout;
    public float speed;
    private bool returning;
    // Start is called before the first frame update
    void Start()
    {
        returning = false;
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddTorque(new Vector3(0, 0, 600.0f));
    }

    IEnumerator autoReturn()
    {
        yield return (new WaitForSeconds(timeout));
        reverse();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(rb.velocity * -1.0f * Time.deltaTime);
    }

    public void launch(Vector3 heading)
    {
        Debug.Log("Launching");
        Debug.Log("Heading = " + heading.ToString());
        Debug.Log("RB = " + rb.ToString());
        rb.velocity = heading * speed;

    }
    void reverse()
    {
        if(!(returning))
        {
            returning = true;
            rb.velocity = rb.velocity * -1.0f;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(returning && other.GetComponent<GoriyaMovement>())
        {
            Debug.Log("Returned to Goriya");
            other.GetComponent<GoriyaMovement>().boomerangReset();
            Destroy(gameObject);
        }
        else
        {
            if(!(other.isTrigger))
            {
                reverse();
            }
        }

        if(other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(0.5f);
        }
    }

}
