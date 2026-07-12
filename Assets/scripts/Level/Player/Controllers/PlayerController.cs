using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInput input;
    private PlayerLiveController liveController;
    private PlayerDashController dashController;

    public SpriteRenderer spritePlayer;
    public CameraController cameraPlayer;
    public HudController hudController;

    public int dashDisponibles = 0;

    public int flechasDisponibles = 0;

    public bool llaveObtenida;

    [SerializeField] private float walkSpeed = 4.0f;

    public bool EspadaOArcoEquipado = true;

    public bool estaMoviendose;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        liveController = GetComponent<PlayerLiveController>();
        dashController = GetComponent<PlayerDashController>();

        cameraPlayer = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }

    void FixedUpdate()
    {
        MovePlayer(input.direccionMovimiento.x, input.direccionMovimiento.y);

        UpdateHUD();
    }


    public void UpdateHUD()
    {
        hudController.vida = liveController.vidaJugador;
        hudController.espadaOArcoSeleccionado = EspadaOArcoEquipado;
        hudController.dashDisponibles = dashDisponibles;
        hudController.llaveObtenida = llaveObtenida;
        hudController.numeroFlechas = flechasDisponibles;
    }

    //esta funcion sirve para mover al jugador y le pasamos las direcciones del script de input
    private void MovePlayer(float direccionMovimientoX,float direccionMovimientoY)
    {
        estaMoviendose = false;

        //si esta haciendo dash no dejaremos que te muevas por tu cuenta
        if (dashController.haciendoDash == false)
        {
            
            Vector3 direccion = new Vector3(direccionMovimientoX, direccionMovimientoY, 0.0f);

            if (direccion != Vector3.zero)
            {
                FlipSpritePlayer(direccion);
            }

            //mueve al jugador
            transform.position += direccion * walkSpeed * Time.deltaTime;
        }
    }

    private void FlipSpritePlayer(Vector3 direccion)
    {
        if (direccion.x < 0.0f)
        {
            spritePlayer.flipX = false;
        }
        else if (direccion.x > 0.0f)
        {
            spritePlayer.flipX = true;
        }

        estaMoviendose = true;
    }

    public void ObtenerDash(int cantidad)
    {
        dashDisponibles += cantidad;
        if (dashDisponibles > 5)
        {
            dashDisponibles = 5;
        }
    }

    public void ObtenerFlechas(int cantidad)
    {
        flechasDisponibles += cantidad;
    }

    public void ObtenerItem(lootCaja item)
    {
        print("obtuviste un/a: " + item);

        switch (item)
        {
            case lootCaja.None:
                break;
            case lootCaja.RecargaDash:
                ObtenerDash(3);
                break;
            case lootCaja.Vida:
                liveController.GanarVida(1);
                break;
            case lootCaja.Arco:
                ObtenerFlechas(Random.Range(3, 8));
                break;
            default:
                break;
        }
    }
}
