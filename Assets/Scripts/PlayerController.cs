using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Velocidad de movimiento")]
    public float speed = 5f;

    [Header("Límites del mapa")]
    public float minX = -8f;
    public float maxX = 8f;
    public float minY = -4.5f;
    public float maxY = 4.5f;

    private Vector2 moveInput;

    // Este método será llamado automáticamente por el sistema de Input
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void Update()
    {
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0f) * speed * Time.deltaTime;

        Vector3 newPosition = transform.position + movement;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;
    }
}
