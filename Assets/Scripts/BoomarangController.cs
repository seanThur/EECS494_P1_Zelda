using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomarangController : MonoBehaviour
{
    public float speed = 5.0f;
    public Rigidbody rb;
    private bool reversing = false;
    static bool iExist;
    bool iCount = false;

    // Start is called before the first frame update
    void Start()
    {
        if (iExist)
        {
            Destroy(gameObject);
        }
        else
        {
            iCount = true;
            iExist = true;
        }
        rb = GetComponent<Rigidbody>();
        rb.AddTorque(new Vector3(0, 0, 600.0f));
        StartCoroutine(limitLength());
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
        Inventory inventory = PlayerController.playerInstance.gameObject.GetComponent<Inventory>();
        Health health = PlayerController.playerInstance.gameObject.GetComponent<Health>();
        Debug.Log("Other Tag = " + other.gameObject.tag);
        if(other.CompareTag("Enemy"))
        {
            reverse();
            other.GetComponent<EnemyController>().boomerangHit();
        }
        else if (other.tag.Equals("rupee"))
        {
            Debug.Log("Collected rupee!");
            inventory.AddRupees(1);
            AudioController.audioInstance.playEffect(AudioController.audioInstance.rupee);

            Destroy(other.gameObject);

        }
        else if (other.tag.Equals("heart"))
        {
            Debug.Log("Collected heart");
            AudioController.audioInstance.playEffect(AudioController.audioInstance.heartKey);
            Destroy(other.gameObject);

            if (!health.isAtMaxHearts())
            {
                health.heal(1);
            }

        }
        else if (other.tag.Equals("bomb"))
        {
            Debug.Log("Collected bomb");
            AudioController.audioInstance.playEffect(AudioController.audioInstance.bombDrop);
            Destroy(other.gameObject);

            inventory.AddBombs(1);

        }
        else if (other.tag.Equals("key"))
        {
            Debug.Log("Collected key");
            AudioController.audioInstance.playEffect(AudioController.audioInstance.heartKey);
            Destroy(other.gameObject);

            inventory.addKeys(1);

        }
        else if (other.CompareTag("Bow"))
        {
            GetComponent<Bow>().equipped = true;
            Destroy(other.gameObject);
        }
    }

    IEnumerator limitLength()
    {
        yield return (new WaitForSeconds(1.0f));

        reverse();
    }
}
