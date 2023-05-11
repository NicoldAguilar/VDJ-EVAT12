using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using System.Diagnostics;

public class GameSpriteManager : MonoBehaviour
{
    public int personaje = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //guardar Personaje
    public void SaveGameT2()
    {
        var filePath = Application.persistentDataPath + "/saveT2D.dat";
        FileStream file;
        if (File.Exists(filePath)) file = File.OpenWrite(filePath);
        else file = File.Create(filePath);
        DataManager data = new DataManager();
        data.CoinsT1 = 0;
        data.Balas = 20;
        data.Vidas = 3;
        data.Zombies = 0;
        data.Personaje = personaje;
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }
}
