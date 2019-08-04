using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greendman : MonoBehaviour{
    //referencias
    private Transform obj_trasform;
    Rigidbody2D rb;
    public Animator anim;
    public GameObject particulas;

    public enum Modo {dead, normal};
    public Modo modo;
    private float speed = 2.5f;
    bool choque = false;
    float limiteDer;
    float limiteIzq;
    float direccion = -1;
    bool destroy = false;
    float countDead = 60;
    
    
    private void Awake(){
        obj_trasform = this.transform;
        gameObject.transform.localScale = new Vector3(1.0F, 1.0F, 1f);
        anim = GetComponent<Animator> ();
    }

    void Start(){
        modo = Modo.normal;
        rb = GetComponent<Rigidbody2D>();
        var posicionInicial = gameObject.GetComponent<Transform>().position;
        limiteDer = posicionInicial.x + 3f;
        limiteIzq = posicionInicial.x - 3f;
}
    // Update is called once per frame
    void Update(){
        if(modo != Modo.dead){
            CheckDestroy(destroy);
            transform.localScale = new Vector3(1 * direccion,1,1);
            rb.velocity = new Vector2(speed*direccion, 0.0f);
            if(transform.position.x < limiteIzq && !choque){
                direccion = 1;
            }
            if(transform.position.x > limiteDer && !choque){
                direccion = -1;
            }
        }else{
            countDead--;
            if(countDead == 0){
                Destroy(gameObject);
            }
        }
    }

    public void CheckDestroy(bool destroy){
		if (destroy == true){
           //Destroy(gameObject);
            //anim.SetBool ("dead", true);
        }
        var position = transform.position;
        if(position.y < -15f){
             Destroy(gameObject);
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Player" && modo != Modo.dead){
            if (collision.gameObject.GetComponent<Demo>().ataque()){
                morir();  
            }else{
                anim.SetBool("attack", true);
            }
        }
        if (collision.gameObject.tag == "zombie" && modo != Modo.dead){
            choque = true;
            direccion = direccion * -1 ;
        }
        if (collision.gameObject.tag == "bomba" && modo != Modo.dead){
            var position = transform.position;
            Instantiate(particulas, new Vector3(position.x, position.y, -10), Quaternion.identity);
            morir();
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision){
        if (collision.gameObject.tag == "Player"  && modo != Modo.dead){
            anim.SetBool ("attack", false);
        }
        if (collision.gameObject.tag == "zombie" && modo != Modo.dead){
            choque = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision){
        if (collision.gameObject.tag == "Player" && modo != Modo.dead){
            anim.SetBool("attack", true);
        }
    }

    private void morir(){
        anim.SetBool("attack", false);
        anim.SetBool("dead", true);
        rb.constraints = RigidbodyConstraints2D.None;
        modo = Modo.dead;
    }
}
