using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    [Header("Paths")]
    public Transform[] path1;
    public Transform[] path2;
    public Transform[] path3;

    [Header("Spawn Points")]
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;

    [Header("Spawn Settings")]
    public float spawnInterval = 2f;        // initial spawn interval
    public int enemiesBeforeSpeedUp = 25;   // after how many enemies to increase speed
    public float spawnSpeedMultiplier = 0.2f; // reduce interval by multiplying
    public float minSpawnInterval = 1.5f;  

    private float timer = 0f;
    private int spawnedEnemies = 0;
    


    private void Start()
    {
        
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        int randomPath = Random.Range(1, 4); // pick 1–3

        Transform[] selectedPath = null;
        Transform spawnPoint = null;

        switch (randomPath)
        {
            case 1:
                selectedPath = path1;
                spawnPoint = spawnPoint1;
                break;
            case 2:
                selectedPath = path2;
                spawnPoint = spawnPoint2;
                break;
            case 3:
                selectedPath = path3;
                spawnPoint = spawnPoint3;
                break;
        }

        if (selectedPath != null && spawnPoint != null)
        {
            // spawn enemy
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            // assign path
            enemy.GetComponent<EnemyMovement>().waypoints = selectedPath;

            // count total spawns
            spawnedEnemies++;

            // after every 25 → increase spawn speed
            if (spawnedEnemies % enemiesBeforeSpeedUp == 0)
            {
                spawnInterval = Mathf.Max(spawnInterval * spawnSpeedMultiplier, minSpawnInterval);
                Debug.Log("Increased difficulty! New spawn interval: " + spawnInterval);
            }
        }
    }
}
