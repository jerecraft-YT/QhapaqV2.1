using UnityEngine;

public class Espada : MonoBehaviour
{
    public float timeLive = 0.15f;
    public float dañoEspada = 2.0f;
    public float range = 1.0f;
    public GameObject efectoHitPrefab;
    private void Start()
    {
        Destroy(gameObject,timeLive);
        transform.position += transform.up * range;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();

            enemyController.vidaEnemigo -= dañoEspada;

            enemyController.tiempoQuieto = 0.35f;

            enemyController.EnemigoGolpeado();

            float randomRotation = Random.Range(0.0f, 360.0f);

            GameObject efectoHit = Instantiate(efectoHitPrefab, enemyController.transform.position, Quaternion.Euler(0.0f,0.0f,randomRotation));
            Destroy(efectoHit, 0.2f);
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
}
