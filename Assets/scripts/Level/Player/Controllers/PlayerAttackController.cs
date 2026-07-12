using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public PlayerInput input;
    public PlayerController playerController;

    public GameObject flechaPrefab;
    public GameObject espadaPrefab;
    public GameObject slashEspadaPrefab;
    public bool poderCambiarArma = true;

    private float timeCooldownAtacar;
    [SerializeField] private float cooldownAtacar = 0.4f;

    [SerializeField] private Transform attackReference;

    // Update is called once per frame
    void Update()
    {
        ShotPlayer();
        CambiarArma();
    }

    private void CambiarArma()
    {
        if (playerController.flechasDisponibles <= 0)
        {
            playerController.EspadaOArcoEquipado = true;
        }

        if (input.presionoBotonCambiarArma == true && playerController.flechasDisponibles > 0)
        {
            if (poderCambiarArma == true)
            {
                playerController.EspadaOArcoEquipado = !playerController.EspadaOArcoEquipado;
                poderCambiarArma = false;
            }
        }
        else
        {
            poderCambiarArma = true;
        }

        if (playerController.EspadaOArcoEquipado)
        {
            cooldownAtacar = 0.45f;
        }
        else
        {
            cooldownAtacar = 0.2f;
        }
    }

    private void ShotPlayer()
    {
        timeCooldownAtacar -= Time.deltaTime;

        if (input.presionoBotonAtacar == true && timeCooldownAtacar < 0.0f)
        {
            timeCooldownAtacar = cooldownAtacar;

            //le pasamos a la funcion la posicion actual del mouse
            Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (playerController.EspadaOArcoEquipado == true)
            {
                CrearEspada(MousePos);
            }
            else
            {
                CrearFlecha(MousePos);
            }
        }
    }

    private void CrearEspada(Vector3 MousePos)
    {
        print("atacar espada");

        Vector3 playerPosition = attackReference.transform.position;

        Vector3 direction = MousePos - playerPosition;
        direction.z = 0.0f;
        direction.Normalize();

        //Quaternion identity representa rotacion 0 del objeto osea 0 grados
        GameObject espada = Instantiate(espadaPrefab, playerPosition, Quaternion.identity);

        espada.transform.up = direction;

        GameObject slash = Instantiate(slashEspadaPrefab, playerPosition, Quaternion.identity);
        slash.transform.up = direction;
        Destroy(slash, 0.25f);
    }

    private void CrearFlecha(Vector3 MousePos)
    {
        playerController.flechasDisponibles--;

        print("atacar flecha");

        Vector3 playerPosition = attackReference.transform.position;

        Vector3 direction = MousePos - playerPosition;
        direction.z = 0.0f;
        direction.Normalize();

        //Quaternion identity representa rotacion 0 del objeto osea 0 grados
        GameObject flecha = Instantiate(flechaPrefab, playerPosition, Quaternion.identity);

        flecha.transform.up = direction;
    }
}
