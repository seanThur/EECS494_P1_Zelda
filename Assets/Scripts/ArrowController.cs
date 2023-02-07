using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float damage = 1.0f;
    public bool blowTheHellUp = false;
    private Rigidbody rb;
    static bool iExist;
    private bool iCount = false;

    // Start is called before the first frame update
    void Start()
    {
        if (iExist)
        {
            PlayerController.playerInstance.gameObject.GetComponent<Inventory>().AddRupees(1);//Dont judge me Im tired
            Destroy(gameObject);
        }
        else
        {
            iCount = true;
            iExist = true;
        }
        rb = GetComponent<Rigidbody>();
    }

    private void OnDestroy()
    {
        if (iCount)
        {
            iExist = false;
        }
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
            if (other.gameObject.GetComponent<MoveOnGrid>())
            {
                other.gameObject.GetComponent<MoveOnGrid>().enemyJolt(rb.velocity);
            }
            Explosion();
        }
        if(other.gameObject.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
    }

    void Explosion()
    {
        rb.velocity = Vector3.zero;
        float speed = 3.0f;
        Destroy(gameObject);
    }
}
