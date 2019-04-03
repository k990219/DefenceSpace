using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    Animator animator;


    // Use this for initialization
    void Start () {
        animator=GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
          //  animator.SetFloat("MoveY", pm.moveY);
          //  animator.SetFloat("MoveX", pm.moveX);



        if (Input.GetMouseButton(0))
        {
        //    animator.SetBool("Shoot", true);
        }
        else
        {
        //    animator.SetBool("Shoot", false);

        }

    }
}
