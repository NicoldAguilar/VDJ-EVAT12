using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text livesText;
    public Text balasText;
    public Text coinsText;

    public int balas;
    public int lives;
    public int coins;

    public bool choque = false;

    void Start()
    {
        balas = 20;
        lives = 3;
        coins = 0;

        PrintInScreenBullet();
        PrintLivesInScreen();
        PrintCoinsT1InScreen();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BalasRestantes(int bullet)
    {
        balas -= bullet;
        PrintInScreenBullet();
    }

    public void PerderVida(int vidasPerdidas)
    {
        lives -= vidasPerdidas;
        if(lives == 0)
        {
            choque = true;
        }
        PrintLivesInScreen();
    }

    public void GanaMonedasT1(int moneditas)
    {
        coins += moneditas;
        PrintCoinsT1InScreen();
    }

    private void PrintInScreenBullet()
    {
        balasText.text = "Balas Restantes: " + balas + "/20";
    }

    public void PrintLivesInScreen()
    {
        livesText.text = "Vida: " + lives;
    }

    private void PrintCoinsT1InScreen()
    {
        coinsText.text = "Monedas T1: " + coins;
    }
}
