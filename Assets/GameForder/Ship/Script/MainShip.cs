using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainShip : MonoBehaviour {

    private static MainShip instance;
    public static MainShip mianShip
    {
        get
        {
            if (!instance) instance =  FindObjectOfType(typeof(MainShip)) as MainShip; ;
            return instance;
        }
    }

	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Translate(Vector3.forward * Time.deltaTime*0.1f);
        if (transform.position.x > 10)
            transform.position = new Vector3(-10,0,0);

	}
}
