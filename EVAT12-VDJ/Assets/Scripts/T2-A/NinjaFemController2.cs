using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaFemController2 : MonoBehaviour
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

        if (status == true)
        {
            if (Input.GetKeyUp(KeyCode.C))
            {
                ChangeAnimation(ANIMATION_THROW);
                DispararKunais();
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
            }
            //Debug.Log(saltos);
            if (hongo == true && enelaire == true && Input.GetKeyDown(KeyCode.D))
            {
                enelaire = false;
                rb.AddForce(new Vector2(0, (jumpForce + 2)), ForceMode2D.Impulse);
                hongo = false;
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
            status = false;           
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
        if (other.gameObject.tag == "Hongo")
        {
            Debug.Log("Atrapaste el hongo");
            hongo = true;
            Destroy(other.gameObject);
        }
        Debug.Log(checkpointverif);
        if (other.gameObject.tag == "Checkpoint1" && checkpointverif == true)
        {
            Debug.Log("Checkpoint 1 funciona");
            lastCheckpointPosition = transform.position;
        }
        else if (other.gameObject.tag == "Checkpoint2")
        {
            Debug.Log("Checkpoint 2 funciona");
            lastCheckpointPosition = transform.position;//guarda la ultima posición del trasform
            checkpointverif = false;
        }
        if (other.gameObject.tag == "Potenciador1")
        {
            velocity = velocity + 2;
            Debug.Log(velocity);
        }
        if (other.gameObject.tag == "Zombie")
        {
            Destroy(other.gameObject);            
        }
    }
    private void ChangeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
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
        }
        else
        {
            CrearBala(-1, 0, false, bullet);
        }
    }
}
