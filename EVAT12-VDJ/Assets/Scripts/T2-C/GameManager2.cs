using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    public Text livesText;
    public Text balasText;
    public Text coinsText;
    public Text zombiesCantidadText;

    public int balas;
    public int lives;
    public int coins;
    public int zombiesCant;

    public bool choque = false;

    public GameObject zombie;

    //public const int SCENE_T1C = 6;

    void Start()
    {
        balas = 5;
        lives = 2;
        coins = 0;
        zombiesCant = 0;

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
        if (lives == 0)
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

    public int CantidadZombies()
    {
        return zombiesCant;
    }

    public void ZombieMuerto(int zombietieso)
    {
        zombiesCant += zombietieso;
        PrintZombieInScreen();
    }

    public void GanaMasBalas(int masBalas)
    {
        balas += masBalas;
        PrintInScreenBullet();
    }

    private void PrintZombieInScreen()
    {
        zombiesCantidadText.text = "Zombies Destruidos: " + zombiesCant;
    }

    private void PrintInScreenBullet()
    {
        balasText.text = "Balas Restantes: " + balas;
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
