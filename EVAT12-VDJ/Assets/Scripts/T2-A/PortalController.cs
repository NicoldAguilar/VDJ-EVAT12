using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public GameObject zombie;
    float cont = 0;

    public GameManager2 gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.lives == 0)
        {
            return; //devuelve null
        }
        if (gameManager.zombiesCant == 5)
        {
            return;
        }
        cont += Time.deltaTime;
        if (cont >= 3) { CrearZombie(-1); cont = 0; }
    }

    public void CrearZombie(int posicion)
    {
        var bulletPosition = new Vector3(0, 0, 0);
        bulletPosition = transform.position + new Vector3(posicion, 0, 0);
        var gb = Instantiate(zombie, bulletPosition, Quaternion.identity);
        var controller = gb.GetComponent<ZombieController2>();
    }
}
