using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public int energiaTotal = 12000;
    public int energiaActual = 12000;
    public int puntaje = 0;
    int deadCount = 120;
    public bool dead = false;
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
        cantidad.Add(0);
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
		HandleInput();
        if(dead && deadCount-- < 0){
            SceneManager.LoadScene("perdiste");
        }
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
		if (Input.GetKeyDown (KeyCode.X) && !dead){
            modo = Modo.ataque;
			anim.SetBool ("Attack", true);
			anim.SetFloat ("Speed", 0);
		}
		if (Input.GetKeyUp(KeyCode.X)){
            modo = Modo.normal;
			anim.SetBool ("Attack", false);
		}
		if (grounded && Input.GetKeyDown(KeyCode.Space) && !dead){
			anim.SetBool ("Ground", false);
			rb.AddForce (new Vector2 (0,jumpForce));
		}

		if(Input.GetKeyUp(KeyCode.V) && cantidad.Count > 0){
			if(cantidad[0] > 0){
				var position = rb.position;
	            GameObject obj = (GameObject) Instantiate(bomba, new Vector3(position.x + 2.5f, position.y, -5), Quaternion.identity);
	            obj.GetComponent<Rigidbody2D>().velocity = new Vector3(3, 0, 0);
	            obj.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
	            cantidad[0]--;
			}
        }
	}

	private void Flip (float horizontal){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    void SetCountText(){
        puntajeText.text = puntaje.ToString();
        vidaText.text = vidas.ToString();
        if(cantidad.Count > 0){
			cantidadText.text = cantidad[0].ToString();
        }
        if (puntaje >= 35){
            infoText.text = "Ganaste!";
            SceneManager.LoadScene("menu");
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        collision(col);
		// infoText.text = "";
    }

	private void OnCollisionStay2D(Collision2D col){
		collision(col);
		// infoText.text = "";
    }

    public bool ataque(){
    	return Modo.ataque == modo;
    }

    private void collision(Collision2D col){
    	if(col.gameObject.tag == "zombie" && !dead){
            switch (modo){
                case Modo.normal:
                    energiaActual = energiaActual - 20;
                    anim.SetBool ("Hurt", true);
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
                    greendman zombieAttacked = col.gameObject.GetComponent<greendman>();
                    zombieGolpeado(zombieAttacked.getKilled());
                    break;
            }
        }
        if(col.gameObject.tag == "bomba" && !dead){
        	herramientas.Add(col.gameObject);
        	cantidad[0] += 5;
        	Destroy(col.gameObject);
        }
    }

    public void zombieGolpeado(bool isKilled){
    	if(isKilled == false){
			// infoText.text = "Golpe!!";
    		// puntaje += 1;
		}
		else{
			// infoText.text = "Aniquilado!!";
    		puntaje += 3;
		}
    }
}
