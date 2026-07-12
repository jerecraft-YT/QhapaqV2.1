using UnityEngine;

public class SpawnearEnemigos : MonoBehaviour
{
    public GameObject larvaPrefab;
    public GameObject saltamontesPrefab;
    public enemyToSpawn enemyToSpawn;
    public float timeToSpawn = 3;
    public float radius = 3;
    public bool puedeTenerLlave = false;
    [SerializeField] private int numeroEnemigosRonda = 5;
    private PlayerController playerController;

    const float PLAYER_DISTANCE_TO_SPAWN = 20.0f;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnController();
    }

    void SpawnController()
    {
        if (!EstaCercaJugador()) return;

        if (numeroEnemigosRonda == 0) return;

        timeToSpawn -= Time.deltaTime;

        if (timeToSpawn < 0)
        {
            SpawnEnemy();
            timeToSpawn = 3;
        }
    }

    private bool EstaCercaJugador()
    {
        Vector3 playerPos = playerController.transform.position;

        Vector3 myPos = transform.position;

        float distance = Vector3.Distance(myPos, playerPos);

        if (distance < PLAYER_DISTANCE_TO_SPAWN)
        {
            return true;
        }

        return false;
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

        GameObject enemy = null;

        switch (enemyToSpawn)
        {
            case enemyToSpawn.None:
                return;

            case enemyToSpawn.Larva:
                enemy = Instantiate(larvaPrefab);
                break;

            case enemyToSpawn.Saltamontes:
                enemy = Instantiate(saltamontesPrefab);
                break;

            default:
                return;
        }

        Vector3 randomDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f).normalized;

        enemy.transform.position = transform.position + randomDirection * radius;

        EnemyController enemigoScript = enemy.GetComponent<EnemyController>();

        if (puedeTenerLlave)
        {
            if (probabilidadLLave > 60 || numeroEnemigosRonda == 0)
            {
                puedeTenerLlave = false;
                enemigoScript.tieneLlave = true;
            }
        }
    }
}
