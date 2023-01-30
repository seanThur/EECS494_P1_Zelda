using System.Collections;
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
