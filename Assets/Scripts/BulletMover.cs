using UnityEngine;

public class BulletMover : MonoBehaviour
{
    [Header("Velocidad de la bala")]
    public float speed = 10f;

    private Camera mainCamera;
    private Vector2 screenBounds;

    void Start()
    {
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
    }

    void Update()
    {
        // Movimiento hacia arriba (eje Y)
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Verificar si está fuera de la cámara
        Vector3 pos = transform.position;
        if (pos.y > screenBounds.y + 1f || pos.y < -screenBounds.y - 1f ||
            pos.x > screenBounds.x + 1f || pos.x < -screenBounds.x - 1f)
        {
            Destroy(gameObject);
        }
    }
}
