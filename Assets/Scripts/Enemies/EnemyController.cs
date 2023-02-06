using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float contactDamage = 0.0f;
    public Animator an;
    private Health health;
    public bool diesOnBoomerangHit = false;
    private Vector3 startPoint;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
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
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        if(GetComponent<WallMasterController>())
                return;
        if (transform.localPosition.x < 2) {
            transform.localPosition = new Vector3(2, transform.localPosition.y, 0.0f);
        }
        if (transform.localPosition.y < 2) {
            transform.localPosition = new Vector3(transform.localPosition.x, 2, 0.0f);
        }
        if (transform.localPosition.x > 13) {
            transform.localPosition = new Vector3(13,transform.localPosition.y, 0.0f);
        }
        if(transform.localPosition.y > 8) {
            transform.localPosition = new Vector3(transform.localPosition.x,8,0.0f);
=======
        if(transform.localPosition.x < 0 || transform.localPosition.y < 0 || transform.localPosition.x > 15 || transform.localPosition.y > 8)
        {
            transform.position = startPoint;
>>>>>>> 5b6466be3aa1ef3dee9239bf5929df17cb89a7a9
=======
        if(transform.localPosition.x < 0 || transform.localPosition.y < 0 || transform.localPosition.x > 15 || transform.localPosition.y > 8)
        {
            transform.position = startPoint;
>>>>>>> 5b6466be3aa1ef3dee9239bf5929df17cb89a7a9
=======
        if(transform.localPosition.x < 0 || transform.localPosition.y < 0 || transform.localPosition.x > 15 || transform.localPosition.y > 8)
        {
            transform.position = startPoint;
>>>>>>> 5b6466be3aa1ef3dee9239bf5929df17cb89a7a9
        }
    }

    IEnumerator bringOutYourDead(float x)
    {
        yield return (new WaitForSeconds(x));
        Debug.Log("BringingOut");
        int drop = Random.Range(0,20);
        Debug.Log("Drop_Code = " + drop);
        if(drop <= 1)
        {
            Instantiate(enemyDropBox.instance.heartPickup).transform.position = transform.position;
        }
        else if(drop <= 3)
        {
            Instantiate(enemyDropBox.instance.rupeePickup).transform.position = transform.position;
        }
        else if(drop <= 5)
        {
            Instantiate(enemyDropBox.instance.bombPickup).transform.position = transform.position;
        }
        Destroy(gameObject);
    }

    public void die()
    {
        if(!(GetComponent<BoxCollider>().enabled))
        {
            return;
        }
        an.SetTrigger("dead");
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        StartCoroutine(bringOutYourDead(4.0f/6.0f));
        Debug.Log("Dying");
        health.hearts = 0;
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
            die();


        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        StartCoroutine(unboomerang());
    }

    IEnumerator unboomerang()
    {
        yield return (new WaitForSeconds(3.5f));
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }

    IEnumerator recursiveUnfreeze(int countDown)
    {
        yield return (new WaitForSeconds(0.5f));

        GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r+0.05f, 1.0f,1.0f);//Not working

        if(countDown > 0)
        {
            StartCoroutine(recursiveUnfreeze(countDown - 1));
        }
        else
        {
            unfreeze();

        }
    }

    public void unfreeze()
    {
        if(!(GetComponent<BoxCollider>().enabled))
        {
            return;
        }
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        GetComponent<BoxCollider>().isTrigger = true;
        GetComponent<Animator>().speed = 1;
        gameObject.tag = "Enemy";
    }

    public void freeze()
    {
        GetComponent<SpriteRenderer>().color = new Color(0.6f, 1.0f, 1.0f);//Not working
        GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.white);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        GetComponent<BoxCollider>().isTrigger = false;
        GetComponent<Animator>().speed = 0;
        gameObject.tag = "Frozen";
        StartCoroutine(recursiveUnfreeze(6));
    }
}
