using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sniperScript : MonoBehaviour
{
    public float sniperDamage=10f;
    public float sniperRange=100f;

    public Camera sniperFpsCam;
    public ParticleSystem sniperMussleFlash;
    public GameObject sniperHitImpactObjects;
    public GameObject sniperHitImpactEnemy;

    public AudioSource sniperShootSound;
    public AudioSource sniperReloadtSound;

    public int sniperMaxAmmo=5;
    private int currentAmmo;
    public float sniperReloadTime=1f;
    private bool isReloading=false;

    private bool isADSon= false;
    public GameObject sniperScopeOverlay;

    public Animator sniperAnimations;

    public GameObject weaponCam;

    void Start()
    {
        currentAmmo=sniperMaxAmmo;
    }   

      void OnEnable ()
    {
        isReloading = false;
       sniperAnimations.SetBool("reloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isReloading)
        return;

        if(currentAmmo<=0 || Input.GetKeyDown("r"))
        {
           StartCoroutine(reload());
            return;
        }

        if(Input.GetButtonDown("Fire1"))
        {   
            shoot();
            
        }

        if(Input.GetButtonDown("Fire2"))
        {   
            isADSon = !isADSon;

            if(isADSon)
            {
                StartCoroutine(scoped());
            }
            else
            {
                unscoped();
            }

            sniperAnimations.SetBool("isG1ASD", false);
            sniperAnimations.SetBool("isPistolScope", false);
            sniperADS();
        }
    }

     IEnumerator scoped()
     {
         yield return new WaitForSeconds(0.15f);  // waiting for scope in animation to complete
         sniperScopeOverlay.SetActive(true);   // setting the scope overlay image as active
         weaponCam.SetActive(false);  // hiding the gun
         sniperFpsCam.fieldOfView=15f;  // zooming in
     }

     void unscoped()
     {
        sniperScopeOverlay.SetActive(false);  // setting the scope overlay image as inactive
        weaponCam.SetActive(true);   // showing the gun
        sniperFpsCam.fieldOfView=60f;  // zooming out
     }

    void sniperADS()
    {
        if(isADSon)
         {sniperAnimations.SetBool("isSniperScoped", true);}

         else
         {
            sniperAnimations.SetBool("isSniperScoped", false); 
         }
    }

    IEnumerator reload()
    {   
        isReloading=true;
        sniperReloadtSound.Play();
        

        sniperAnimations.SetBool("reloading", true);

        yield return new WaitForSeconds(sniperReloadTime-0.25f); // wait for (pistolReloadTime) seconds

        currentAmmo=sniperMaxAmmo;
        isReloading=false;
        
        yield return new WaitForSeconds(0.25f);

        sniperAnimations.SetBool("reloading", false);
    }

    void shoot()
    {

       currentAmmo--;

       RaycastHit hit;
      if(Physics.Raycast(sniperFpsCam.transform.position , sniperFpsCam.transform.forward, out hit, sniperRange))
      {
          
          
          target ourTarget= hit.transform.GetComponent<target>();
          if(ourTarget != null)
          {   
              sniperShootSound.Play();
              sniperMussleFlash.Play();
              ourTarget.takeDamage(sniperDamage);
              GameObject tempEnemyImpact =Instantiate(sniperHitImpactEnemy, hit.point,Quaternion.LookRotation(hit.normal));  
              Destroy(tempEnemyImpact,1f);
              
          }
          
          else
          { 
            sniperShootSound.Play();
            sniperMussleFlash.Play();
            GameObject tempImpact =Instantiate(sniperHitImpactObjects, hit.point,Quaternion.LookRotation(hit.normal));  
            Destroy(tempImpact,1f);
          }
      }
      else
      {
            sniperShootSound.Play();
            sniperMussleFlash.Play();
      }
    }

}
