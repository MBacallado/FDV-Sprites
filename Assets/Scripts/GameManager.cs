using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Animator animator;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("x"))
        {
            animator.SetBool("activate", true);
        } else
        {
            animator.SetBool("activate", false);
        }
    }
}
