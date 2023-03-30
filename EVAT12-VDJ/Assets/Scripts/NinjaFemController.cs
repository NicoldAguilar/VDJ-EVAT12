using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaFemController : MonoBehaviour
{
    public float velocity = 5;

    Rigidbody2D rb; //gravedad inicial
    SpriteRenderer sr;
    Animator animator;

    //Constantes para los estados de las animaciones 
    const int ANIMATION_IDDLE = 0;
    const int ANIMATION_RUN = 1;
    const int ANIMATION_THROW = 2;
    const int ANIMATION_SLIDE = 3;
    const int ANIMATION_DIE = 4;
    const int ANIMATION_ATTACK = 5;

    bool status = true;

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

        if(status == true)
        {
            if (Input.GetKey(KeyCode.C))
            {
                ChangeAnimation(ANIMATION_THROW);
            }

            if (Input.GetKey(KeyCode.A))
            {
                ChangeAnimation(ANIMATION_ATTACK);
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
        if (Input.GetKey(KeyCode.X)){
            status = false;
            if(status == false)
            {
                ChangeAnimation(ANIMATION_DIE);
            }
        }
    }
    private void ChangeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
    }
}
