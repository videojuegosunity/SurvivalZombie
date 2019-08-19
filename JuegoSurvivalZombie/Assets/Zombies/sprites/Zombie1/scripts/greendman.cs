using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greendman : MonoBehaviour{
    //referencias
    private Transform obj_trasform;
    Rigidbody2D rb;
    public Animator anim;
    public GameObject particulas;
    Collider2D m_Collider;

    public enum Modo {dead, normal};
    public Modo modo;
    private float speed = 2.5f;
    bool choque = false;
    float limiteDer;
    public float mass;
    float limiteIzq;
    float direccion = -1;
    bool destroy = false;
    public int zombieLife = 10;
    float countDead = 90;
    public int extraPoder;

    public float spriteBlinkingTimer = 0.0f;
    public float spriteBlinkingMiniDuration = 0.1f;
    public float spriteBlinkingTotalTimer = 0.0f;
    public float spriteBlinkingTotalDuration = 0.8f;
    public bool startBlinking = false;

    private void Awake(){
        obj_trasform = this.transform;
        gameObject.transform.localScale = new Vector3(1.0F, 1.0F, 1f);
        anim = GetComponent<Animator> ();
        m_Collider = GetComponent<Collider2D>();
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
            CheckDestroy();
            transform.localScale = new Vector3(1 * direccion,1,1);
            rb.velocity = new Vector2(speed*direccion, 0.0f);
            if(transform.position.x < limiteIzq && !choque){
                direccion = 1;
            }
            if(transform.position.x > limiteDer && !choque){
                direccion = -1;
            }
        }else{
            rb.velocity = new Vector2(0f, -2f);
            var position = rb.position;
             rb.mass = mass;
            //position.y += -0.04f;
            position.x += 0.01f;
            rb.position = position;
            if(this.zombieLife > 0){
                this.zombieLife = Mathf.FloorToInt(countDead *10 /60);
            }
            countDead--;
            if(countDead == 0){
                Destroy(gameObject);
            }
        }
        if (startBlinking == true){
            SpriteBlinkingEffect();
        }
    }

    public void CheckDestroy(){
        var position = transform.position;
        if(position.y < -15f){
             Destroy(gameObject);
        }
    }
    
    public bool getKilled(){
        if(zombieLife == 1){
            morir();
            this.zombieLife = 0;
            return true;
        }else if(zombieLife>1){
            this.zombieLife = this.zombieLife - 1;
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Player" && modo != Modo.dead){
            if (collision.gameObject.GetComponent<Demo>().ataque()){
                anim.SetBool("attack", false);
                if (zombieLife>1){
                    startBlinking = true;
                }
                var position = transform.position;
                Instantiate(particulas, new Vector3(position.x, position.y, -10), Quaternion.identity);
            }else{
                collision.gameObject.GetComponent<Demo>().energiaActual-=extraPoder;
                anim.SetBool("attack", true);
            }
        }
        if (collision.gameObject.tag == "zombie" && modo != Modo.dead){
            choque = true;
            direccion = direccion * -1;
        }
        if (collision.gameObject.tag == "bomba" && modo != Modo.dead){
            var position = transform.position;
            Demo player = GameObject.FindWithTag("Player").GetComponent(typeof(Demo)) as Demo;
            player.puntaje += 5;
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
        m_Collider.enabled = false;
    }

    private void SpriteBlinkingEffect(){
        spriteBlinkingTotalTimer += Time.deltaTime;
        if(spriteBlinkingTotalTimer >= spriteBlinkingTotalDuration){
            startBlinking = false;
            spriteBlinkingTotalTimer = 0.0f;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;  
            return;
        }

        spriteBlinkingTimer += Time.deltaTime;
        if(spriteBlinkingTimer >= spriteBlinkingMiniDuration){
            spriteBlinkingTimer = 0.0f;
            if (this.gameObject.GetComponent<SpriteRenderer>().enabled == true) {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;  //make changes
            } else {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = true;   //make changes
            }
        }
    }

    public int getZombieLife(){
        return this.zombieLife;
    }
}