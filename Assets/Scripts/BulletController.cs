using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float damage = 1.0f;
    public bool blowTheHellUp = false;
    private Rigidbody rb;
    public GameObject upLeft;
    public GameObject downLeft;
    public GameObject upRight;
    public GameObject downRight;

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
        Instantiate(upLeft, transform.position, Quaternion.identity).GetComponent<Rigidbody>().velocity = new Vector3(-1.0f,1.0f,0.0f)*speed;
        Instantiate(downLeft, transform.position, Quaternion.identity).GetComponent<Rigidbody>().velocity = new Vector3(-1.0f, -1.0f, 0.0f) * speed;
        Instantiate(upRight, transform.position, Quaternion.identity).GetComponent<Rigidbody>().velocity = new Vector3(1.0f, 1.0f, 0.0f) * speed;
        Instantiate(downRight, transform.position, Quaternion.identity).GetComponent<Rigidbody>().velocity = new Vector3(1.0f, -1.0f, 0.0f) * speed;
        Destroy(gameObject);
    }
}
