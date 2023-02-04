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
        if (Random.Range(0,1024) == 0 && !(stopped))
        {
            setRandomDirection();
        }
        if(dir != wasDir)
        {
            wasDir = dir;
            an.SetInteger("Dir", dir);
            an.SetTrigger("Impulse");
        }
        
    }

    private void throwBoomerang()
    {
        //Add scan to ensure 1 tile space
        stopped = true;
        GameObject inst = Instantiate(boomerang);
        inst.transform.position = transform.position;
        inst.GetComponent<GoriyaBoomerangController>().launch(getHeadingVector());
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
