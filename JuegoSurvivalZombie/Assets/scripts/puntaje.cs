using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class puntaje : MonoBehaviour
{
	public Text puntajeText;
    // Start is called before the first frame update
    void Start()
    {
        puntajeText.text  =  PlayerPrefs.GetInt("score").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        puntajeText.text  =  PlayerPrefs.GetInt("score").ToString();
    }
}
