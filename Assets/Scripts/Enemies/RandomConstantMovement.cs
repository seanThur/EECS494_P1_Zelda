using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomConstantMovement : MoveOnGrid
{
    public float speed = 5.0f;
    private int dir;
    // Start is called before the first frame updat
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        setRandomDirection();
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.deltaTime >= 5.0f && Random.Range(0,30) == 0)
        {
            setRandomDirection();
        }
    }

    void setRandomDirection()
    {
        dir = Random.Range(0, 4);
        switch (dir)
        {
            case 0:
                moveUp(speed);
            break;
            case 1:
                moveLeft(speed);
                break;
            case 2:
                moveDown(speed);
                break;
            default:
                moveRight(speed);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        stopNSnap();
        setRandomDirection();
    }
}
