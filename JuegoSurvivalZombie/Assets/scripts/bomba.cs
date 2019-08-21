using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomba : MonoBehaviour{
	public GameObject particulas;
    public AudioSource m_my_audio_source_machine;
    public bool active;
    
	Rigidbody2D rb;
    // Start is called before the first frame update
    void Start(){
        //m_my_audio_source_machine = GetComponent<AudioSource>();
        m_my_audio_source_machine.Play();
    }

    // Update is called once per frame
    void Update(){
        //m_my_audio_source_machine.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Player"){
            
        }else if (collision.gameObject.tag == "zombie"){
        
        }else{
        	var position = transform.position;
        	//Instantiate(particulas, new Vector3(position.x, position.y, -10), Quaternion.identity);
        	Destroy(gameObject);
        }
    }

}


