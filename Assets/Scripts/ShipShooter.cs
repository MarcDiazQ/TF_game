using UnityEngine;
using UnityEngine.InputSystem;

public class ShipShooter : MonoBehaviour
{
    [Header("Disparo")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    public void OnFire(InputAction.CallbackContext context)
    {
        // Disparar solo cuando se presiona el botón, no cuando se suelta
        if (context.performed)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
        else
        {
            Debug.LogWarning("Falta asignar el bulletPrefab o firePoint en ShipShooter.");
        }
    }
}
