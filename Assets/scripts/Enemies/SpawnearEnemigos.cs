using UnityEngine;

public class SpawnearEnemigos : MonoBehaviour
{
    public GameObject enemigo;
    public float timeToSpawn = 3;
    public float radius = 3;
    public Transform Target;

    // Update is called once per frame
    void Update()
    {
        SpawnController();
    }

    void SpawnController()
    {
        timeToSpawn -= Time.deltaTime;

        if (timeToSpawn < 0)
        {
            SpawnEnemy();
            timeToSpawn = 3;
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemigo);

        EnemyController enemigoScript = enemy.GetComponent<EnemyController>();
        enemigoScript.targetEnemy = Target;

        Vector3 randomDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f).normalized;

        enemy.transform.position = transform.position + randomDirection * Random.Range(0.0f,radius);

    }
}
