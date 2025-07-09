using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Texto TMP de salud")]
    public TMP_Text healthText;

    [Header("Sonido al recibir daño")]
    public AudioClip damageSound;
    private AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        UpdateHealthText();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Daño recibido: " + amount + " | Vida actual: " + currentHealth);
        UpdateHealthText();

        PlayDamageSound();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void PlayDamageSound()
    {
        if (damageSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
    }

    void Die()
    {
        Debug.Log("Jugador destruido.");
        Destroy(gameObject);
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Salud: " + currentHealth.ToString("0");
        }
    }
}
