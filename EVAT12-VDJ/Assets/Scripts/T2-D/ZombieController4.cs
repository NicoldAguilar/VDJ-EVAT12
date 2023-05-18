using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController4 : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D c;
    Animator animator;

    public int velocity = -1, vidasZombie = 2;


    const int ANIMATION_IDDLE = 0;
    const int ANIMATION_DIE = 1;

    public GameManager3 gameManager;
    public GameSpriteManager spriteManager;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        c = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager3>(); //Para saber a que objeto se refiere (buscar e igualar)
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velocity, rb.velocity.y);
        ChangeAnimation(ANIMATION_IDDLE);
        if (vidasZombie <= 0)
        {
            Destroy(this.gameObject);
            gameManager.ZombieMuerto(1);
            if(gameManager.personaje == 0) gameManager.RobotGanaPuntos(5);
            else if(gameManager.personaje == 1) gameManager.NinjaGanaPuntos(5);
            gameManager.SaveGameT2();
        }
        if (gameManager.lives == 0)
        {
            Debug.Log("Choco el zombie");
            ChangeAnimation(ANIMATION_DIE);
            velocity = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Kunai")
        {
            Debug.Log(vidasZombie);
            Destroy(other.gameObject); //destruye kunai
            quitarVidasZombie(1);
        }
    }

    public void quitarVidasZombie(int perder)
    {
        vidasZombie -= perder;
    }

    private void ChangeAnimation(int animation)
    {
        animator.SetInteger("EstadoZombie", animation);
    }
}
