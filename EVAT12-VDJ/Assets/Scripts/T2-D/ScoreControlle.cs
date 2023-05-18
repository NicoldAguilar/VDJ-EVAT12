using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class ScoreControlle : MonoBehaviour
{
    public Text zombiesCantidadText;
    public int zombiesCant;
    public GameSpriteManager spriteManager;
    public SelectionCharacterController per;

    public const int T2D1 = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LoadGame();
        PrintZombieInScreen();
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
        zombiesCant = data.Zombies;
        PrintZombieInScreen();

        UnityEngine.Debug.Log("Carga");

    }

    private void PrintZombieInScreen()
    {
        zombiesCantidadText.text = "Puntos por Zombie Muerto: " + zombiesCant;
    }

    public void SelectCharacterBTN2()
    {
        Debug.Log("hola");
        if (zombiesCant >= 5)
        {           
            spriteManager.personaje = per.seleccionarPersonaje;
            spriteManager.SaveGameT2();
            SceneManager.LoadScene(T2D1);
        }

    }
    public void SelectCharacterBTN3()
    {
        if (zombiesCant == 10)
        {
            spriteManager.personaje = per.seleccionarPersonaje;
            spriteManager.SaveGameT2();
            SceneManager.LoadScene(T2D1);
        }
    }

}
