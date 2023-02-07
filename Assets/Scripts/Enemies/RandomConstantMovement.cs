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
        RaycastHit rch;
        bool result = Physics.Raycast(transform.position, new Vector3(0.0f, 1.0f, 0.0f), out rch, 1.0f);
        if (!(result))
        {
            Debug.Log("Check Up, found nothing");
            return (true);
        }
        Debug.Log("Check Up, found: " + rch.collider.gameObject.name + "  (I am " + gameObject.name + ")");
        if (rch.collider.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Exception. I am: " + gameObject.name + ", who saw a: " + rch.collider.gameObject.name);
            return (false);
        }
        return (!(result));
    }
    public bool checkRight()
    {
        RaycastHit rch;
        bool result = Physics.Raycast(transform.position, new Vector3(1.0f, 0.0f, 0.0f), out rch, 1.0f);
        if (!(result))
        {
            Debug.Log("Check Right, found nothing");
            return (true);
        }
        Debug.Log("Check Right, found: " + rch.collider.gameObject.name + "  (I am " + gameObject.name + ")");
        if (rch.collider.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Exception. I am: " + gameObject.name + ", who saw a: " + rch.collider.gameObject.name);

            return (false);
        }
        return (!(result));
    }
    public bool checkDown()
    {
        RaycastHit rch;
        bool result = Physics.Raycast(transform.position, new Vector3(0.0f, -1.0f, 0.0f), out rch, 1.0f);
        if (!(result))
        {
            Debug.Log("Check Down, found nothing");
            return (true);
        }
        Debug.Log("Check Down, found: " + rch.collider.gameObject.name + "  (I am " + gameObject.name + ")");
        if (rch.collider.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Exception. I am: " + gameObject.name + ", who saw a: " + rch.collider.gameObject.name);

            return (false);
        }
        return (!(result));
    }
    public bool checkLeft()
    {
        RaycastHit rch;
        bool result = Physics.Raycast(transform.position, new Vector3(-1.0f, 0.0f, 0.0f), out rch, 1.0f);
        if (!(result))
        {
            Debug.Log("Check Left, found nothing");
            return (true);
        }
        Debug.Log("Check Left, found: " + rch.collider.gameObject.name + "  (I am " + gameObject.name + ")");
        if (rch.collider.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Exception. I am: " + gameObject.name + ", who saw a: " + rch.collider.gameObject.name);

            return (false);
        }
        return (!(result));
    }

    public bool isLocked()
    {
        return (!(checkLeft() || checkDown() || checkUp() || checkRight()));
    }
    public bool checkDir(int code)
    {
        Debug.Log("Checki8ng");
        switch (code)
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
        if (!(checkDir(dir)))
        {
            setRandomSideways();
            return (true);
        }
        return (false);
    }

    public void maybeGoSidewaysWellSee()
    {
        if (Random.Range(0, 100) != 50)
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
        if (checkOnGridBoth())
        {
            if (!(lookBeforeLeap()))
            {
                maybeGoSidewaysWellSee();
            }
        }
    }

    public bool checkAntigradeInline()
    {
        if(dir == 0 || dir == 2)
        {
            return (checkOnGridY());
        }
        else
        {
            return (checkOnGridX());
        }
    }

    public void setRandomDirection()
    {
        if(isLocked())
        {
            dir = 0;
            return;
        }
        dir = Random.Range(0, 4);
        if (!(checkDir(dir)))
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

        if (isLocked())
        {
            stop();
            return;
        }
        if (checkOne && checkTwo)
        {
            dir = (Random.Range(0, 2) == 0) ? firstC : secondC;
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
        //Debug.Log("E_Set = "+dir);
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
        if ((!(other.isTrigger) && !(other.CompareTag("ChuckMelter"))))
        {
            //setRandomSideways();
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