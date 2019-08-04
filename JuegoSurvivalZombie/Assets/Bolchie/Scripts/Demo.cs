using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demo : MonoBehaviour {
    // GUI
    public Text puntajeText;
    public Text vidaText;
    public Text infoText;
   	public Text cantidadText;
    public Image mascaraDano;
    public Image barraEnergia;
    public Image herramienta;
    public Animator anim;

    // informacion de jugador
    public int vidas = 3;
    public int energiaTotal = 100;
    public int energiaActual = 100;
    public int puntaje = 0;
    bool dead = false;
    public enum Modo {ataque, normal};
    public Modo modo;
    List<GameObject> herramientas = new List<GameObject>();
    List<int> cantidad = new List<int>();
    public int index_herramientas;

	private float speed = 5f;
	public GameObject bomba;
	private bool facingRight = true;
	
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;

	//variable for how high player jumps//
	[SerializeField]
	private float jumpForce = 1f;

	public Rigidbody2D rb { get; set; }
	public Transform obj_trans;

    private void Awake(){
    	obj_trans = this.transform;
        modo = Modo.normal;
    }

	void Start () {
		GetComponent<Rigidbody2D>().freezeRotation = true;
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
		}else if (horizontal < 0 && facingRight && !dead && modo != Modo.ataque){
			Flip (horizontal);
		}
		barraEnergia.fillAmount = (float)energiaActual / energiaTotal;
	}

	private void HandleInput(){
		if (Input.GetKeyDown (KeyCode.C) && !dead){
            modo = Modo.ataque;
			anim.SetBool ("Attack", true);
			anim.SetFloat ("Speed", 0);
		}
		if (Input.GetKeyUp(KeyCode.C)){
            modo = Modo.normal;
			anim.SetBool ("Attack", false);
		}
		if (grounded && Input.GetKeyDown(KeyCode.Space) && !dead){
			anim.SetBool ("Ground", false);
			rb.AddForce (new Vector2 (0,jumpForce));
		}

		if(Input.GetKeyUp(KeyCode.V) && cantidad[0] > 0){
            var position = rb.position;
            GameObject obj = (GameObject) Instantiate(bomba, new Vector3(position.x + 2.5f, position.y, -5), Quaternion.identity);
            obj.GetComponent<Rigidbody2D>().velocity = new Vector3(3, 0, 0);
            cantidad[0] --;
            cantidadText.text = cantidad[0].ToString();
        }
	}

	private void Flip (float horizontal){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    void SetCountText (){
        puntajeText.text = puntaje.ToString();
        vidaText.text = vidas.ToString();
        if (puntaje >= 20){
            infoText.text = "Ganaste!";
        }
    }

    void OnCollisionEnter2D(Collision2D col){       
        collision(col);
    }

    public bool ataque(){
    	return Modo.ataque == modo; 
    }

    private void collision(Collision2D col){
    	if(col.gameObject.tag == "zombie" && !dead){
            switch (modo){
                case Modo.normal:
                    energiaActual = energiaActual - 5;
                    if(energiaActual <= 0){
                    	infoText.text = "Perdiste una vida!!!!!";
                    	vidas--;
                    	energiaActual = energiaTotal;
                    }
                    rb.velocity = new Vector2 (rb.velocity.y - 1, rb.velocity.y);
                    if(vidas<=0){
                        anim.SetBool ("Dead", true);
				        anim.SetFloat ("Speed", 0);
				        dead = true;
                        infoText.text = "Perdiste!!!!!";
                    } 
                    break;
                case Modo.ataque:
                    infoText.text = "mataste un zombie!!!!!";
                    greendman scriptToAccess = col.gameObject.GetComponent<greendman>();
                    scriptToAccess.CheckDestroy(true);
                    puntaje += 2; 
                    break;
            }  
        }
        if(col.gameObject.tag == "bomba" && !dead){
        	//herramienta.sprite = (Sprite)Resources.Load <Sprite>("Free Game Items/300dpi/bomb");
        	herramientas.Add(col.gameObject);
        	cantidad.Add(5);
        	cantidadText.text = "5";
        	Destroy(col.gameObject);
        }
    }
}
