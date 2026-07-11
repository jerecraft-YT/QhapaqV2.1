using UnityEngine;

public class objetoInteractuable : MonoBehaviour
{
    private PlayerDialogController playerDialog;
    private PlayerController playerController;

    public float distanceToInteract = 4.0f;
    private SpriteRenderer sprite;
    public lootCaja contenidoCaja;
    private bool JugadorEstaCerca = false;
    public GameObject iconoPrefab;
    public Transform refIcono;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        playerDialog = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDialogController>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        ControlOrden();

        InteractController();
    }

    private void InteractController()
    {
        float distance = Vector3.Distance(transform.position, playerDialog.transform.position);

        if (distance <= distanceToInteract)
        {
            playerDialog.puedeInteractuar = true;
            JugadorEstaCerca = true;

            if (Input.GetKey(KeyCode.E))
            {
                OpenBox();
            }
        }
        else if (distance > distanceToInteract && JugadorEstaCerca)
        {
            JugadorEstaCerca = false;
            playerDialog.puedeInteractuar = false;
        }
    }


    //esta funcion ayuda a que el objeto se vea delante o detras del jugador
    private void ControlOrden()
    {
        if (playerDialog.transform.position.y > transform.position.y)
        {
            sprite.sortingOrder = 1;
        }
        else
        {
            sprite.sortingOrder = -1;
        }
    }

    public void OpenBox()
    {
        playerController.ObtenerItem(contenidoCaja);

        playerDialog.puedeInteractuar = false;

        MostrarObjetoObtenido();

        Destroy(gameObject);
    }

    private void MostrarObjetoObtenido()
    {
        GameObject iconoCaja = Instantiate(iconoPrefab, refIcono.position, Quaternion.identity);
        IconoCaja scriptIcono = iconoCaja.GetComponent<IconoCaja>();

        scriptIcono.contenidoCaja = contenidoCaja;

        Destroy(iconoCaja, 0.25f);
    }
}
