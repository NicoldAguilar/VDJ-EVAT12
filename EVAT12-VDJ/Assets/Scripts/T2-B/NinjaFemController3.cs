using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NinjaFemController3 : MonoBehaviour
{
    public float velocity = 5, jumpForce = 3;

    Rigidbody2D rb; //gravedad inicial
    SpriteRenderer sr;
    Animator animator;
    Vector3 lastCheckpointPosition;

    public GameObject bullet;
    public GameObject katana;

    //Constantes para los estados de las animaciones 
    const int ANIMATION_IDDLE = 0;
    const int ANIMATION_RUN = 1;
    const int ANIMATION_THROW = 2;
    const int ANIMATION_SLIDE = 3;
    const int ANIMATION_DIE = 4;
    const int ANIMATION_ATTACK = 5;
    const int ANIMATION_JUMP = 6;

    //Banderas
    bool status = true;
    bool saltos = true;
    bool saltos3 = true;
    bool hongo = false;
    bool enelaire = false;
    bool checkpointverif = true;
    bool cambio = false;

    Vector3 bulletPosition = new Vector3(0, 0, 0);

    public AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip dieSound;
    public AudioClip coinSound;
    public AudioClip bulletSound;

    public Text texto;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Ha iniciado el juego");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        ChangeAnimation(ANIMATION_IDDLE);

        if (status == true)
        {
            if (Input.GetKeyUp(KeyCode.C))
            {
                ChangeAnimation(ANIMATION_THROW);
                if(gameManager.balas > 0)
                {
                    DispararKunais();
                }
                else
                {
                    Debug.Log("No hay más balas disponibles");
                }
                
            }
            if (Input.GetKeyUp(KeyCode.A))
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
            //saltos

            if (Input.GetKeyUp(KeyCode.Space) && saltos == true)
            {
                ChangeAnimation(ANIMATION_JUMP);
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                audioSource.PlayOneShot(jumpSound);
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
                audioSource.PlayOneShot(dieSound);
            }
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

        if (other.gameObject.tag == "Coin")
        {
            gameManager.GanaMonedasT1(1);
            audioSource.PlayOneShot(coinSound);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "MasBalas")
        {
            gameManager.GanaMasBalas(5);
            audioSource.PlayOneShot(bulletSound);
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
        animator.SetInteger("Estado", animation);
    }
}
