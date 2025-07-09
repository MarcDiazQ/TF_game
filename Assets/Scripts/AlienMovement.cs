using UnityEngine;

public class AlienMovement : MonoBehaviour
{
    [Header("Movimiento vertical")]
    public float verticalSpeed = 2f;

    [Header("Movimiento sinusoidal")]
    public float amplitude = 1f;
    public float frequency = 2f;

    private float initialX;
    private Camera mainCamera;
    private float timeElapsed = 0f;
    private bool isDead = false;

    public AlienHealth health;

    void Start()
    {
        initialX = transform.position.x;
        mainCamera = Camera.main;
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        // Movimiento senoidal en X
        float offsetX = Mathf.Sin(timeElapsed * frequency) * amplitude;
        float newX = initialX + offsetX;

        // Movimiento hacia abajo en Y
        float newY = transform.position.y - verticalSpeed * Time.deltaTime;

        transform.position = new Vector3(newX, newY, transform.position.z);

        // Verificar si pasa el borde inferior
        float bottomLimit = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;

        if (transform.position.y < bottomLimit - 1f && !isDead)
        {
            isDead = true;
            DamagePlayer();
            health.TakeDamage(1000); // Elimina al alien
        }
    }

    void DamagePlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10f);
            }
        }
    }
}
