using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoriyaMovement : RandomConstantMovement
{
    Animator an;
    bool stopped;
    public GameObject boomerang;
    private int wasDir;
    private void Start()
    {
        an = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        stopped = false;
        setRandomDirection();
        snap();
        StartCoroutine(boomerangIn(5.0f + Random.Range(0.0f, 10.0f)));
    }
    // Update is called once per frame
    void Update()
    {
        if (!(stopped))
        {
            if (checkOnGridBoth())
            {
                if (!(lookBeforeLeap()))
                {
                    maybeGoSidewaysWellSee();
                }
            }
        }
        if (dir != wasDir  && GetComponent<BoxCollider>().enabled)
        {
            wasDir = dir;
            an.SetInteger("Dir", dir);
            an.SetTrigger("Impulse");
        }
        
    }

    private void throwBoomerang()
    {
        if (!(GetComponent<BoxCollider>().enabled))
        {
            return;
        }
        //Add scan to ensure 1 tile space
        stopped = true;
        GameObject inst = Instantiate(boomerang);
        inst.transform.position = transform.position;
        inst.GetComponent<GoriyaBoomerangController>().launch(getHeadingVector(), this);
        stop();
    }
    public void boomerangReset()
    {
        stopped = false;
        goInDirection();
        StartCoroutine(boomerangIn(5.0f+Random.Range(0.0f,10.0f)));
    }

    IEnumerator boomerangIn(float x)
    {
        yield return (new WaitForSeconds(x));
        throwBoomerang();
    }
}
