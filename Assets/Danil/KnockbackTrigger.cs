using UnityEngine;

public class KnockbackTrigger : MonoBehaviour
{
    public float force = 15f; 

    private void OnTriggerEnter(Collider other)
    {
    
        CharacterController controller = other.GetComponent<CharacterController>();

        if (controller != null)
        {

            Vector3 direction = -other.transform.forward;

        
          
            StartCoroutine(ApplyKnockback(controller, direction));
        }
    }

    private System.Collections.IEnumerator ApplyKnockback(CharacterController controller, Vector3 dir)
    {
        float timer = 0.2f;
        while (timer > 0)
        {
            
            controller.Move(dir * force * Time.deltaTime);
            timer -= Time.deltaTime;
            yield return null; 
        }
    }
}
