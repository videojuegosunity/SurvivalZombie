using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Basic Player Script//
//controls:
//A, D, Left, Right to move
//Left Alt to attack
//Space to jump
//Z is to see dead animation

public class Demo : MonoBehaviour {
    // GUI
    public Text puntajeText;
    public Text vidaText;
    public Text infoText;
    public Image mascaraDano;
    public Animator anim;

    // informacion de jugador
    public int vidas = 3;
    public int puntaje = 0;
    public enum Modo {ataque, normal};
    public Modo modo;
    public GameObject[] herramientas;

	private float speed = 5f;
	private bool facingRight = true;
	
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;

	//variable for how high player jumps//
	[SerializeField]
	private float jumpForce = 300f;

	public Rigidbody2D rb { get; set; }

	bool dead = false;
	//bool attack = false;
	public Transform obj_trans;

    private void Awake(){
        obj_trans = this.transform;
        modo = Modo.normal;
    }

	void Start () {
		GetComponent<Rigidbody2D> ().freezeRotation = true;
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponentInChildren<Animator> ();
	}

	void Update(){
        SetCountText();
        float valfa = 0.15f * (3 - vidas);
        mascaraDano.color =  new Color(255, 0, 0, valfa);
		HandleInput ();
	}

	void FixedUpdate (){
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		float horizontal = Input.GetAxis("Horizontal");
		if (!dead && modo != Modo.ataque){
			anim.SetFloat ("vSpeed", rb.velocity.y);
			anim.SetFloat ("Speed", Mathf.Abs (horizontal));
			rb.velocity = new Vector2 (horizontal * speed, rb.velocity.y);
         
		}
		if (horizontal > 0 && !facingRight && !dead && modo != Modo.ataque) {
			Flip (horizontal);
		}

		else if (horizontal < 0 && facingRight && !dead && modo != Modo.ataque){
			Flip (horizontal);
		}
	}

	//attacking and jumping//
	private void HandleInput(){
		if (Input.GetKeyDown (KeyCode.C) && !dead)
		{
			//attack = true;
            modo = Modo.ataque;
			anim.SetBool ("Attack", true);
			anim.SetFloat ("Speed", 0);

		}
		if (Input.GetKeyUp(KeyCode.C))
			{
			//attack = false;
            modo = Modo.normal;
			anim.SetBool ("Attack", false);
			}

		if (grounded && Input.GetKeyDown(KeyCode.Space) && !dead)
		{
			anim.SetBool ("Ground", false);
			rb.AddForce (new Vector2 (0,jumpForce));
		}

		//dead animation for testing//
		if (Input.GetKeyDown (KeyCode.Z))
		{
			if (!dead) {
				anim.SetBool ("Dead", true);
				anim.SetFloat ("Speed", 0);
				dead = true;
			} else {
					anim.SetBool ("Dead", false);
					dead = false;
				}
		}
	}

	private void Flip (float horizontal)
	{
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
	}

    void SetCountText (){
        puntajeText.text = puntaje.ToString();
        vidaText.text = vidas.ToString();

        if (puntaje >= 10){
            infoText.text = "Ganaste!";
        }
    }

    void OnCollisionEnter2D(Collision2D col){       
        if(col.gameObject.tag == "zombie" && !dead){
            switch (modo){
                case Modo.normal:
                    infoText.text = "Perdiste una vida!!!!!";
                    vidas--;
                    rb.velocity = new Vector2 (rb.velocity.y - 1, rb.velocity.y);
                    //Destroy(col.gameObject);
                    if(vidas<=0){
                        anim.SetBool ("Dead", true);
				        anim.SetFloat ("Speed", 0);
				        dead = true;
                        infoText.text = "Perdiste!!!!!";
                        //Destroy(gameObject);
                    } 
                    break;
                case Modo.ataque:
                    Debug.Log("impactado");
                    infoText.text = "mataste un zombie!!!!!";
                    Destroy(col.gameObject);
                    puntaje += 2; 
                    break;
            }  
            
        }
    }
}
