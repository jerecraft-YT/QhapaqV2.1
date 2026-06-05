using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //como para mover al personaje necesitamos saber si se presiono una
    //tecla entonces buscamos el script que detecta eso

    //obtener un script es super facil, solo en la parte donde tiene que ir el tipo
    //como int, float, bool, etc, ahi le pondrias el nombre de tu script
    //ademas asegurate de hacerlo publico para luego agregar el script desde el inspector

    //estoy usando ([SerializeField] private) porque estas variables solo las usaremos aqui
    //pero como quiero que se vea desde el inspector le ponemos [SerializeField] y ya
    [SerializeField] private Playerinput input;

    //una cosa es que puedes ponerle valores por defecto a tus variables, esto te evita
    //que tengas que ir al inspector a cambiarle el valor cada vez que creas uno nuevo
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

    void Update()
    {
        MovePlayer(input.direccionMovimiento.x, input.direccionMovimiento.y);
        DashPlayer();
        ShotPlayer();
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
            CrearFlecha(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
    //esta funcion sirve para crear el proyectil en la escena
    private void CrearFlecha(Vector3 MousePos)
    {
        print("atacar");

        Vector3 direction = MousePos - transform.position;
        direction.z = 0.0f;
        direction.Normalize();

        GameObject flecha = Instantiate(flechaPrefab, transform.position, Quaternion.identity);
        flecha.transform.up = direction;
    }
}
