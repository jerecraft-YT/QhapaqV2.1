using UnityEngine;

public class objetoInteractuable : MonoBehaviour
{
    private PlayerController player;
    public float timeToRespawn = 2.0f;
    public float distanceToInteract = 4.0f;
    private float timeRespawn;
    private bool interactuable = true;
    private SpriteRenderer sprite;
    public lootCaja contenidoCaja;


    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (player.transform.position.y > transform.position.y)
        {
            sprite.sortingOrder = 1;
        }
        else
        {
            sprite.sortingOrder = -1;
        }

        if (!interactuable)
        {
            if (timeRespawn >= timeToRespawn)
            {
                interactuable = true;
                sprite.enabled = true;
            }

            timeRespawn += Time.deltaTime;
            return;
        }

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= distanceToInteract)
        {
            if (Input.GetKey(KeyCode.E))
            {
                interactuable = false;
                sprite.enabled = false;
                timeRespawn = 0.0f;

                switch (contenidoCaja)
                {
                    case lootCaja.None:
                        break;
                    case lootCaja.RecargaDash:
                        player.ObtenerItem("RecargaDash");
                        break;
                    case lootCaja.Vida:
                        player.ObtenerItem("Vida");
                        break;
                    case lootCaja.Arco:
                        player.ObtenerItem("Arco");
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
