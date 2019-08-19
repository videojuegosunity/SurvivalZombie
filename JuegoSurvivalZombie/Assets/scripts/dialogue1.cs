using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogue1 : MonoBehaviour
{
    public Text txt;
    public string texto;
    AudioSource m_my_audio_source_machine;
    bool change_btn_color;
    public Button init_btn;
    // var colors = GetComponent<Button> ().colors;
    // colors.normalColor = Color.red;
    // public GetComponent<Button> ().colors = colors;
    // Start is called before the  first frame update
    void Start()
    {

        m_my_audio_source_machine = GetComponent<AudioSource>();
        change_btn_color = false;
        StartCoroutine(ditado(texto));
        // btn_color = init_btn.colors;

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ditado(string frase)
    {

        int letra = 0;
        txt.text = "";
        m_my_audio_source_machine.Play();
        while (letra < frase.Length)
        {
            txt.text += frase[letra];
            letra += 1;
            yield return new WaitForSeconds(0.02f);
        }
        m_my_audio_source_machine.Stop();
        // change_btn_color = true;
        change_init_btn_color();

    }

    void change_init_btn_color(){
        var colors = init_btn.colors;
        colors.normalColor = Color.green;
        init_btn.colors = colors;
    }

        // var colors = GetComponent<Button> ().colors;
        // colors.normalColor = Color.red;
        // GetComponent<Button> ().colors = colors;
}
