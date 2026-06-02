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

    //cuando trabajes con fisicas es mejor usar FixedUpdate que Update
    void Update()
    {
        MovePlayer();
        DashPlayer();
        ShotPlayer();
    }
    private void MovePlayer()
    {
        //si esta haciendo dash no dejaremos que te muevas por tu cuenta
        if (haciendoDash == false)
        {
            //mueve al jugador
            Vector3 direccion = new Vector3(input.direccionMovimiento.x, input.direccionMovimiento.y, 0.0f);

            transform.position += direccion * speed * Time.deltaTime;
        }
    }

    private void DashPlayer()
    {
        if (haciendoDash == false)
        {
            timeCooldownDash -= Time.deltaTime;

            //esto sirve para obtener la ultima direccion en la que te moviste
            if(input.direccionMovimiento != Vector2.zero)
            {
                ultimaDireccion = input.direccionMovimiento;
            }
            
            if (input.presionoBotonDash && timeCooldownDash <= 0)
            {
                haciendoDash = true;
                timeDuracionDash = 0.0f;
                print("Dash");
            }
        }
        if (haciendoDash == true)
        {
            if (timeDuracionDash >= duracionDash)
            {
                timeDuracionDash = 0.0f;
                timeCooldownDash = cooldownDash;
                haciendoDash = false;
                return;
            }
            transform.position += ultimaDireccion * dashSpeed * Time.deltaTime;
            timeDuracionDash += Time.deltaTime;
        }
    }

    private void ShotPlayer()
    {
        if (input.presionoBotonAtacar == true)
        {
            CrearFlecha();
        }
    }

    private void CrearFlecha()
    {
        print("atacar");

        Vector3 myPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = myPos - transform.position;
        direction.z = 0.0f;
        direction.Normalize();

        GameObject flecha = Instantiate(flechaPrefab, transform.position, Quaternion.identity);
        flecha.transform.up = direction;
    }
}
