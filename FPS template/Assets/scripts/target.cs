
using UnityEngine;

public class target : MonoBehaviour
{
   public float health=100f;

   public void takeDamage(float amount)  //making public function so that we can call it on every hit
   {
      health -= amount;
      if(health<=0f)
      {
          die();
      }
   }

   void die()
   {
       Destroy(gameObject);
   }
   
}
