using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputToAnimator : MonoBehaviour
{
    public Animator animator;
    public MoveOnGrid mog;
    public bool isAttacking = false;
    public bool isHurt = false;
    public int lastDirection = 3;
    private float horizontal;
    private float vertical;
    public bool isUsing = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        mog = GetComponent<MoveOnGrid>();
    }

    public int getDir()
    {
        return (getDir(horizontal, vertical));
    }

    int getDir(float h, float v)
    {
        if(h==0 && v!=0)
        {
            if(v>0)
            {
                return (1);
            } 
            else
            {
                return (3);
            }
        }
        else if(h!=0 && v==0)
        {
            if(h>0)
            {
                return (2);
            }
            else
            {
                return (4);
            }
        }
        return (0);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.acceptInput)
        {
            //horizontal = Input.GetAxisRaw("Horizontal");
            //vertical = Input.GetAxisRaw("Vertical");
            int dir = getDir(mog.currentInputx, mog.currentInputy);

            if (dir != 0)
            {
                lastDirection = dir;
            }

            if (!(isAttacking))
            {

                if (dir != 0)
                {
                    animator.SetInteger("Direction", dir);
                }
            }

            //if ((horizontal == 0 && vertical == 0) && !(isAttacking) && !(isHurt) && !(isUsing))
            if(dir == 0 && !isAttacking && !isHurt && !isUsing)
            {
                animator.speed = 0.0f;
            }
            else
            {
                animator.speed = 1.0f;
            }
        }
    }

    IEnumerator stopUse()
    {
        yield return (new WaitForSeconds(1.1f/6.0f));
        isUsing = false;
    }

    public void useItem()
    {
        animator.SetTrigger("UseItem");
        animator.speed = 1.0f;
        isUsing = true;
        StartCoroutine(stopUse());
    }
    public bool attack()
    {
        if(isAttacking) { return (false); }
        animator.speed = 1.0f;
        isAttacking = true;
        animator.SetTrigger("Attack");
        animator.SetInteger("Direction",0);
        StartCoroutine(stopAttack());
        return (true);
    }

    public void damaged()
    {
        isHurt = true;
        animator.SetTrigger("Hurt");
        StartCoroutine(stopIsHurt());
    }

    IEnumerator stopIsHurt()
    {
        yield return (new WaitForSeconds(0.1f));
        isHurt = false;
    }

    IEnumerator stopAttack()
    {
        yield return (new WaitForSeconds(0.25f));
        isAttacking = false;
    }

}
