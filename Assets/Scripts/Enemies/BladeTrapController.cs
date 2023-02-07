using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTrapController : EnemyProjectile
{
    public Rigidbody rb;
    public float speed;

    public bool reversing = false;
    private Vector3 saveVel;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    bool areWeThereYet()
    {
        return (Mathf.Abs((transform.position - startPos).magnitude) < 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        if (reversing)
        {
            if (areWeThereYet())
            {
                rb.velocity = Vector3.zero;
                transform.position = startPos;
                reversing = false;
            }
        }
        else
        {
            int res = checkAll();
            if (res != 0)
            {
                getEm(res);
            }
        }
    }

    private void getEm(int dir)
    {
        switch(dir)
        {
            case 1:
                rb.velocity = new Vector3(0.0f,1.0f,0.0f) * speed;
                break;
            case 2:
                rb.velocity = new Vector3(1.0f, 0.0f, 0.0f) * speed;
                break;
            case 3:
                rb.velocity = new Vector3(0.0f, -1.0f, 0.0f) * speed;
                break;
            default:
                rb.velocity = new Vector3(-1.0f, 0.0f, 0.0f) * speed;
                break;
        }
        saveVel = rb.velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger || other.CompareTag("Water"))
            return;
       // Debug.Log("HIT");
        if (!(reversing))
        {
            reversing = true;
            rb.velocity = saveVel * -1.0f * 0.5f;
        }
    }

    private int checkAll()
    {
        Vector3 player = PlayerController.playerInstance.transform.position;

        if(player.y > startPos.y - 0.95f && player.y < startPos.y + 0.95f)
        {
            if(player.x > startPos.x)
            {
                return (2);
            } else
            {
                return (4);
            }
        }
        if (player.x > startPos.x - 0.95f && player.x < startPos.x + 0.95f)
        {
            if (player.y > startPos.y)
            {
                return (1);
            }
            else
            {
                return (3);
            }
        }
        return (0);


        bool top = Physics.BoxCast(transform.position, transform.localScale * 0.5f, new Vector3(0.0f,1.0f,0.0f), Quaternion.identity, 10.0f, 0);
        bool bottom = Physics.BoxCast(transform.position, transform.localScale * 0.5f, new Vector3(0.0f,-1.0f,0.0f), Quaternion.identity, 10.0f, 0);
        bool left = Physics.BoxCast(transform.position, transform.localScale * 0.5f, new Vector3(-1.0f, 0.0f, 0.0f), Quaternion.identity, 10.0f, 0);
        bool right = Physics.BoxCast(transform.position, transform.localScale * 0.5f, new Vector3(1.0f, 0.0f, 0.0f), Quaternion.identity, 10.0f, 0);
        
        
        if (top)
            return (1);
        if (right)
            return (2);
        if (bottom)
            return (3);
        if (left)
            return (4);
        return (0);
    }
}
