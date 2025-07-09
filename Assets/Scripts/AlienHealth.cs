using UnityEngine;

public class AlienHealth : MonoBehaviour
{
    [Header("Salud del alien")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Sprites por daño")]
    public Sprite[] damageSprites;
    private SpriteRenderer spriteRenderer;

    [Header("Sonido de muerte")]
    public AudioClip deathSound;
    private AudioSource audioSource;

    [Header("Láser al morir")]
    public GameObject deathLaserPrefab;
    public Transform laserSpawnPoint;

    public event System.Action OnDeath;
    private bool isDying = false;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        UpdateSprite();
    }

    public void TakeDamage(int damage)
    {
        if (isDying) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateSprite();

        if (currentHealth <= 0)
        {
            isDying = true;

            // Disparar láser al morir
            ShootDeathLaser();

            // Notificar muerte
            OnDeath?.Invoke();

            // Sumar puntos
            ScoreManager.Instance?.AddScore(10);

            // Reproducir sonido y destruir
            if (deathSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(deathSound);
                Destroy(gameObject, deathSound.length);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void UpdateSprite()
    {
        int index = 0;
        if (currentHealth <= 75) index = 1;
        if (currentHealth <= 50) index = 2;
        if (currentHealth <= 25) index = 3;

        if (damageSprites != null && index < damageSprites.Length)
        {
            spriteRenderer.sprite = damageSprites[index];
        }
    }

    void ShootDeathLaser()
    {
        if (deathLaserPrefab != null)
        {
            Vector3 spawnPosition = laserSpawnPoint != null ? laserSpawnPoint.position : transform.position;
            Instantiate(deathLaserPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
