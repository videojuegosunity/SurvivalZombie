using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//toggles player in demo scene//
public class Toggle : MonoBehaviour {

	public GameObject ch;
	public GameObject chr;
	public bool onoff;

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.X)) {
			if (!onoff) {
                var position = chr.transform.position;
                var scale = chr.transform.localScale;
                ch.transform.position = position;
                //ch.transform.localScale = scale;
				ch.SetActive (true);
				chr.SetActive (false);
				onoff = true;
			} else {
                var position = ch.transform.position;
                var scale = ch.transform.localScale;
                chr.transform.position = position;
                //chr.transform.localScale = scale;
				ch.SetActive (false);
				chr.SetActive (true);
				onoff = false;
			}
		}
	}
}
