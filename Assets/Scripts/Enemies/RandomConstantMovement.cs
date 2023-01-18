using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomConstantMovement : MonoBehaviour
{
    public float speed = 1.0f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        setRandomDirection();
        rb.velocity = transform.forward*speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.deltaTime >= 5.0f)
        {
            setRandomDirection();
        }
    }

    void setRandomDirection()
    {
        switch(Random.Range(0,3))
        {
            case 0:
                transform.forward = Vector3.up;
            break;
            case 1:
                transform.forward = Vector3.right;
            break;
            case 2:
                transform.forward = Vector3.down;
            break;
            case 3:
                transform.forward = Vector3.left;
            break;
        }
    }
}
