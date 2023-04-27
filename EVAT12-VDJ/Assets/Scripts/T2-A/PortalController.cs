using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public GameObject zombie;
    float cont = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
