using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomarangController : MonoBehaviour
{
    public float speed = 5.0f;
    public Rigidbody rb;
    private bool reversing = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddTorque(new Vector3(0, 0, 600.0f));
        StartCoroutine(limitLength());
    }

    // Update is called once per frame
    void Update()
    {
        if(reversing)
        {
            rb.velocity = speed * ((PlayerController.playerInstance.transform.position - transform.position).normalized);
        }
    }

    public void throwInDir(Vector3 heading)
    {
        rb.velocity = heading * speed;
    }

    private void reverse()
    {
        if (!(reversing))
        {
            rb.velocity = rb.velocity * -1.0f;
            reversing = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            reverse();
        }
        if(other.CompareTag("Player") && reversing)
        {
            Destroy(gameObject);
        }
        if(other.CompareTag("Enemy"))
        {
            reverse();
            other.GetComponent<EnemyController>().boomerangHit();
        }
    }

    IEnumerator limitLength()
    {
        yield return (new WaitForSeconds(2.0f));

        reverse();
    }
}
