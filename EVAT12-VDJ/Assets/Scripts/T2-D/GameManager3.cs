using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;


//Para el nivel
public class GameManager3 : MonoBehaviour
{
    public Text livesText;
    public Text balasText;
    public Text coinsText;
    public Text zombiesCantidadText;

    public int balas;
    public int lives;
    public int coins;
    public int zombiesCant;
    public int personaje;
    public int score;

    public bool choque = false;

    public int puntosRobot;
    public int puntosNinja;
    public int accPuntos;

    public GameObject personaje1;
    public GameObject personaje2;
    private GameObject personajeElegido; //es el que se crea

    public const int SCENE_T2D2 = 3;
    public const int SCENE_SCORE = 4;
    public const int T2D3 = 5;

    void Start()
    {
        balas = 20;
        lives = 2;
        coins = 0;
        zombiesCant = 0;
        personaje = 0;
        score = 0;

        LoadGame();
        LoadCharacterGame();
       

        if(personaje == 0)
        {
            personajeElegido = personaje1;
            personaje1.transform.position =  new Vector3(-3.268111f, 13.89708f, 0);
            Destroy(personaje2);
        }
        else
        {
            personajeElegido = personaje2;
            personaje2.transform.position = new Vector3(-3.268111f, 13.89708f, 0);
            Destroy(personaje1);
        }
        //Crear al personaje

        PrintInScreenBullet();
        PrintLivesInScreen();
        PrintCoinsT1InScreen();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveGameT2()
    {
        var filePath = Application.persistentDataPath + "/saveT2D.dat";
        FileStream file;
        if (File.Exists(filePath)) file = File.OpenWrite(filePath);
        else file = File.Create(filePath);
        DataManager data = new DataManager();
        data.CoinsT1 = coins;
        data.Balas = balas;
        data.Vidas = lives;
        data.Zombies = zombiesCant;
        data.Personaje = personaje;
        data.Score = score;
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadCharacterGame()
    {
        var filePath = Application.persistentDataPath + "/saveT2D.dat";
        FileStream file;
        if (File.Exists(filePath)) file = File.OpenRead(filePath);
        else
        {
            UnityEngine.Debug.LogError("No se encontro el archivo");
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        DataManager data = (DataManager)bf.Deserialize(file);
        file.Close();

        UnityEngine.Debug.Log(data.Score);
        //Para llamar a los datos guardados
        personaje = data.Personaje;

        UnityEngine.Debug.Log("Carga");

    }
    public void LoadGame()
    {
        var filePath = Application.persistentDataPath + "/saveT2D.dat";
        FileStream file;
        if (File.Exists(filePath)) file = File.OpenRead(filePath);
        else
        {
            UnityEngine.Debug.LogError("No se encontro el archivo");
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        DataManager data = (DataManager)bf.Deserialize(file);
        file.Close();

        UnityEngine.Debug.Log(data.Score);
        //Para llamar a los datos guardados
        lives = data.Vidas;
        PrintLivesInScreen();
        coins = data.CoinsT1;
        PrintCoinsT1InScreen();
        zombiesCant = data.Zombies;
        PrintZombieInScreen();
        balas = data.Balas;
        PrintInScreenBullet();
        personaje = data.Personaje;

        UnityEngine.Debug.Log("Carga");

    }

    public void empezarCeroT2()
    {
        var filePath = Application.persistentDataPath + "/saveT2D1.dat";
        FileStream file;
        if (File.Exists(filePath)) file = File.OpenWrite(filePath);
        else file = File.Create(filePath);

        DataManager data = new DataManager();
        data.Zombies = 0;
        data.CoinsT1 = 0;
        data.Balas = 10;
        data.Vidas = 3;
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public void RobotGanaPuntos(int accPuntos)
    {
        puntosRobot += accPuntos;
        PrintZombieInScreen();
    }

    public void NinjaGanaPuntos(int accPuntos)
    {
        puntosNinja += accPuntos;
        PrintZombieInScreen();
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
        zombiesCantidadText.text = "Puntos por Zombie Muerto: " + zombiesCant;
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

    public void regresarAlMenu()
    {
        SceneManager.LoadScene(T2D3);
    }
}
