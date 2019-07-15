using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greendman : MonoBehaviour
{
    private float speed = 2.5f;
    private Transform obj_trasform;
    bool toRight;

    private void Awake()
    {
        obj_trasform = this.transform;
        gameObject.transform.localScale = new Vector3(1.5F, 1.5F, 0.0f);
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Transform>().position = new Vector3(18.0f, 0.0f, 0.0f);
        toRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        move(toRight);
    }

    private void move(bool to_right){
        int mult = 1;
        bool next = false;
        if (to_right == false){
            mult = -1;
            next = true;
        }

        if (to_right && gameObject.transform.position.x < 11.00f || next && gameObject.transform.position.x > -11.00f){
                gameObject.transform.localScale = new Vector3(mult*1.5F, 1.5F, 0.0f);
                gameObject.transform.Translate(mult*speed*Time.deltaTime, 0, 0);
        }
        else{
            linearStop();
            this.toRight = next;
        }
    }

    private void linearStop()
    {
        var v = gameObject.GetComponent<Rigidbody2D>().velocity;
        v =  Vector3.zero;
        gameObject.GetComponent<Rigidbody2D>().velocity = v;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

}
