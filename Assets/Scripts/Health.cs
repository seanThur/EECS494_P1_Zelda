using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float hearts = 3.0f;
    public float maxHearts = 3.0f;
    public bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        maxHearts = hearts;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goCommitDie()
    {
        dead = true;
        //Destroy(gameObject);
    }

    private void checkLimits()
    {
        if (hearts <= 0.05f)
        {
            goCommitDie();
        }
        //if (hearts > maxHearts)
        //{
        //    hearts = maxHearts;
        //}
    }

    public void takeDamage(float amount)
    {
        hearts -= amount;
        checkLimits();
    }

    public void heal(float amount)
    {
        hearts += amount;
        checkLimits();
    }

    public bool isAtMaxHearts()
    {
        return (maxHearts-0.05f <= hearts);
    }
}
