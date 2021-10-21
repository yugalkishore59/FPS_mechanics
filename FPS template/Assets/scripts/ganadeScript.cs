using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ganadeScript : MonoBehaviour
{
    // basic granade properties
    public float delay = 3f; 
    public float granadeDamage=100f; 
    public float blastRadius=5f;
    float countdown;

    bool hasExploded=false; 

    public GameObject expolsionEffect; //ParticleSystem
    

    void Start()
    {
        countdown=delay; //setting initial value for countdown
    }
    void Update()
    {
       countdown-=Time.deltaTime;  //decreasing countdown with time

       if(countdown <=0f && !hasExploded) //explosion conditions
       {
           explode();
           hasExploded=true;
       }
    }

    void explode()
        {
          GameObject tempExp=Instantiate(expolsionEffect, transform.position, transform.rotation); //spawning explosion effect particle system
          Collider[] nearbyObjs= Physics.OverlapSphere(transform.position,blastRadius); // getting list of nearby objects in blast radius
          
          foreach (Collider nearbyObjects in nearbyObjs) // checking each objects
          {
             target ourTarget= nearbyObjects.GetComponent<target>();  // checking foe enemy

             if(ourTarget != null)  // if enemy found
           {   
              ourTarget.takeDamage(granadeDamage);  //calling damage function inside the enemy (target.cs script)
           }
          }

          Destroy(gameObject); //destroying the granade
          Destroy(tempExp,1f);  //destroying the explosion effect
        }
    

   
}