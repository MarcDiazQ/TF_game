using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    [Header("Prefab del alien")]
    public GameObject alienPrefab;

    [Header("Espacio de aparición")]
    public float spawnMarginX = 0.5f;
    public float spawnOffsetY = 1f;
    public float spawnHeightFraction = 0.5f; // Mitad de la altura de la pantalla

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private int spawnLimit = 1;
    private int activeAliens = 0;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        CalculateSpawnArea();
        SpawnWave();
    }

    void CalculateSpawnArea()
    {
        Vector3 left = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 right = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        Vector3 top = mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));

        minX = left.x + spawnMarginX;
        maxX = right.x - spawnMarginX;

        float verticalSize = top.y - mainCamera.transform.position.y;
        minY = top.y + spawnOffsetY;
        maxY = minY + (verticalSize * spawnHeightFraction);
    }

    void SpawnWave()
    {
        for (int i = 0; i < spawnLimit; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

            GameObject alien = Instantiate(alienPrefab, spawnPosition, Quaternion.identity);
            activeAliens++;

            AlienHealth alienHealth = alien.GetComponent<AlienHealth>();
            if (alienHealth != null)
            {
                alienHealth.OnDeath += OnAlienDeath;
            }
        }
    }

    void OnAlienDeath()
    {
        activeAliens--;

        if (activeAliens <= 0)
        {
            spawnLimit++;
            CalculateSpawnArea(); // recalcular por si el tamaño cambió
            SpawnWave();
        }
    }
}
