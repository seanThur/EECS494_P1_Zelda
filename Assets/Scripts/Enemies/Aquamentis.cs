using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aquamentis : MoveOnGrid
{
    float rightBound;
    float leftBound;
    Animator an;
    public GameObject fireball;
    // Start is called before the first frame update

    public void shoot()
    {
        an.SetTrigger("Fire");
        StartCoroutine(shootInX(5.0f));
        StartCoroutine(shootingPayload());
    }

    IEnumerator shootingPayload()
    {
        yield return (new WaitForSeconds(0.4f));
        Vector3 fHeading = PlayerController.playerInstance.transform.position - transform.position;
        Debug.Log("FIREING: fheading = " + fHeading.ToString());

        GameObject inst = Instantiate(fireball);
        GameObject inst2 = Instantiate(fireball);
        GameObject inst3 = Instantiate(fireball);

        inst.transform.position = transform.position + (Vector3.left);
        inst2.transform.position = transform.position + (Vector3.left) + Vector3.up;
        inst3.transform.position = transform.position + (Vector3.left) + Vector3.down;

        inst.GetComponent<AquaBallController>().launch(fHeading.normalized);
        inst2.GetComponent<AquaBallController>().launch((fHeading+Vector3.up).normalized);
        inst3.GetComponent<AquaBallController>().launch((fHeading+Vector3.down).normalized);
    }

    IEnumerator shootInX(float x)
    {
        yield return (new WaitForSeconds(x));
        shoot();
    }

    void Start()
    {
        StartCoroutine(shootInX(5.0f));
        an = GetComponent<Animator>();
        rightBound = transform.position.x +  1;
        leftBound = transform.position.x - 2;
        moveLeft(movementSpeed);
    }

    // Update is called once per frame
    void Update()
    {

        if(transform.position.x > rightBound)
        {
            moveLeft(movementSpeed);
        }
        if(transform.position.x < leftBound)
        {
            moveRight(movementSpeed);
        }
    }
}
