using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createZombieGreendman : MonoBehaviour
{
public GameObject prefab_zombie_greendman;

    int count = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        count++;
        if( count==75 )
        {
            Vector3 position = new Vector3(-10.0f, prefab_zombie_greendman.transform.position.y, prefab_zombie_greendman.transform.position.z);
            Instantiate(prefab_zombie_greendman, position, Quaternion.identity);
            count = 0;
        }
    }
}
