using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;

    public GameObject bullet;    

    public float velocity = 5, realVelocity, realVelocityY, velocityX = 1, velocityY = 1;

    Vector3 bulletPosition = new Vector3(0, 0, 0);

    public GameManager gameManager;

    public void SetDirection(bool verificar)
    {
        if (verificar == true) { realVelocity = velocity; realVelocityY = 0; }
        if (verificar == false) { realVelocity = -velocity; realVelocityY = 0; }
    }

    //Arriba
    public void SetDirectionUp(bool verificar)
    {
        if (verificar == true) { realVelocity = velocity; realVelocityY += 1; }
        if (verificar == false) { realVelocity = -velocity; realVelocityY += 1; }
    }
    //Abajo
    public void SetDirectionDown(bool verificar)
    {
        if (verificar == true) { realVelocity = velocity; realVelocityY -= 1; }
        if (verificar == false) { realVelocity = -velocity; realVelocityY -= 1; }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Destroy(this.gameObject, 5);       
    }

    void Update()
    {
        rb.velocity = new Vector2(realVelocity, realVelocityY);
        flip();
        if (Input.GetKeyUp(KeyCode.Z))
        {
            GenerarMasBalas();
            gameManager.BalasRestantes(2);
        }
    }
    public void flip()
    {
        if (rb.velocity.x < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    public void GenerarMasBalas()
    {        
        if (sr.flipX == false)
        {
            BalaUp(velocityX, velocityY, true, bullet);
            BalaDown(velocityX, -velocityY, true, bullet);

        }
        else
        {
            BalaUp(-velocityX, velocityY, true, bullet);
            BalaDown(-velocityX, -velocityY, true, bullet);
        }
        
    }
    public void CrearBala(float posicionX, float posicionY, bool vrf, GameObject bala)
    {
        bulletPosition = transform.position + new Vector3(posicionX, posicionY, 0);
        var gb = Instantiate(bala, bulletPosition, Quaternion.identity);
        var controller = gb.GetComponent<KunaiController>();
        controller.SetDirection(vrf);
    }

    /*public void Bala(float posicionX, float posicionY, bool vrf, GameObject bala) { 
    }*/

    public void BalaUp(float posicionX, float posicionY, bool vrf, GameObject bala)
    {
        bulletPosition = transform.position + new Vector3(posicionX, posicionY, 0);
        var gb = Instantiate(bala, bulletPosition, Quaternion.identity);
        var controller = gb.GetComponent<KunaiController>();
        controller.SetDirectionUp(vrf);        
    }

    public void BalaDown(float posicionX, float posicionY, bool vrf, GameObject bala)
    {
        bulletPosition = transform.position + new Vector3(posicionX, posicionY, 0);
        var gb = Instantiate(bala, bulletPosition, Quaternion.identity);
        var controller = gb.GetComponent<KunaiController>();
        controller.SetDirectionDown(vrf);
    }
}
