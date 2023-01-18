using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnGrid : MonoBehaviour
{
    public Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void moveHorizontal(float speed)
    {
        if(snapVertical())
        {
            return;
        }
        rb.velocity = new Vector3(speed, 0.0f, 0.0f);
    }

    void moveVertical(float speed)
    {
        if(snapHorizontal())
        {
            return;
        }
        rb.velocity = new Vector3(0.0f,speed,0.0f);
    }

    public void moveUp(float speed)
    {
        moveVertical(-1.0f*speed);
    }

    public void moveDown(float speed)
    {
        moveVertical(speed);
    }

    public void moveLeft(float speed)
    {
        moveHorizontal(-1.0f*speed);
    }

    public void moveRight(float speed)
    {
        moveHorizontal(speed);
    }

    //True if the snap move a "large" distance (>= 0.1)
    bool snapVertical()
    {
        float tempY = rb.transform.position.y;
        float tempYFixed = Mathf.Round(tempY * 2.0f) / 2.0f;
        rb.transform.position = new Vector3(rb.transform.position.x, tempYFixed, rb.transform.position.z);
        return (Mathf.Abs(tempY - tempYFixed) >= 0.1f);
    }

    bool snapHorizontal()
    {
        float tempX = rb.transform.position.x;
        float tempXFixed = Mathf.Round(tempX * 2.0f) / 2.0f;
        rb.transform.position = new Vector3(tempXFixed, rb.transform.position.y, rb.transform.position.z);
        return (Mathf.Abs(tempX - tempXFixed) >= 0.1f);
    }

    public void stop()
    {
        rb.velocity = Vector3.zero;
    }

    public void snap()
    {
        snapHorizontal();
        snapVertical();
    }

    public void stopNSnap()
    {
        stop();
        snap();
    }
}
