using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnGrid : MonoBehaviour
{
    public Rigidbody rb;
    public float gridDist = .5f;
    public float movementSpeed = 1.0f;

    private float x;
    private float y;
    private int xRem;
    private int yRem;
    private bool onGridx;
    private bool onGridy;

    private float xInput = 0.0f;
    private float yInput = 0.0f;

    private float tolerance = 0.05f;

    private bool allowY = true;

    static public bool isDoor(string tag)
    {
        if(tag == "eastDoor" || tag == "westDoor" || tag == "northDoor" || tag == "southDoor" || tag == "LeastDoor" || tag == "LwestDoor" || tag == "LnorthDoor" || tag == "LsouthDoor")
        {
            return (true);
        }
        return (false);
    }
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
        if (GetComponent<GelMovement>())
        {
            snap();
        }
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
        snap();
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

    //mog.moveDir(vecToDir(direction),movementSpeed*5.0f,0.2f);
    public void moveDir(int dir, float speed, float time)
    {
        switch(dir)
        {
            case 1: moveUp(speed, time); break;
            case 2: moveRight(speed, time); break;
            case 3: moveDown(speed, time); break;
            case 4: moveLeft(speed, time); break;
        }
    }
    /*
    public bool checkUp()
    {
        return (Physics.Raycast(gameObject.transform.position, new Vector3(0.0f,1.0f,0.0f), gridDist));
    }

<<<<<<< HEAD
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

        int x10 = Mathf.RoundToInt(x * 10);
        int y10 = Mathf.RoundToInt(y * 10);
        float gd10f = 10 * gridDist;
        int gridDist10 = Mathf.RoundToInt(gd10f);

        xRem = x10 % gridDist10;
        yRem = y10 % gridDist10;

        //if(rb.gameObject.name.Equals("Player"))
        //{
        //    Debug.Log(rb.gameObject.name + " x: " + x + "xRem: " + xRem + " x10: " + x10 + " y: " + y + " yRem: " + yRem + " y10: " + y10 + 
        //        " gridDist: " + gridDist + " gridDist10: " + gridDist10);
        //}

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
        if (Mathf.Abs(xInput) > 0.0f || !allowY)
        {
            yInput = 0.0f;
        }

        //by this point, only have an x input or a y input
        Debug.Assert((xInput != 0.0f && yInput == 0.0f) || (xInput == 0.0f && yInput != 0.0f), x.ToString() + " " + y.ToString() + " invalid");

        //should also be on one of the grids
        Debug.Assert(onGridx || onGridy, "Not on either gridline! " + xRem + " " + yRem);

        return gridMovement(xInput, yInput);
    }

    private Vector2 gridMovement(float xInput, float yInput)
    {
        Debug.Assert(xInput == 0.0f || yInput == 0.0f, "One input must be 0 by this point!");
        float xOutput = xInput;
        float yOutput = yInput;

        int gridDistInt = (int)gridDist * 10;
        //if xInput but not on grid x
        if (xInput != 0.0f && !onGridx)
        {
            //should be on grid y
            if (!onGridy)
                Debug.Assert(onGridy, "Not on either gridline! (should be on grid y) " + xRem + " " + yRem);

            xOutput = 0.0f;

            if (yRem <= 3)
            {
                yOutput = -Mathf.Abs(xInput);
            }
            else
            {
                yOutput = Mathf.Abs(xInput);
            }
        }
        else if (yInput != 0.0f && !onGridy)
        {
            if (!onGridx)
                Debug.Assert(onGridx, "Not on either gridline! (should be on grid x) " + xRem + " " + yRem);

            yOutput = 0.0f;

            if (xRem <= 3)
            {
                xOutput = -Mathf.Abs(yInput);
            }
            else
            {
                xOutput = Mathf.Abs(yInput);
            }
        }

        return new Vector2(xOutput, yOutput);
    }

    public void setAllowY(bool b)
    {
        allowY = b;
        //return allowY;
    }
}