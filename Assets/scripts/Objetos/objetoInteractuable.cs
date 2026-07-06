using UnityEngine;

public class objetoInteractuable : MonoBehaviour
{
    private PlayerController player;
    public float distanceToInteract = 4.0f;
    private SpriteRenderer sprite;
    public lootCaja contenidoCaja;
    private bool puedeInteractuar = false;
    public GameObject iconoPrefab;
    public Transform refIcono;

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

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= distanceToInteract)
        {
            player.puedeDialogar = true;
            puedeInteractuar = true;

            if (Input.GetKey(KeyCode.E))
            {
                OpenBox();
            }
        }

        else if (distance > distanceToInteract && puedeInteractuar)
        {
            puedeInteractuar = false;
            player.puedeDialogar = false;
        }
    }

    public void OpenBox()
    {
        player.ObtenerItem(contenidoCaja);
        player.puedeDialogar = false;

        GameObject iconoCaja = Instantiate(iconoPrefab, refIcono.position, Quaternion.identity);
        IconoCaja scriptIcono = iconoCaja.GetComponent<IconoCaja>();

        scriptIcono.contenidoCaja = contenidoCaja;

        Destroy(iconoCaja, 0.25f);

        Destroy(gameObject);
    }
}
