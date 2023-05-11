using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionCharacterController : MonoBehaviour
{
    public Sprite personaje1;
    public Sprite personaje2;

    public const int T2D1 = 2;

    SpriteRenderer sr;

    public GameSpriteManager spriteManager;

    int seleccionarPersonaje = 0;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeCharacter()
    {
        if(seleccionarPersonaje == 0)
        {
            sr.sprite = personaje1;
            seleccionarPersonaje = 1;
        }
        else
        {
            sr.sprite = personaje2;
            seleccionarPersonaje = 0;
        }
    }
    public void SelectCharacter()
    {
        spriteManager.personaje = seleccionarPersonaje;
        spriteManager.SaveGameT2();
        SceneManager.LoadScene(T2D1);
    }
}
