using UnityEngine;

public class Espada : MonoBehaviour
{
    public float timeLive = 0.15f;
    public float dañoEspada = 2.0f;
    public float range = 1.0f;
    public GameObject efectoHitPrefab;

    const float ENEMY_STUN_TIME = 0.35f;
    const float EFFECT_HIT_LIVETIME = 0.2f;

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

            enemyController.tiempoQuieto = ENEMY_STUN_TIME;

            enemyController.EnemigoGolpeado();

            float randomRotation = Random.Range(0.0f, 360.0f);

            GameObject efectoHit = Instantiate(efectoHitPrefab, 
                enemyController.transform.position, 
                Quaternion.Euler(0.0f,0.0f,randomRotation));

            Destroy(efectoHit, EFFECT_HIT_LIVETIME);
        }

        if (collision.gameObject.tag == "Caja")
        {
            print("detecte caja");

            objetoInteractuable objetoInteractuable = collision.gameObject.GetComponent<objetoInteractuable>();

            objetoInteractuable.OpenBox();

            float randomRotation = Random.Range(0.0f, 360.0f);

            GameObject efectoHit = Instantiate(efectoHitPrefab, objetoInteractuable.transform.position, Quaternion.Euler(0.0f, 0.0f, randomRotation));
            Destroy(efectoHit, EFFECT_HIT_LIVETIME);
        }

        Destroy(gameObject);
    }
}
