using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    float boomyBox = 0;
    IEnumerator disapate()
    {
        yield return (new WaitForSeconds(1.5f));
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(disapate());
    }

    // Update is called once per frame
    void Update()
    {
        boomyBox+=Time.deltaTime;
        if(boomyBox >= 0.1f)
        {
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().takeDamage(5);
        }
        else if(other.CompareTag("BombWall"))
        {
            other.GetComponent<Health>().goCommitDie();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().takeDamage(5);
        }
        else if (other.CompareTag("BombWall"))
        {
            other.GetComponent<Health>().goCommitDie();
        }
    }
}
