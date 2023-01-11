using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputToAnimator : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("xInput", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("yInput", Input.GetAxisRaw("Vertical"));

        if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            animator.speed = 0.0f;
        }
        else
        {
            animator.speed = 1.0f;
        }
    }

}
