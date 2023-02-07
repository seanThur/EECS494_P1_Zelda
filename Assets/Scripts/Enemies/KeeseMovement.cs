using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeseMovement : MonoBehaviour
{
    public float topSpeed = 10.0f;
    public float acceleration = 1.0f;
    private Rigidbody rb;
    private Animator an;


    private bool paused = false;
    private bool noThinking = false;
    private float speed;
    public Vector3 heading;
    public float lockoutRandom = 0;
    private int intHeading;
    private Vector3 ejectorSeat;
    private int toingCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        an = GetComponent<Animator>();
        randomHeading();
        ejectorSeat = transform.position;
    }

    void randomHeading()
    {
        float h = (Random.Range(-1, 2));
        float v = (Random.Range(-1, 2));
        intHeading = Random.Range(0, 8);
        switch (intHeading)
        {
            case 0:
                h = -1;
                v = 0;
            break;
            case 1:
                h = -1;
                v = 1;
            break;
            case 2:
                h = 0;
                v = 1;
            break;
            case 3:
                h = 1;
                v = 1;
            break;
            case 4:
                h = 1;
                v = 0;
                break;
            case 5:
                h = 1;
                v = -1;
                break;
            case 6:
                h = 0;
                v = -1;
                break;
            case 7:
                h = -1;
                v = -1;
                break;
        }
        heading = (new Vector3(h, v, 0.0f)).normalized;
    }

    // Update is called once per frame

    private bool isValidTravel()
    {
        RaycastHit rch;
        bool isGonnaHit = Physics.BoxCast(transform.position,new Vector3(0.8f,0.4f,0.0f),heading, out rch, Quaternion.identity, 0.4f);
        if(isGonnaHit)
        {
            if(!(rch.collider.CompareTag("wall")))
            {
                isGonnaHit = false;
            }
        }
        return (!(isGonnaHit));
    }
    void Update()
    {
        if (!(GetComponent<BoxCollider>().enabled))
            return;
        if (!(noThinking))
        {
            if (paused)
            {
                speed -= acceleration * Time.deltaTime;
                if (speed < 0)
                {
                    speed = 0;
                    StartCoroutine(flipAfter(3));
                }
            }
            else
            {
                speed += acceleration * Time.deltaTime;
                if (speed > topSpeed)
                {
                    speed = topSpeed;
                    StartCoroutine(flipAfter(3));
                }
            }
        }
        

        rb.velocity = heading * speed;
        an.speed = speed / topSpeed;

        if(!(isValidTravel()))
        {
            reverse();
            toingCount++;
        } else
        {
            if(toingCount > 0)
            {
                toingCount--;
            }
        }

        if(toingCount>=10 || Mathf.Abs((ejectorSeat - transform.position).magnitude) > 16)
        {
            transform.position = ejectorSeat;
        }

        //Vector3 viewPos = Camera.main.WorldToViewportPoint(gameObject.transform.position);
        //if (!(viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0))
        //{
        //    reverse();
        //}
        if (lockoutRandom <= 0)
        {
            if (Random.Range(0, 400) == 1)
            {
                randomHeading();
            }
        }
        else
        {
            lockoutRandom-=Time.deltaTime;
        }
    }

    public void reverse()
    {
        heading = heading * -1.0f;
        lockoutRandom = 0.2f;
    }

    IEnumerator flipAfter(float n)
    {
        noThinking = true;
        yield return (new WaitForSeconds(n));
        noThinking = false;
        paused = !(paused);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.CompareTag("wall") && lockoutRandom <= 0)
        //{
        //    reverse();
        //}
    }

}
