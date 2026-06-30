using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Playerinput input;
    [SerializeField] private HudController hudController;
    public int vidaJugador = 3;
    public int dashDisponibles = 0;
    public bool llaveObtenida;
    [SerializeField] private float cooldownRecibirDaño = 1.0f;
    [SerializeField] private SpriteRenderer burbujaDialogo;
    public bool puedeDialogar = false;
    [SerializeField] private float speed = 4.0f;
    [SerializeField] private float cooldownDispararFlecha = 0.1f;
    [SerializeField] private float dashSpeed = 10.0f;
    [SerializeField] private float cooldownDash = 0.2f;
    [SerializeField] private float duracionDash = 0.5f;
    //ponemos una direccion por defecto
    private Vector3 ultimaDireccion = Vector2.right;
    private bool haciendoDash;
    private float timeCooldownDash;
    private float timeDuracionDash;
    public GameObject flechaPrefab;
    private float timeCooldownFlecha;
    private float timeCooldownRecibirDaño;

    public bool EspadaOArcoEquipado = true;
    public bool poderCambiarArma = true;

    public bool estaMoviendose;

    void FixedUpdate()
    {
        MovePlayer(input.direccionMovimiento.x, input.direccionMovimiento.y);
        DashPlayer();
        ShotPlayer();

        //esto es aparte del movimiento
        ControlDialogo();
        CambiarArma();
        LiveController();
        HudController();
    }

    private void HudController()
    {
        hudController.vida = vidaJugador;
        hudController.espadaOArcoSeleccionado = EspadaOArcoEquipado;
        hudController.dashDisponibles = dashDisponibles;
        hudController.llaveObtenida = llaveObtenida;
    }

    private void LiveController()
    {
        timeCooldownRecibirDaño -= Time.deltaTime;

        if (timeCooldownRecibirDaño > 0)
        {

        }
    }

    public void QuitarVida()
    {
        if (timeCooldownRecibirDaño < 0)
        {
            timeCooldownRecibirDaño = cooldownRecibirDaño;
            vidaJugador -= 1;
        }

    }

    private void CambiarArma()
    {
        if (input.presionoBotonCambiarArma == true )
        {
            if (poderCambiarArma == true)
            {
                EspadaOArcoEquipado = !EspadaOArcoEquipado;
                poderCambiarArma = false;
            }
        }
        else
        {
            poderCambiarArma = true;
        }
    }

    private void ControlDialogo()
    {
        if (puedeDialogar == true)
        {
            burbujaDialogo.enabled = true;
        }
        if (puedeDialogar == false)
        {
            burbujaDialogo.enabled = false;
        }
    }

    //esta funcion sirve para mover al jugador y le pasamos las direcciones del script de input
    private void MovePlayer(float direccionMovimientoX,float direccionMovimientoY)
    {
        estaMoviendose = false;

        //si esta haciendo dash no dejaremos que te muevas por tu cuenta
        if (haciendoDash == false)
        {
            //mueve al jugador
            Vector3 direccion = new Vector3(direccionMovimientoX, direccionMovimientoY, 0.0f);

            if (direccion != Vector3.zero)
            {
                estaMoviendose = true;
            }

            transform.position += direccion * speed * Time.deltaTime;
        }
    }

    private void DashPlayer()
    {
        if (haciendoDash == false)
        {
            ControlNoHacerDash(input.direccionMovimiento, input.presionoBotonDash);
        }
        if (haciendoDash == true)
        {
            ControlHacerDash();
        }
    }

    private void ControlHacerDash()
    {
        if (timeDuracionDash >= duracionDash && haciendoDash == true)
        {
            timeDuracionDash = 0.0f;
            timeCooldownDash = cooldownDash;
            haciendoDash = false;
            return;
        }
        else
        {
            //mover al jugador si hace el dash
            transform.position += ultimaDireccion * dashSpeed * Time.deltaTime;
            timeDuracionDash += Time.deltaTime;
        }
    }
    //esta funcion sirve para controlar todo lo que debe pasar mientras no haces el dash
    //como el tiempo de espera entre dash o que te deje activarlo
    private void ControlNoHacerDash(Vector2 direccionMovimiento,bool presionoBotonDash)
    {
        timeCooldownDash -= Time.deltaTime;

        //esto sirve para obtener la ultima direccion en la que te moviste
        if (direccionMovimiento != Vector2.zero)
        {
            ultimaDireccion = direccionMovimiento;
        }

        if (presionoBotonDash && timeCooldownDash <= 0)
        {
            haciendoDash = true;
            timeDuracionDash = 0.0f;
            print("Dash");
        }
    }

    private void ShotPlayer()
    {
        timeCooldownFlecha -= Time.deltaTime;

        if (input.presionoBotonAtacar == true && timeCooldownFlecha < 0.0f)
        {
            timeCooldownFlecha = cooldownDispararFlecha;

            //le pasamos a la funcion la posicion actual del mouse
            Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            CrearFlecha(MousePos);
        }
    }
    private void CrearFlecha(Vector3 MousePos)
    {
        print("atacar flecha");

        Vector3 playerPosition = transform.position; 

        Vector3 direction = MousePos - playerPosition;
        direction.z = 0.0f;
        direction.Normalize();
        
        //Quaternion identity representa rotacion 0 del objeto osea 0 grados
        GameObject flecha = Instantiate(flechaPrefab, playerPosition, Quaternion.identity);

        flecha.transform.up = direction;
    }

    private void CrearAtaqueEspada(Vector3 MousePos)
    {

    }

    public void ObtenerItem(string item)
    {
        print("obtuviste un/a: " + item);
    }
}
