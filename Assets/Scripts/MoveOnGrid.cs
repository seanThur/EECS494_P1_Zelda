using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnGrid : MonoBehaviour
{
    public Rigidbody rb;
    public static float gridDist = .5f;
    public float movementSpeed = 1.0f;

    private float x;
    private float y;
    public int xRem;
    public int yRem;
    private bool onGridx;
    private bool onGridy;

    private float xInput = 0.0f;
    private float yInput = 0.0f;

    private float tolerance = 0.05f;

    public bool checkOnGridX()
    {
        float gridMul = (1 / gridDist);
        float nearest = Mathf.Round(transform.position.x * gridMul) / gridMul;

        return (Mathf.Abs(transform.position.x - nearest) <= tolerance);
    }

    public bool checkOnGridY()
    {
        float gridMul = (1 / gridDist);
        float nearest = Mathf.Round(transform.position.y * gridMul) / gridMul;

        return (Mathf.Abs(transform.position.y - nearest) <= tolerance);
    }

    public bool checkOnGridBoth()
    {
        return (checkOnGridX() && checkOnGridY());
    }

    public bool checkOnGridEither()
    {
        return (checkOnGridX() || checkOnGridY());
    }

    public float getxInput()
    {
        return (xInput);
    }

    public float getyInput()
    {
        return (yInput);
    }

    public Vector3 getHeadingVector()
    {
        return (new Vector3(xInput, yInput, 0.0f));
    }

    public void snap()
    {
        float gridlineConstant = 1.0f / gridDist;
        float xPos = Mathf.Round(rb.transform.position.x*gridlineConstant)/gridlineConstant;
        float yPos = Mathf.Round(rb.transform.position.y*gridlineConstant)/gridlineConstant;
        rb.transform.position = new Vector3(xPos,yPos,0.0f);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        snap();
    }


    public void moveUp()
    {
        xInput = 0.0f;
        yInput = 1.0f;
    }

    public void moveDown()
    {
        xInput = 0.0f;
        yInput = -1.0f;
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
        snap();
    }

    public void manualSet(float inX, float inY)
    {
        xInput = inX;
        yInput = inY;
    }

    IEnumerator phaseOutInX(float x)
    {
        yield return (new WaitForSeconds(x));

        GetComponent<BoxCollider>().isTrigger = true;
    }

    public void enemyJolt(Vector3 heading)
    {
        GetComponent<BoxCollider>().isTrigger = false;
        heading = heading.normalized;
        if(Mathf.Abs(heading.x) < Mathf.Abs(heading.y))
        {
            if(heading.y > 0)
            {
                moveUp(20.0f,0.25f);
            }
            else
            {
                moveDown(20.0f, 0.25f);
            }
        } else
        {
            if (heading.x > 0)
            {
                moveRight(20.0f, 0.25f);
            }
            else
            {
                moveLeft(20.0f, 0.25f);
            }
        }
        StartCoroutine(phaseOutInX(0.25f));
    }
    /*
    public bool checkUp()
    {
        return (Physics.Raycast(gameObject.transform.position, new Vector3(0.0f,1.0f,0.0f), gridDist));
    }

    public bool checkDown()
    {
        return (Physics.Raycast(gameObject.transform.position, new Vector3(0.0f, -1.0f, 0.0f), gridDist));
    }

    public bool checkLeft()
    {
        return (Physics.Raycast(gameObject.transform.position, new Vector3(-1.0f, 0.0f, 0.0f), gridDist));
    }

    public bool checkRight()
    {
        return (Physics.Raycast(gameObject.transform.position, new Vector3(1.0f, 0.0f, 0.0f), gridDist));
    }

    public bool checkDir(int dir)
    {
        switch(dir)
        {
            case 1:return (checkUp());
            case 2: return (checkUp());
            case 3: return (checkUp());
            default: return (checkUp());
        }
    }
    */

    void FixedUpdate()
    {
        int rem = 0;
        //set gridMovement values
        x = rb.transform.position.x;
        y = rb.transform.position.y;

        //with 2 decimal places
        xRem = (int)Mathf.Floor(x * 100) % (int)(gridDist * 10);
        yRem = (int)Mathf.Floor(y * 100) % (int)(gridDist * 10);
        if (xRem == rem)
        {
            onGridy = true;
        }
        else
        {
            onGridy = false;
        }

        if (yRem == rem)
        {
            onGridx = true;
        }
        else
        {
            onGridx = false;
        }

        //calls grid movement, checks movement
        Vector2 currentInput = GetInput();
        
        rb.velocity = currentInput * movementSpeed;


    }

    Vector2 GetInput()
    {

        if (xInput == 0.0f && yInput == 0.0f)
        {
            return Vector2.zero;
        }


        //prevents diagonal movement, implements grid movement
        if (Mathf.Abs(xInput) > 0.0f)
        {
            yInput = 0.0f;
        }

        //by this point, only have an x input or a y input
        Debug.Assert((xInput != 0.0f && yInput == 0.0f) || (xInput == 0.0f && yInput != 0.0f), x.ToString() + " " + y.ToString() + " invalid");

        //should also be on one of the grids
        Debug.Assert(onGridx || onGridy, "Not on either gridline!");

        return gridMovement(xInput, yInput);
    }

    private Vector2 gridMovement(float xInput, float yInput)
    {
        Debug.Assert(xInput == 0.0f || yInput == 0.0f, "One input must be 0 by this point!");
        float xOutput = xInput;
        float yOutput = yInput;

        //if xInput but not on grid x
        if (xInput != 0.0f && !onGridx)
        {
            //should be on grid y
            if (!onGridy)
                Debug.Assert(onGridy, "Not on either gridline! (should be on grid y)");

            xOutput = 0.0f;

            if (rb.position.y % gridDist < (gridDist / 2.0f))
            {
                yOutput = -0.1f;
            }
            else
            {
                yOutput = 0.1f;
            }
        }
        else if (yInput != 0.0f && !onGridy)
        {
            if (!onGridx)
                Debug.Assert(onGridx, "Not on either gridline! (should be on grid x)");

            yOutput = 0.0f;

            if (x % gridDist < (gridDist / 2.0f))
            {
                xOutput = -0.1f;
            }
            else
            {
                xOutput = 0.1f;
            }
        }

        return new Vector2(xOutput, yOutput);
    }
}