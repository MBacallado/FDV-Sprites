using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private bool direction = false;
    private bool isGrounded = true;
    private Animator animator;
    public Animator otherAnimator;
    private float umbral = 2f;
    private float dist;

    public GameObject objetive;

    private void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log("DIST: " + dist);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Jump();
            ChangeDirection();
        }

        if(Input.GetKey("m"))
        {
            animator.SetBool("moonwalker", true);
        } else
        {
            animator.SetBool("moonwalker", false);
        }

        if(objetive.activeSelf)
        {
            dist = Vector2.Distance(transform.position, objetive.transform.position);

            if (dist < umbral)
            {
                otherAnimator.SetBool("run", true);
            }
            else
            {
                otherAnimator.SetBool("run", false);
            }
        } 
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        float h = Input.GetAxis("Horizontal");

        animator.SetFloat("speed", h);

        if (h == 0)
        {
            animator.SetBool("run", false);
        } else
        {
            animator.SetBool("run", true);
        }
        
        this.transform.Translate(new Vector2(h,0) * speed * Time.deltaTime);
    }

    void Jump()
    {
        if (isGrounded)
        {
            StartCoroutine(Jumping());
        }
    }

    IEnumerator Jumping()
    {
        isGrounded = false;
        float count = 0;
        while (count <= 0.5f)
        {
            count += Time.deltaTime;
            Vector2 newPosition = new Vector2(transform.position.x, transform.position.y + 0.5f);
            transform.position = Vector2.Lerp(transform.position, newPosition, 1 * Time.deltaTime);
            yield return null;
        }

        count = 0;
        while (count <= 0.5f)
        {
            count += Time.deltaTime;
            Vector2 newPosition = new Vector2(transform.position.x, transform.position.y - 0.5f);
            transform.position = Vector2.Lerp(transform.position, newPosition, 1 * Time.deltaTime);
            yield return null;
        }
        isGrounded = true;
        yield return null;
    }

    void ChangeDirection()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.flipX = direction = !direction;
    }
}
