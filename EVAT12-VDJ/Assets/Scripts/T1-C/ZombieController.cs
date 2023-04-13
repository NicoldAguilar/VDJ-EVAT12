using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public int velocity = -1;
    Rigidbody2D rb;
    Collider2D c;
    bool movement = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        c = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

       rb.velocity = new Vector2(velocity, rb.velocity.y);  
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Hero")
        {            
            Debug.Log("Choco el zombie");
            velocity = 0;
            Debug.Log(velocity);
        }
    }
}
