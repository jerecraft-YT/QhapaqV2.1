using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Playerinput input;
    [SerializeField] private SpriteRenderer burbujaDialogo;
    public bool puedeDialogar = false;
    [SerializeField] private float speed = 4.0f;

    [SerializeField] private float dashSpeed = 10.0f;
    [SerializeField] private float cooldownDash = 0.2f;
    [SerializeField] private float duracionDash = 0.5f;
    //ponemos una direccion por defecto
    private Vector3 ultimaDireccion = Vector2.right;
    private bool haciendoDash;
    private float timeCooldownDash;
    private float timeDuracionDash;
    public GameObject flechaPrefab;
    void FixedUpdate()
    {
        MovePlayer(input.direccionMovimiento.x, input.direccionMovimiento.y);
        DashPlayer();
        ShotPlayer();

        //esto es aparte del movimiento
        ControlDialogo();
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
        //si esta haciendo dash no dejaremos que te muevas por tu cuenta
        if (haciendoDash == false)
        {
            //mueve al jugador
            Vector3 direccion = new Vector3(direccionMovimientoX, direccionMovimientoY, 0.0f);

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
        if (input.presionoBotonAtacar == true)
        {
            //le pasamos a la funcion la posicion actual del mouse
            Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            CrearFlecha(MousePos);
        }
    }
    private void CrearFlecha(Vector3 MousePos)
    {
        print("atacar");

        Vector3 playerPosition = transform.position; 

        Vector3 direction = MousePos - playerPosition;
        direction.z = 0.0f;
        direction.Normalize();
        
        //Quaternion identity representa rotacion 0 del objeto osea 0 grados
        GameObject flecha = Instantiate(flechaPrefab, playerPosition, Quaternion.identity);

        flecha.transform.up = direction;
    }

    public void ObtenerItem(string item)
    {
        print("obtuviste un/a: " + item);
    }
}
