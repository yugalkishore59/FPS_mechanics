using UnityEngine;
using System.Collections;
public class gun1Script : MonoBehaviour
{
    public float g1damage=10f;
    public float g1range=100f;
    public float g1FireRate= 15f;
    public Camera g1fpsCam;
    public AudioSource g1shootSound;
    public AudioSource g1ReloadSound;
    public ParticleSystem g1MussleFlash;
    public GameObject g1hitImpactObjects;
    public GameObject g1hitImpactEnemy;

    private float nextTimeToFire=0f;

    public int g1MaxAmmo=10;
    private int currentAmmo;
    public float g1ReloadTime=1f;
    private bool isReloading=false;

    private bool isADSon= false;
    public Camera camFOV;

    public Animator g1Animations;

    void Start()
    {
        
        
         currentAmmo=g1MaxAmmo;
        
    }

    void OnEnable ()
    {
        isReloading = false;
        g1Animations.SetBool("reloading", false);
    }

    
    void Update()
    {   
        if(isReloading)
        return;

         if(currentAmmo<=0 || Input.GetKeyDown("r"))
        {
            StartCoroutine(reload());
            return;
        }

        if(Input.GetButton("Fire1")&& Time.time >=nextTimeToFire)
        {   
            nextTimeToFire= Time.time + 1f/g1FireRate;
            shoot();
            
        }

        if(Input.GetButtonDown("Fire2"))
        {   
            isADSon = !isADSon;
            g1Animations.SetBool("isPistolScope", false);
            g1Animations.SetBool("isSniperScoped", false);
            g1ADS();
        }
    } 

    void g1ADS()
    {
        if(isADSon)
         { g1Animations.SetBool("isG1ASD", true);
           camFOV.fieldOfView=50;
         }

         else
         {
            g1Animations.SetBool("isG1ASD", false);
            camFOV.fieldOfView=60; 
         }
    }
     
      IEnumerator reload()
    {   
        g1Animations.SetBool("reloading", true);
        g1ReloadSound.Play();

        isReloading=true;
        
        yield return new WaitForSeconds(g1ReloadTime- 0.25f); // wait for (g1ReloadTime) seconds
        currentAmmo=g1MaxAmmo;
        isReloading=false;
        yield return new WaitForSeconds(0.25f);
         g1Animations.SetBool("reloading", false);
    }
    void shoot()
    {  
        currentAmmo--;

       RaycastHit hit;
      if(Physics.Raycast(g1fpsCam.transform.position , g1fpsCam.transform.forward, out hit, g1range))
      {
          
          
          target ourTarget= hit.transform.GetComponent<target>();
          if(ourTarget != null)
          {   
              g1shootSound.Play();
              g1MussleFlash.Play();
              ourTarget.takeDamage(g1damage);
              GameObject tempEnemyImpact =Instantiate(g1hitImpactEnemy, hit.point,Quaternion.LookRotation(hit.normal));  
              Destroy(tempEnemyImpact,1f);
              
          }
          
          else
          { 
            g1shootSound.Play();
            g1MussleFlash.Play();
            GameObject tempImpact =Instantiate(g1hitImpactObjects, hit.point,Quaternion.LookRotation(hit.normal));  
            Destroy(tempImpact,1f);
          }
      }
      else
      {
          g1shootSound.Play();
            g1MussleFlash.Play();
      }
    }
}