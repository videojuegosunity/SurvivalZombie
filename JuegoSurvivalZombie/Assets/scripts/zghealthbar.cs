using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zghealthbar : MonoBehaviour
{

	public greendman zombie;
	Vector3 localScale;
	int zombieLife;
    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        zombieLife = zombie.getZombieLife();
        localScale.x = zombieLife/50f;
        transform.localScale = localScale;
    }
}
