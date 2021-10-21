using System.Collections;
using UnityEngine;

public class weaponSwitch : MonoBehaviour
{
    public int currentWeapon=0;
    public AudioSource switchSound;

    public Animator switchWeaponAni;

    void Start()
    {
         int j=0;
        foreach(Transform weapon in transform )
        {
            if(j== currentWeapon)
            { weapon.gameObject.SetActive(true);}
        else
            {weapon.gameObject.SetActive(false);}
        
        j++;
        }    
    }

    
    void Update()
    {
        int previousWeapon=currentWeapon;

      /*  this is to switch weapon with mouse wheel
      
      if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {   if(currentWeapon >= transform.childCount - 1)
                currentWeapon=0;
             else
                currentWeapon++;
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {   if(currentWeapon <= 0)
                currentWeapon=transform.childCount - 1;
             else
                currentWeapon--;
        }

      */

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon=0;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)&& transform.childCount >=2)
        {
            currentWeapon=1;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)&& transform.childCount >=3)
        {
            currentWeapon=2;
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)&& transform.childCount >=4)
        {
            currentWeapon=3;
        }
        if(Input.GetKeyDown(KeyCode.Alpha5)&& transform.childCount >=5)
        {
            currentWeapon=4;
        }

        if(previousWeapon != currentWeapon)
        {
            StartCoroutine(selectWeapon());
        }
    }
    
    IEnumerator selectWeapon()
    {
        switchWeaponAni.SetBool("switchingWeapon", true);
        yield return new WaitForSeconds(0.25f);

        switchWeaponAni.SetBool("isPistolScope", false);
        switchWeaponAni.SetBool("isG1ASD", false);
        switchWeaponAni.SetBool("isSniperScoped", false);


        int i=0;
        foreach(Transform weapon in transform )
        {
            if(i== currentWeapon)
            {
             weapon.gameObject.SetActive(true);
             switchSound.Play();
             }

            else
            {
               weapon.gameObject.SetActive(false);
            }     
        i++;
        }

        switchWeaponAni.SetBool("switchingWeapon", false);
    }
}
