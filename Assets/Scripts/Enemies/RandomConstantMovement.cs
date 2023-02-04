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
        return (!(Physics.Raycast(transform.position, new Vector3(0.0f, 1.0f, 0.0f), 1.0f)));
    }
    public bool checkRight()
    {
        return (!(Physics.Raycast(transform.position, new Vector3(1.0f, 0.0f, 0.0f), 1.0f)));
    }
    public bool checkDown()
    {
        return (!(Physics.Raycast(transform.position, new Vector3(0.0f, -1.0f, 0.0f), 1.0f)));
    }
    public bool checkLeft()
    {
        return (!(Physics.Raycast(transform.position, new Vector3(-1.0f, 0.0f, 0.0f), 1.0f)));
    }
    public bool checkDir(int code)
    {
        switch(code)
        {
            case 0:
                return (checkUp());
            case 1:
                return (checkLeft());
            case 2:
                return (checkDown());
            default:
                return (checkRight());
        }
    }


    public bool lookBeforeLeap()
    {
        if(!(checkDir(dir)))
        {
            setRandomSideways();
            return (true);
        }
        return (false);
    }

    public void maybeGoSidewaysWellSee()
    {
        if(Random.Range(0,100) != 50)
            return;
        bool checkOne = (checkDir((dir + 1) % 4));
        bool checkTwo = (checkDir((dir + 3) % 4));

        if (checkOne && checkTwo)
        {
            dir = (dir + (Random.Range(0, 2) * 2) + 1) % 4;
        }
        else if (checkOne && !(checkTwo))
        {
            dir = (dir + 1) % 4;
        }
        else if (!(checkOne) && checkTwo)
        {
            dir = (dir + 3) % 4;
        }
        goInDirection();
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
        //if(Random.Range(0,1024) == 0)
        //{
        //    setRandomDirection();
        //}
        if(checkOnGridBoth())
        {
            if(!(lookBeforeLeap()))
            {
                maybeGoSidewaysWellSee();
            }
        }

    }



    public void setRandomDirection()
    {
        dir = Random.Range(0, 4);
        if(!(checkDir(dir)))
        {
            setRandomDirection();
            return;
        }
        goInDirection();
    }
    public void setRandomSideways()
    {
        int firstC = ((dir + 1) % 4);
        int secondC = ((dir + 3) % 4);
        bool checkOne = (checkDir(firstC));
        bool checkTwo = (checkDir(secondC));

        if (checkOne && checkTwo)
        {
            dir = (Random.Range(0,2) == 0) ? firstC : secondC;
        }
        else if (checkOne && !(checkTwo))
        {
            dir = firstC;
        }
        else if (!(checkOne) && checkTwo)
        {
            dir = secondC;
        }
        else if (!(checkOne) && !(checkTwo))
        {
            dir = (dir + 2) % 4;
        }
        Debug.Log("E_Set = "+dir);
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
            setRandomSideways();
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
