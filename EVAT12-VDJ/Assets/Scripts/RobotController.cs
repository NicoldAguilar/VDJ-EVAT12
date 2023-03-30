using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public float velocity = 5;

    Rigidbody2D rb; //gravedad inicial
    SpriteRenderer sr;
    Animator animator;

    //Constantes para los estados de las animaciones 
    const int ANIMATION_IDDLE = 0;
    const int ANIMATION_RUN = 1;
    const int ANIMATION_RUNSHOOT = 2;
    const int ANIMATION_MEELE = 3;
    const int ANIMATION_SHOOT = 4;
    const int ANIMATION_SLIDE = 5;
    const int ANIMATION_DIE = 6;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Ha iniciado el juego");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        ChangeAnimation(ANIMATION_IDDLE);

        if (Input.GetKey(KeyCode.X))
        {
            ChangeAnimation(ANIMATION_DIE);
        }

        if (Input.GetKey(KeyCode.C))
        {
            ChangeAnimation(ANIMATION_SHOOT);
        }

        if (Input.GetKey(KeyCode.M))
        {
            ChangeAnimation(ANIMATION_MEELE);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y);
            sr.flipX = false;
            ChangeAnimation(ANIMATION_RUN);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            sr.flipX = true;
            ChangeAnimation(ANIMATION_RUN);
        }

        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.C))
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y);
            sr.flipX = false;
            ChangeAnimation(ANIMATION_RUNSHOOT);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.C))
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            sr.flipX = true;
            ChangeAnimation(ANIMATION_RUNSHOOT);
        }
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y);
            sr.flipX = false;
            ChangeAnimation(ANIMATION_SLIDE);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            sr.flipX = true;
            ChangeAnimation(ANIMATION_SLIDE);
        }
    }

    private void ChangeAnimation(int animation)
    {
        //Estado en 1 = pasa de iddle a correr
        //Estado en 0 = De correr a iddle
        animator.SetInteger("Estado", animation);
    }
}