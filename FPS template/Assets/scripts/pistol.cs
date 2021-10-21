using System.Collections;  // must include for time dependent functions (#)
using UnityEngine;

public class pistol : MonoBehaviour
{
    public float damage=10f;      //basic gun specific properties
    public float range=100f;


    public Camera fpsCam;        // shooting properties (raycast and particle system)
    public ParticleSystem pistolMussleFlash;
    public GameObject hitImpactObjects;
    public GameObject hitImpactEnemy;

    public AudioSource shootSound;  // sounds
    public AudioSource pistolReloadtSound;

    public int pistolMaxAmmo=10;   // reloading properties
    private int currentAmmo;
    public float pistolReloadTime=1f;
    private bool isReloading=false;

    private bool isADSon= false;   //scope in and out

    public Animator pistolAnimations;  // animations


    void Start()
    {  
        
         currentAmmo=pistolMaxAmmo;  // setting to full ammo on start
       
    }

     void OnEnable ()    // called every time when this gun is enabled (unlike start function)
    {
        isReloading = false;
        pistolAnimations.SetBool("reloading", false);
    }

    
    void Update()
    {   
        if(isReloading)
        return;  // to stop firing, scope in out etc while reloading

        if(currentAmmo<=0 || Input.GetKeyDown("r"))  //reloading condition
        {
           StartCoroutine(reload());
            return;
        }

       if(Input.GetButtonDown("Fire1"))  // firing condition
        {   
           shoot();
            
        }

        if(Input.GetButtonDown("Fire2"))  // scope in out condition
        {   
            isADSon = !isADSon;

            pistolAnimations.SetBool("isG1ASD", false); // setting off other gun's animations
            pistolAnimations.SetBool("isSniperScoped", false);

            pistolADS();   // scope in out function
        }

        
    } 

    void pistolADS()   // scope in out animations
    {
        if(isADSon)
         {pistolAnimations.SetBool("isPistolScope", true);}

         else
         {
            pistolAnimations.SetBool("isPistolScope", false); 
         }
    }

    IEnumerator reload()  //time dependent function (#)
    {   
        isReloading=true;
        pistolReloadtSound.Play();
        

        pistolAnimations.SetBool("reloading", true);

        yield return new WaitForSeconds(pistolReloadTime-0.25f); // wait for (pistolReloadTime) seconds (#)

        currentAmmo=pistolMaxAmmo;
        isReloading=false;
        
        yield return new WaitForSeconds(0.25f);  // (#)

        pistolAnimations.SetBool("reloading", false);
    }

    void shoot()
    {

       currentAmmo--;  //decreasing 1 ammo on every shot

       RaycastHit hit;  // creating an invisible ray
      if(Physics.Raycast(fpsCam.transform.position , fpsCam.transform.forward, out hit, range)) // if our ray hit something in range according to starting conditions
      {
          
          
          target ourTarget= hit.transform.GetComponent<target>();  // getting the target(name) script from the object collided with ray (attatched to enemies only)
          if(ourTarget != null)  //if we get the script (ie. shoot the enemy)
          {   
              shootSound.Play();
              pistolMussleFlash.Play();
              ourTarget.takeDamage(damage); //giving damage to our enemy (ie. calling damage function inside that enemy)
              GameObject tempEnemyImpact =Instantiate(hitImpactEnemy, hit.point,Quaternion.LookRotation(hit.normal));  //creating blood effect (particle system)
              Destroy(tempEnemyImpact,1f); //destroying blood effect particle sytem after 1 second
              
          }
          
          else  //if we hit environment objects (not enemy)
          { 
            shootSound.Play();
            pistolMussleFlash.Play();
            GameObject tempImpact =Instantiate(hitImpactObjects, hit.point,Quaternion.LookRotation(hit.normal)); //creating dust/scratch/spark effect
            Destroy(tempImpact,1f); // destroying dust/scratch/spark effect after 1 second
          }
      }
      else  // if we didn't hit anything in range
      {     
            //creting mussle flash and sound only
            shootSound.Play();
            pistolMussleFlash.Play(); 
      }
    }
}
