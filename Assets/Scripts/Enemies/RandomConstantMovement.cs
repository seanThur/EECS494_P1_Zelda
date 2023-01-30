using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomConstantMovement : MoveOnGrid
{
    public float speed = 5.0f;
    public int dir;
    // Start is called before the first frame update
    public bool checkUp()
    {
        return (Physics.Raycast(transform.position, new Vector3(0.0f, 1.0f, 0.0f), 1.5f));
    }
    public bool checkRight()
    {
        return (Physics.Raycast(transform.position, new Vector3(1.0f, 0.0f, 0.0f), 1.5f));
    }
    public bool checkDown()
    {
        return (Physics.Raycast(transform.position, new Vector3(0.0f, -1.0f, 0.0f), 1.5f));
    }
    public bool checkLeft()
    {
        return (Physics.Raycast(transform.position, new Vector3(-1.0f, 0.0f, 0.0f), 1.5f));
    }
    public bool checkDir()
    {
        switch(dir)
        {
            case 0:
                return (checkUp());
            case 1:
                return (checkRight());
            case 2:
                return (checkDown());
            default:
                return (checkLeft());
        }
    }
    private void Start()
    {
        gridDist = 1.0f;
        rb = GetComponent<Rigidbody>();
        setRandomDirection();
    }
    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0,1024) == 0)
        {
            setRandomDirection();
        }
    }

    public void setRandomDirection()
    {
        dir = Random.Range(0, 4);
        goInDirection();
    }

    public void goInDirection()
    {
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
        if (!(other.isTrigger)) {
            rb.position -= rb.velocity * Time.deltaTime * 2;//Step out of solid

            dir += Random.RandomRange(0, 2) == 0 ? 1 : 3;//Pick a perpendicular direction...
            dir = dir % 4;

            goInDirection();//...and commit to it!
        }
        
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        stop();
        setRandomDirection();
    }
    */
}
