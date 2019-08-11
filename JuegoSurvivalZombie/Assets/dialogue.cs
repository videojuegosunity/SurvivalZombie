using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogue : MonoBehaviour
{
    public Text txt;
    public string texto;

    // Start is called before the  first frame update
    void Start()
    {
        StartCoroutine(ditado(texto));
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator ditado(string frase)
    {
        int letra = 0;
        txt.text = "";
        while (letra < frase.Length)
        {
            txt.text += frase[letra];
            letra += 1;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
