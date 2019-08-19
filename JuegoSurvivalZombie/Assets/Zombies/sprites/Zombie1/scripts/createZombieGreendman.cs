using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createZombieGreendman : MonoBehaviour{
    public GameObject prefab_zombie_greendman;

    int count = 0;
    int zombieI = 0;
    public int qty;
    public int life;

    // Start is called before the first frame update
    void Start(){

    }

    // Update is called once per frame
    void Update(){
        count++;
        if( zombieI < qty ){
            if( count == 200 ){
                Vector3 position = new Vector3(30.0f + zombieI*5 ,-4f, prefab_zombie_greendman.transform.position.z);
                GameObject obj = (GameObject) Instantiate(prefab_zombie_greendman, position, Quaternion.identity);
                greendman zombie = obj.GetComponent<greendman>();
                zombie.zombieLife = life;
                count = 0;
                zombieI++;
            }
        }
    }
}
