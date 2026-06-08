using UnityEngine;

public enum TipoItem
{
    ARCO,
    ESPADA,
    MONEDA
}

public class objetoInteractuable : MonoBehaviour
{
    public PlayerController player;
    public TipoItem tipoItem = TipoItem.ARCO;
    public float timeToRespawn = 2.0f;
    public float distanceToInteract = 4.0f;
    private float timeRespawn;
    private bool interactuable = true;
    private SpriteRenderer sprite;


    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }



    // Update is called once per frame
    void Update()
    {

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
                player.ObtenerItem(tipoItem);
                
                //esto deberia ir en el jugador para verificar el tipo
                switch (tipoItem)
                {
                    case TipoItem.ARCO:

                        break;
                    case TipoItem.ESPADA:

                        break;
                }
            }
        }
    }
}
