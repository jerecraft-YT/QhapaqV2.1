using UnityEngine;

public class Flecha : MonoBehaviour
{
    public float speed = 10.0f;
    public float timeLive = 3.0f;
    public float dañoFlecha = 5.0f;
    public GameObject efectoHitPrefab;
    private void Start()
    {
        Destroy(gameObject,timeLive);
    }

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            QuitarVidaEnemigo(collision);

            CrearEfecto();
        }

        Destroy(gameObject);
    }

    private void QuitarVidaEnemigo(Collision2D collision)
    {
        EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();

        enemyController.vidaEnemigo -= dañoFlecha;

        enemyController.EnemigoGolpeado();
    }

    private void CrearEfecto()
    {
        float randomRotation = Random.Range(0.0f, 360.0f);

        GameObject efectoHit = Instantiate(efectoHitPrefab, transform.position, Quaternion.Euler(0.0f, 0.0f, randomRotation));
        Destroy(efectoHit, 0.2f);
    }
}
