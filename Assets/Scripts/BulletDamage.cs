using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public int damageAmount = 25;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Alien"))
        {
            AlienHealth alien = other.GetComponent<AlienHealth>();
            if (alien != null)
            {
                alien.TakeDamage(damageAmount);
            }

            Destroy(gameObject); // Destruir la bala al impactar
        }
    }
}
