using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController2 : MonoBehaviour
{
    
    Rigidbody2D rb;
    Collider2D c;
    Animator animator;

    public int velocity = -1, vidas = 1;

    bool movement = true;

    const int ANIMATION_IDDLE = 0;
    const int ANIMATION_DIE = 1;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        c = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velocity, rb.velocity.y);
        ChangeAnimation(ANIMATION_IDDLE);

        if (vidas <= 0)
        {            
            Destroy(this.gameObject);            
        }

        if(gameManager.lives == 0)
        {
            Debug.Log("Choco el zombie");
            ChangeAnimation(ANIMATION_DIE);
            velocity = 0;           
        }
    }
    public void quitarVidasZombie(int perder)
    {
        vidas -= perder;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        /*if (gameManager.choque == true && other.gameObject.tag == "Hero")
        {
            Debug.Log("Choco el zombie");
            velocity = 0;
            ChangeAnimation(ANIMATION_DIE);
            Debug.Log(velocity);
        }*/
    }
    private void ChangeAnimation(int animation)
    {
        animator.SetInteger("EstadoZombie", animation);
    }
}
