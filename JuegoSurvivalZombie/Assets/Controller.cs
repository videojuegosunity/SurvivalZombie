using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public void CambiarEscena(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    public void Salir()
    {
        Debug.Log("QUIT GAME");
        Application.Quit();
    }
}
