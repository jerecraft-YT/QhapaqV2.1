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
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();

            QuitarVidaEnemigo(enemyController);

            CrearEfecto(enemyController);
        }

        if (collision.gameObject.tag == "Caja")
        {
            print("detecte caja");

            objetoInteractuable objetoInteractuable = collision.gameObject.GetComponent<objetoInteractuable>();

            objetoInteractuable.OpenBox();

            float randomRotation = Random.Range(0.0f, 360.0f);

            GameObject efectoHit = Instantiate(efectoHitPrefab, objetoInteractuable.transform.position, Quaternion.Euler(0.0f, 0.0f, randomRotation));
            Destroy(efectoHit, 0.2f);
        }

        Destroy(gameObject);
    }

    private void QuitarVidaEnemigo(EnemyController enemyController)
    {
        enemyController.vidaEnemigo -= dañoFlecha;

        enemyController.tiempoQuieto = 0.3f;

        enemyController.EnemigoGolpeado();
    }

    private void CrearEfecto(EnemyController enemyController)
    {
        float randomRotation = Random.Range(0.0f, 360.0f);

        GameObject efectoHit = Instantiate(efectoHitPrefab, enemyController.transform.position, Quaternion.Euler(0.0f, 0.0f, randomRotation));
        Destroy(efectoHit, 0.2f);
    }
}
