/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelMovement : MoveOnGrid
{
    private int heading;
    private int dir;
    private bool stopper;
    // Start is called before the first frame updat
    private void Start()
    {
        gridDist = 1.0f;
        rb = GetComponent<Rigidbody>();
        stopper = false;
        setRandomDirection();
        goInDirection();
    }

    private void setRandomDirection()
    {
        heading = (Random.Range(1, 5));
    }

    private void goInDirection() {
        float timeout = (gridDist / movementSpeed);
        switch (heading)
        {
            case 1:
                moveUp(movementSpeed, timeout);
                break;
            case 2:
                moveRight(movementSpeed, timeout);
                break;
            case 3:
                moveDown(movementSpeed, timeout);
                break;
            case 4:
                moveLeft(movementSpeed, timeout);
                break;
        }
        stopper = true;
        StartCoroutine(resumeAfterX(timeout));
        StartCoroutine(pauseForXBeforeGoing(timeout + Random.Range(0.0f, 2.0f)));
    }

    private void Update()
    {
        if (Random.Range(1, 1024) == 1)
        {
            setRandomDirection();
        }
    }

    IEnumerator resumeAfterX(float x)
    {
        yield return (new WaitForSeconds(x));
        stopper = false;
        snap();
    }

    IEnumerator pauseForXBeforeGoing(float x)
    {
        yield return (new WaitForSeconds(x));
        goInDirection();
    }

    private void OnCollisionEnter(Collision collision)
    {
        heading++;
        if(heading > 4)
        {
            heading = 1;
        }
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelMovement : MoveOnGrid
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

    private void Start()
    {
        gridDist = 1.0f;
        rb = GetComponent<Rigidbody>();
        setRandomDirection();
        StartCoroutine(goInX(1.0f));
    }
    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator goInX(float x)
    {
        yield return (new WaitForSeconds(x));

        goInDirection();

        StartCoroutine(goInX(0.8f + (Random.Range(0,5)/4.0f)));
    }


    public void setRandomDirection()
    {
        dir = Random.Range(0, 4);
        if (!(checkDir(dir)))
        {
            setRandomDirection();
            return;
        }
    }
    public void setRandomSideways()
    {
        int firstC = ((dir + 1) % 4);
        int secondC = ((dir + 3) % 4);
        bool checkOne = (checkDir(firstC));
        bool checkTwo = (checkDir(secondC));

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
    }

    public void goInDirection()
    {
        if(!(lookBeforeLeap())) {
            if (Random.Range(0, 30) == 5)
            {
                setRandomSideways();
            }
        }
        float time = gridDist/speed;
        switch (dir)
        {
            case 0:
                moveUp(speed,time);
                break;
            case 1:
                moveLeft(speed,time);
                break;
            case 2:
                moveDown(speed,time);
                break;
            default:
                moveRight(speed,time);
                break;
        }
        if(Random.Range(0,10) == 4)
        {
            goInDirection();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!(other.isTrigger))
        {
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
