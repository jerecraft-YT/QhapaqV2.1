using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Playerinput input;
    [SerializeField] private HudController hudController;
    [SerializeField] private Transform attackReference;
    public int vidaJugador = 3;
    public int dashDisponibles = 0;
    public bool llaveObtenida;
    [SerializeField] private float cooldownRecibirDaño = 1.0f;
    [SerializeField] private SpriteRenderer burbujaDialogo;
    public bool puedeDialogar = false;
    [SerializeField] private float speed = 4.0f;
    [SerializeField] private float cooldownAtacar = 0.1f;
    [SerializeField] private float dashSpeed = 10.0f;
    [SerializeField] private float cooldownDash = 0.2f;
    [SerializeField] private float duracionDash = 0.5f;
    [SerializeField] private float recuperacionDash = 2.5f;
    public int flechasDisponibles = 0;
    //ponemos una direccion por defecto
    private Vector3 ultimaDireccion = Vector2.right;
    private bool haciendoDash;
    private float timeCooldownDash;
    private float timeDuracionDash;
    public GameObject flechaPrefab;
    public GameObject espadaPrefab;
    public GameObject slashEspadaPrefab;
    private float timeCooldownAtacar;
    private float timeCooldownRecibirDaño;
    private float timeRecuperacionDash;

    public bool EspadaOArcoEquipado = true;
    public bool poderCambiarArma = true;

    public bool estaMoviendose;

    public bool isDeath;
    public float timeDeath;
    public float opacidadDeath;
    public float velocidadOpacidadDeath = 2.0f;

    private SpriteRenderer spritePlayer;

    private void Start()
    {
        timeRecuperacionDash = recuperacionDash;

        spritePlayer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (isDeath)
        {
            DeathController();
            return;
        }

        MovePlayer(input.direccionMovimiento.x, input.direccionMovimiento.y);
        DashPlayer();
        ShotPlayer();

        //esto es aparte del movimiento
        ControlDialogo();
        CambiarArma();
        LiveController();
        HudController();
        RecuperacionDash();
    }

    private void DeathController()
    {
        timeDeath += Time.deltaTime;

        if (timeDeath > 2)
        {
            opacidadDeath += Time.deltaTime * velocidadOpacidadDeath;

            Color colorOpacidad = hudController.transicionMorir.color;

            colorOpacidad.a = opacidadDeath;

            hudController.transicionMorir.color = colorOpacidad;


        }

        if (timeDeath > 4)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void RecuperacionDash()
    {
        timeRecuperacionDash -= Time.deltaTime;

        if (timeRecuperacionDash < 0)
        {
            timeRecuperacionDash = recuperacionDash;
            dashDisponibles++;
            if (dashDisponibles > 5)
            {
                dashDisponibles = 5;
            }
        }
    }

    private void HudController()
    {
        hudController.vida = vidaJugador;
        hudController.espadaOArcoSeleccionado = EspadaOArcoEquipado;
        hudController.dashDisponibles = dashDisponibles;
        hudController.llaveObtenida = llaveObtenida;
        hudController.numeroFlechas = flechasDisponibles;
    }

    private void LiveController()
    {
        timeCooldownRecibirDaño -= Time.deltaTime;

        if (timeCooldownRecibirDaño > 0)
        {

        }

        if (vidaJugador <= 0)
        {
            isDeath = true;
            
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
        if (flechasDisponibles <= 0)
        {
            EspadaOArcoEquipado = true;
        }

        if (input.presionoBotonCambiarArma == true && flechasDisponibles > 0)
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

        if (EspadaOArcoEquipado)
        {
            cooldownAtacar = 0.45f;
        }
        else
        {
            cooldownAtacar = 0.2f;
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
                if (direccion.x < 0.0f)
                {
                    spritePlayer.flipX = false;
                }
                else
                {
                    spritePlayer.flipX = true;
                }
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

        if (presionoBotonDash && timeCooldownDash <= 0 && dashDisponibles > 0)
        {
            timeRecuperacionDash = recuperacionDash;
            dashDisponibles--;
            timeCooldownRecibirDaño = duracionDash + 0.25f;
            haciendoDash = true;
            timeDuracionDash = 0.0f;
            print("Dash");
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

            if (EspadaOArcoEquipado == true)
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
        Destroy(slash, 0.15f);
    }

    private void CrearFlecha(Vector3 MousePos)
    {
        flechasDisponibles--;

        print("atacar flecha");

        Vector3 playerPosition = attackReference.transform.position; 

        Vector3 direction = MousePos - playerPosition;
        direction.z = 0.0f;
        direction.Normalize();
        
        //Quaternion identity representa rotacion 0 del objeto osea 0 grados
        GameObject flecha = Instantiate(flechaPrefab, playerPosition, Quaternion.identity);

        flecha.transform.up = direction;
    }

    public void ObtenerItem(lootCaja item)
    {
        print("obtuviste un/a: " + item);

        switch (item)
        {
            case lootCaja.None:
                break;
            case lootCaja.RecargaDash:
                dashDisponibles += 2;
                if (dashDisponibles > 5)
                {
                    dashDisponibles = 5;
                }
                break;
            case lootCaja.Vida:
                vidaJugador++;
                if (vidaJugador > 3)
                {
                    vidaJugador = 3;
                }
                break;
            case lootCaja.Arco:
                flechasDisponibles += Random.Range(3,6);
                break;
            default:
                break;
        }
    }
}
