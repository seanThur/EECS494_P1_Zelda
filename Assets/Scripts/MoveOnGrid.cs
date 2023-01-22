using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnGrid : MonoBehaviour
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
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        xInput = inX;
        yInput = inY;
    }

    




    void FixedUpdate()
    {

        //set gridMovement values
        x = rb.transform.position.x;
        y = rb.transform.position.y;

        //with only 1 decimal point
        xRem = (float)System.Math.Floor((double)(x % gridDist) * 10) / 10;
        yRem = (float)System.Math.Floor((double)(y % gridDist) * 10) / 10;

        if (xRem == 0.0f)
        {
            onGridy = true;
        }
        else
        {
            onGridy = false;
        }

        if (yRem == 0.0f)
        {
            onGridx = true;
        }
        else
        {
            onGridx = false;
        }

        //calls grid movement, checks movement
        Vector2 currentInput = GetInput();

        //coords.text = x.ToString() + " " + y.ToString() + "\n" + onGridx.ToString() + " " + onGridy.ToString() + "\n" + currentInput.x.ToString() + " " + currentInput.y.ToString();

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

            if (y % gridDist < (gridDist / 2.0f))
            {
                yOutput = -0.5f;
            }
            else
            {
                yOutput = 0.5f;
            }
        }
        else if (yInput != 0.0f && !onGridy)
        {
            if (!onGridx)
                Debug.Assert(onGridx, "Not on either gridline! (should be on grid x)");

            yOutput = 0.0f;

            if (x % gridDist < (gridDist / 2.0f))
            {
                xOutput = -0.5f;
            }
            else
            {
                xOutput = 0.5f;
            }
        }


        //Debug.Log("x: " + x + " " + "y: " + y + " xOutput: " + xOutput + " yOutput: " + yOutput + " onGridx: " + onGridx + " onGridy: " + onGridy
        //    + " xRem: " + y % xGridDist + " yRem: " + x % yGridDist);

        return new Vector2(xOutput, yOutput);
    }
}