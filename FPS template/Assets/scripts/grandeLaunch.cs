using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grandeLaunch : MonoBehaviour
{
    public float throwForce=40f;
    public GameObject granadePrefb;
    
    public GameObject playerMmt;
    void Start()
    {   
        
    }

    // Update is called once per frame
    void Update()
    {    
        movements ourTarget=playerMmt.GetComponent<movements>();
        if(Input.GetButtonUp("Fire1"))
        {
            GameObject granade = Instantiate(granadePrefb, transform.position,transform.rotation);
            
            Rigidbody rb = granade.GetComponent<Rigidbody>();
            rb.AddForce(5*ourTarget.playerMoveForce-transform.up*throwForce, ForceMode.VelocityChange);
        }
    }
}
