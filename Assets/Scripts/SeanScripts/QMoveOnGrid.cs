using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QMoveOnGrid : MonoBehaviour
{
    public Rigidbody rb;
    public static float gridDist = 0.5f;
    public float movementSpeed = 1.0f;

    private float x;
    private float y;
    private float xRem;
    private float yRem;
    private bool onGridx;
    private bool onGridy;
    private float xInput = 0.0f;
    private float yInput = 0.0f;

    private float tolerance;
    private float watchDogGoalLine;//Gridline Target
    private int iswatchingDogs;//1 - vertical, 2 - horizontal
    private bool isWatchDogOverbound;//true - trigger when OVER line, flase - trigger when UNDER line
    private float perpendicularSpeed;//when gridline hit, use this speed

    public void snap()
    {
        float gridlineConstant = 1.0f / gridDist;
        float xPos = Mathf.Round(rb.transform.position.x*gridlineConstant)/gridlineConstant;
        float yPos = Mathf.Round(rb.transform.position.y*gridlineConstant)/gridlineConstant;
        rb.transform.position = new Vector3(xPos, yPos, 0.0f);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        tolerance = 0.01f;
        iswatchingDogs = 0;
        snap();
    }


    public void moveUp()
    {
        xInput = 0.0f;
        yInput = -1.0f;
    }

    public void moveDown()
    {
        xInput = 0.0f;
        yInput = 1.0f;
    }

    public void moveLeft()
    {
        xInput = -1.0f;
        yInput = 0.0f;
    }

    public void moveRight()
    {
        xInput = 1.0f;
        yInput = 0.0f;
    }


    public void moveUp(float speed)
    {
        movementSpeed = speed;
        moveUp();
    }

    public void moveDown(float speed)
    {
        movementSpeed = speed;
        moveDown();
    }

    public void moveLeft(float speed)
    {
        movementSpeed = speed;
        moveLeft();
    }

    public void moveRight(float speed)
    {
        movementSpeed = speed;
        moveRight();
    }

    public void moveUp(float speed, float timeout)
    {
        StartCoroutine(stopAfterABit(timeout));
        moveUp(speed);
    }

    public void moveDown(float speed, float timeout)
    {
        StartCoroutine(stopAfterABit(timeout));
        moveDown(speed);
    }

    public void moveLeft(float speed, float timeout)
    {
        StartCoroutine(stopAfterABit(timeout));
        moveLeft(speed);
    }

    public void moveRight(float speed, float timeout)
    {
        StartCoroutine(stopAfterABit(timeout));
        moveRight(speed);
    }

    public void reverse()
    {
        xInput = -1.0f * xInput;
        yInput = -1.0f * yInput;
    }

    public void reverse(float speed)
    {
        movementSpeed = speed;
        reverse();
    }

    public void reverse(float speed, float timeout)
    {
        StartCoroutine(stopAfterABit(timeout));
        reverse(speed);
    }

    public void stop()
    {
        xInput = 0.0f;
        yInput = 0.0f;
    }

    IEnumerator stopAfterABit(float aBit)
    {
        float save = movementSpeed;
        yield return (new WaitForSeconds(aBit));

        stop();
        movementSpeed = save;
    }

    public void manualSet(float inX, float inY)
    {
        move(inX, inY);
    }


    private void setWatchdog(float target, bool isVertical, float pSpeed)
    {
        watchDogGoalLine = target;//Gridline Target
        perpendicularSpeed = pSpeed;//when gridline hit, use this speed
        if (isVertical)
        {
            iswatchingDogs = 1;//1 - vertical, 2 - horizontal
            isWatchDogOverbound = rb.position.y < target;//true - trigger when OVER line, flase - trigger when UNDER line
            if(isWatchDogOverbound)
            {
                rb.velocity = new Vector3(0.0f,movementSpeed,0.0f);
            } 
            else
            {
                rb.velocity = new Vector3(0.0f, -1.0f * movementSpeed, 0.0f);
            }
        } else
        {
            iswatchingDogs = 2;//1 - vertical, 2 - horizontal
            isWatchDogOverbound = rb.position.x < target;//true - trigger when OVER line, flase - trigger when UNDER line
            if (isWatchDogOverbound)
            {
                rb.velocity = new Vector3(movementSpeed, 0.0f, 0.0f);
            }
            else
            {
                rb.velocity = new Vector3(-1.0f * movementSpeed, 0.0f, 0.0f);
            }
        }
    }

    private bool checkWatchdog()
    {
        bool passed = false;
        if (iswatchingDogs == 1)
        { // vertical
            if(isWatchDogOverbound)
            {
                if(rb.position.y >= watchDogGoalLine)
                {
                    passed = true;
                }
            } 
            else
            {
                if(rb.position.y <= watchDogGoalLine)
                {
                    passed = true;
                }
            }
            if (passed)
            {
                rb.position = new Vector3(rb.position.x, perpendicularSpeed, 0.0f);
                rb.velocity = new Vector3(perpendicularSpeed, 0.0f, 0.0f);
            }
        } else if (iswatchingDogs == 2)
        { // horizontal
            if (isWatchDogOverbound)
            {
                if (rb.position.x >= watchDogGoalLine)
                {
                    passed = true;
                }
            }
            else
            {
                if (rb.position.x <= watchDogGoalLine)
                {
                    passed = true;
                }
            }

            if (passed)
            {
                rb.position = new Vector3(perpendicularSpeed, rb.position.y,0.0f);
                rb.velocity = new Vector3(0.0f, perpendicularSpeed, 0.0f);
            }
        }
        if(passed)
        {
            iswatchingDogs = 0;
        }
        return (passed);
    }

    private bool isInGrid(float p)
    {
        float gridlineConstant = 1.0f / gridDist;
        float pOnGridline = Mathf.Round(p * gridlineConstant) / gridlineConstant;
        if (Mathf.Abs(p - pOnGridline) <= tolerance)//if the distance from the point to the gridline is within tolerance
        {
            return (true);
        }
        return (false);
    }
    
    private void verticalMovement(float value)
    {
        if(isInGrid(rb.position.x))
        { //Object is horizontally aligned
            rb.velocity = new Vector3(0.0f,value,0.0f);
        }
        else
        { //Object needs to be horizontally aligned
            float gridlineConstant = 1.0f / gridDist;
            float target = Mathf.Round(rb.position.x * gridlineConstant) / gridlineConstant;
            setWatchdog(target, false, value);
        }
    }

    private void horizontalMovement(float value)
    {
        if (isInGrid(rb.position.y))
        { //Object is horizontally aligned
            rb.velocity = new Vector3(value, 0.0f, 0.0f);
        }
        else
        { //Object needs to be horizontally aligned
            float gridlineConstant = 1.0f / gridDist;
            float target = Mathf.Round(rb.position.y * gridlineConstant) / gridlineConstant;
            setWatchdog(target, true, value);
        }
    }

    private void move(Vector3 heading)
    {
        if(heading.x >= tolerance || heading.x <= tolerance*-1)
        {
            horizontalMovement(heading.x * movementSpeed);
        }
        else
        {
            verticalMovement(heading.y * movementSpeed);
        }
    }
    private void move(float x, float y)
    {
        move(new Vector3(x, y, 0.0f));
    }

    

    private void move(Vector3 heading, float speed)
    {
        movementSpeed = speed;
        move(heading);
    }



}