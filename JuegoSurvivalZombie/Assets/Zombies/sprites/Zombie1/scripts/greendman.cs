using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greendman : MonoBehaviour{
    private float speed = 2.5f;
    private Transform obj_trasform;
    bool choque = false;
    Rigidbody2D rb;
    float limiteDer;
    public Animator anim;
    float limiteIzq;
    float direccion = -1;
    bool destroy = false;

    private void Awake(){
        obj_trasform = this.transform;
        gameObject.transform.localScale = new Vector3(1.0F, 1.0F, 1f);
        anim = GetComponent<Animator> ();
    }
    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        var posicionInicial = gameObject.GetComponent<Transform>().position;
        //rb.position = posicionInicial;
        limiteDer = posicionInicial.x + 3f;
        limiteIzq = posicionInicial.x - 3f;
}
    // Update is called once per frame
    void Update(){
        CheckDestroy(destroy);
        transform.localScale = new Vector3(1 * direccion,1,1);
        rb.velocity = new Vector2(speed*direccion, 0.0f);
        if(transform.position.x < limiteIzq && !choque){
            direccion = 1;
        }
        if(transform.position.x > limiteDer && !choque){
            direccion = -1;
        }
    }
    public void CheckDestroy(bool destroy){
        
		if (destroy == true){
           Destroy(gameObject);
            //anim.SetBool ("dead", true);
        }
        var position = transform.position;
        if(position.y < -15f){
             Destroy(gameObject);
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Player")
        {
            //Destroy(gameObject);
        }
        if (collision.gameObject.tag == "zombie")
        {
            choque = true;
            direccion = direccion * -1 ;
        }
    }

    private void OnCollisionExit2D(Collision2D collision){
        if (collision.gameObject.tag == "Player")
        {
            //Destroy(gameObject);
        }
        if (collision.gameObject.tag == "zombie")
        {
            choque = false;
        }
    }

}
