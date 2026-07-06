using UnityEngine;
using UnityEngine.Rendering.UI;

public class SpawnearEnemigos : MonoBehaviour
{
    public GameObject enemigo;
    public float timeToSpawn = 3;
    public float radius = 3;
    public bool puedeTenerLLave = false;
    [SerializeField] private int numeroEnemigosRonda = 5;

    // Update is called once per frame
    void Update()
    {
        SpawnController();
    }

    void SpawnController()
    {
        if (numeroEnemigosRonda == 0)
        {
            return;
        }

        timeToSpawn -= Time.deltaTime;

        if (timeToSpawn < 0)
        {
            SpawnEnemy();
            timeToSpawn = 3;
        }
    }

    void SpawnEnemy()
    {
        float probabilidadLLave = Random.Range(0, 100);

        //restamos 1 a la cantidad de enemigos por ronda
        numeroEnemigosRonda -= 1;

        if (numeroEnemigosRonda == 0)
        {
            print("la ronda de enemigos acabo");
        }

        GameObject enemy = Instantiate(enemigo);

        EnemyController enemigoScript = enemy.GetComponent<EnemyController>();
        //enemigoScript.targetEnemy = Target;

        if (puedeTenerLLave)
        {
            if (probabilidadLLave > 60 || numeroEnemigosRonda == 0)
            {
                puedeTenerLLave = false;
                enemigoScript.tieneLlave = true;
            }
        }

        Vector3 randomDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f).normalized;

        enemy.transform.position = transform.position + randomDirection * radius;

    }
}
