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

    //como necesitamos el rigidBody del jugador para moverlo y que choque entonces lo obtenemos
    //y lo guardamos en una variable, recuerda establecerlo desde el inspector
    [SerializeField] private Rigidbody2D rb;

    //una cosa es que puedes ponerle valores por defecto a tus variables, esto te evita
    //que tengas que ir al inspector a cambiarle el valor cada vez que creas uno nuevo
    [SerializeField] private float speed = 4.0f;

    [SerializeField] private float dashSpeed = 10.0f;
    [SerializeField] private float cooldownDash = 0.2f;
    [SerializeField] private float duracionDash = 0.5f;
    //ponemos una direccion por defecto
    private Vector2 ultimaDireccion = Vector2.right;
    private bool haciendoDash;
    private float timeCooldownDash;
    private float timeDuracionDash;

    //cuando trabajes con fisicas es mejor usar FixedUpdate que Update
    void FixedUpdate()
    {
        MovePlayer();
        DashPlayer();
    }
    private void MovePlayer()
    {
        //si esta haciendo dash no dejaremos que te muevas por tu cuenta
        if (haciendoDash)
        {
            return;
        }
        //esta seria la forma mas facil de mover al jugador pero si seguimos por este camino, sufriremos mucho para las colisiones
        //basicamente porque tendriamos que hacerlas nosotros
        //transform.position += new Vector3(input.direccionMovimiento.x,input.direccionMovimiento.y,0.0f) * speed * Time.deltaTime;

        //es mejor aprovechar el rigidBody2d, linear velocity le da una velocidad constante, ademas como son fisicas no es necesario
        //usar Time.deltaTime ya que el mismo motor de fisicas lo integra
        rb.linearVelocity = new Vector2(input.direccionMovimiento.x, input.direccionMovimiento.y) * speed;
    }

    private void DashPlayer()
    {
        if (!haciendoDash)
        {
            timeCooldownDash -= Time.deltaTime;

            //esto sirve para obtener la ultima direccion en la que te moviste
            if (input.direccionMovimiento != Vector2.zero)
            {
                ultimaDireccion = input.direccionMovimiento;
            }

            if (input.presionoBotonDash && timeCooldownDash <= 0)
            {
                haciendoDash = true;
                timeDuracionDash = 0.0f;
            }
        }
        else
        {
            if (timeDuracionDash > duracionDash)
            {
                timeDuracionDash = 0.0f;
                timeCooldownDash = cooldownDash;
                haciendoDash = false;
                return;
            }

            rb.linearVelocity = ultimaDireccion * dashSpeed;
            timeDuracionDash += Time.deltaTime;
        }
    }
}
