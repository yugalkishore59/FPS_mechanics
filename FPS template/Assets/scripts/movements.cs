using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movements : MonoBehaviour
{
    //movement variables
  // {
        public CharacterController controller;
        public float speed=12f;  //movement
        public Vector3 playerMoveForce;
  // }
    //gravity variables
  // {
    public float gravity=9.8f;
    Vector3 velocity;  //downward velocity
    public Transform groundCheck;  //object checking for ground
    public float groundDistance=0.3f; //radius of detection
    public LayerMask groundMask;  //to check for ground only ie. not other objects
    bool isGrounded;  //if the player is on ground
    public float jumpHeight=3f;
  // }
    
    //sounds
    //{
        public AudioSource jumpSound;

    //}

    public Joystick myJoystick;
  

    void Update()
    {

      //gravity
     //{
       isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // if on ground or not

       if(isGrounded && velocity.y<0) // (velocity.y<0)  set downward velocity to -2(ie small) when on ground
       {
          velocity.y=-2f;
       }
      velocity.y-= gravity*Time.deltaTime;  //increasing downward velocity with time with gravity factor
       controller.Move(velocity*Time.deltaTime); //accelerting player (downwards) using downward velocity
     //}
      

      //moving with w a s d keys
      //{
          
          float x=Input.GetAxis("Horizontal");
          float z=Input.GetAxis("Vertical");

          Vector3 move= transform.right*x + transform.forward*z;  
          playerMoveForce = move;
          controller.Move(move*speed*Time.deltaTime);
     //}

      //jumping with space button
      //{
        if(Input.GetButtonDown("Jump")&& isGrounded)
        {
          velocity.y=Mathf.Sqrt(jumpHeight* 2f * gravity);  //using physics formula to determine initial velocity (v^2 - u^2 = 2as)
        
          jumpSound.Play(); //playing jump sound
        }

     // }
 
      
    }
}
