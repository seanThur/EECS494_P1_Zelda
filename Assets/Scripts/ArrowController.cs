using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float damage = 1.0f;
    public bool blowTheHellUp = false;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(blowTheHellUp)
        {
            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().takeDamage(damage);
            Explosion();
        }
    }

    void Explosion()
    {
        rb.velocity = Vector3.zero;
        float speed = 3.0f;
        Destroy(gameObject);
    }
}
