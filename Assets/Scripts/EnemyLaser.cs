using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    public float speed = 10f;
    public float damagePercent = 0.1f;

    private Vector3 direction;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            direction = (player.transform.position - transform.position).normalized;
        }
        else
        {
            direction = Vector3.down; // Por defecto, si no hay jugador
        }
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        // Destruye si sale por debajo del mapa
        if (transform.position.y < Camera.main.ScreenToWorldPoint(Vector3.zero).y - 1f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                float damageAmount = player.maxHealth * damagePercent;
                player.TakeDamage(damageAmount);
            }

            Destroy(gameObject);
        }
    }
}
