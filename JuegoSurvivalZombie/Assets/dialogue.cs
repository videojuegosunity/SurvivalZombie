using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogue : MonoBehaviour
{
    public Text txt;
    public string texto;
    public bool blink_button;
    bool on_button;
    public int duration_blink_button;
    // public UnityEngine.UI.Button button_start;
    // public GameObject button_start;
    public UnityEngine.UI.Button button_start;
    public AudioClip[] rocochet;
    public AudioSource audio1;
    public AudioSource audio2;
    int count=0;
    // public UnityEngine.UI.Image button_start3;
    // public Transform button_start4;
    // public RectTransform button_start5;

    // Start is called before the  first frame update
    void Start()
    {
        StartCoroutine(ditado(texto));
        blink_button = false;
        // blink_button = true;
        on_button = true;
        duration_blink_button = 15;
        count=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(blink_button == false)
        {
            // gameObject.GetComponent<AudioSource>().clip = audio1;
        }
        if(blink_button == true)
        {
            // gameObject.GetComponent<AudioSource>().clip = audio2;
            blinkButton();
        }
    }

    IEnumerator ditado(string frase)
    {
        // gameObject.GetComponent<AudioSource>().clip = audio1;
        // audio1.clip = rocochet[1];
        // audio1.Play ();
        int letra = 0;
        txt.text = "";
        while (letra < frase.Length)
        {
            txt.text += frase[letra];
            letra += 1;
            yield return new WaitForSeconds(0.02f);
        }
        blink_button = true;
        // audio1.Pause();
    }

    public void blinkButton(){
        if(on_button==true)
        {
            var colors = button_start.colors;
            colors.normalColor = Color.green;
            button_start.colors = colors;
            on_button = false;
        }
        else
        {
            var colors = button_start.colors;
            colors.normalColor = Color.white;
            button_start.colors = colors;
            on_button = true;
        }
        while(count<duration_blink_button)
        {
            count ++;
        }
        if(count>=duration_blink_button){
            count=0;
        }
        // UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject (null);
    }
}
