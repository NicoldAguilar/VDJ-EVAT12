using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RobotController2 : MonoBehaviour
{
    public float velocity = 5, jumpForce = 3;

    Vector3 lastCheckpointPosition;
    Vector3 bulletPosition = new Vector3(0, 0, 0);

    public GameObject bullet;

    Rigidbody2D rb; //gravedad inicial
    SpriteRenderer sr;
    Animator animator;

    //Banderas
    bool status = true;
    bool saltos = true;
    bool saltos3 = true;
    bool enelaire = false;
    bool checkpointverif = true;
    bool cambio = false;
    bool escena = false;

    //Constantes para los estados de las animaciones 
    const int ANIMATION_IDDLE = 0;
    const int ANIMATION_RUN = 1;
    const int ANIMATION_RUNSHOOT = 2;
    const int ANIMATION_MEELE = 3;
    const int ANIMATION_SHOOT = 4;
    const int ANIMATION_SLIDE = 5;
    const int ANIMATION_DIE = 6;
    const int ANIMATION_JUMP = 7;

    public GameManager3 gameManager;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Ha iniciado el juego");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager3>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        ChangeAnimation(ANIMATION_IDDLE);

        if (status == true)
        {
            if (Input.GetKey(KeyCode.X))
            {
                ChangeAnimation(ANIMATION_DIE);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                ChangeAnimation(ANIMATION_SHOOT);
                if (gameManager.balas > 0)
                {
                    DispararKunais();
                }
                else
                {
                    Debug.Log("No hay más balas disponibles");
                }
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
            if (Input.GetKeyUp(KeyCode.Space) && saltos == true)
            {
                ChangeAnimation(ANIMATION_JUMP);
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
        else
        {
            ChangeAnimation(ANIMATION_DIE);
        }           
    }
    void OnCollisionEnter2D(Collision2D other) //recien toca
    {
        if (other.gameObject.tag == "DarkHole")
        {
            if (lastCheckpointPosition != null)
            {
                transform.position = lastCheckpointPosition;
            }
        }
        if (other.gameObject.tag == "Zombie")
        {
            gameManager.PerderVida(1);
            if (gameManager.lives == 0)
            {
                status = false;
            }
        }
        if (other.gameObject.tag == "MasBalas")
        {
            gameManager.GanaMasBalas(5);
            Destroy(other.gameObject);
        }
    }
    void OnCollisionStay2D(Collision2D collision) //siempre toca
    {
        saltos = true;
        enelaire = false;
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        saltos = false; //salte 1 vez
        enelaire = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ataque con Katana
        if (other.gameObject.tag == "Zombie")
        {
            Destroy(other.gameObject);
            gameManager.ZombieMuerto(1);
        }


        if (other.gameObject.tag == "MasBalas")
        {
            gameManager.GanaMasBalas(5);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "cambioEscena")
        {
            escena = true;
            if (escena == true && gameManager.coins == 10)
            {
                SceneManager.LoadScene(GameManager3.SCENE_T2D2);
                gameManager.LoadGame();               
            }
            else
            {
                Debug.Log("Aún no cumples los requisitos");
            }
        }
    }

    public void CambiarScene()
    {
        if (escena == true  && gameManager.coins == 10)
        {
            SceneManager.LoadScene(GameManager3.SCENE_T2D2);
        }
        else
        {
            Debug.Log("Aún no cumples los requisitos");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            gameManager.GanaMonedasT1(1);
            Destroy(other.gameObject);
        }
    }

    public void CrearBala(int posicionX, int posicionY, bool vrf, GameObject bala)
    {
        bulletPosition = transform.position + new Vector3(posicionX, posicionY, 0);
        var gb = Instantiate(bala, bulletPosition, Quaternion.identity);
        var controller = gb.GetComponent<KunaiController>();
        controller.SetDirection(vrf);
    }

    public void DispararKunais() //getKeyUp
    {
        //Kunai:
        if (sr.flipX == false)
        {
            CrearBala(1, 0, true, bullet);
            gameManager.BalasRestantes(1);
        }
        else
        {
            CrearBala(-1, 0, false, bullet);
            gameManager.BalasRestantes(1);
        }
    }


    private void ChangeAnimation(int animation)
    {
        //Estado en 1 = pasa de iddle a correr
        //Estado en 0 = De correr a iddle
        animator.SetInteger("Estado", animation);
    }
}
