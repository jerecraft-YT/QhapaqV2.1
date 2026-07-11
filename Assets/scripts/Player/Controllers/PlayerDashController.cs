using UnityEngine;

public class PlayerDashController : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerLiveController liveController;

    [SerializeField] private float dashSpeed = 20.0f;
    [SerializeField] private float cooldownDash = 0.4f;
    [SerializeField] private float duracionDash = 0.15f;
    [SerializeField] private float recuperacionDash = 5f;

    private Vector3 ultimaDireccion = Vector2.right;
    public bool haciendoDash;
    private float timeCooldownDash;
    private float timeDuracionDash;
    private float timeRecuperacionDash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeRecuperacionDash = recuperacionDash;
    }

    // Update is called once per frame
    void Update()
    {
        DashPlayer();
        RecuperacionDash();
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
    private void ControlNoHacerDash(Vector2 direccionMovimiento, bool presionoBotonDash)
    {
        timeCooldownDash -= Time.deltaTime;

        //esto sirve para obtener la ultima direccion en la que te moviste
        if (direccionMovimiento != Vector2.zero)
        {
            ultimaDireccion = direccionMovimiento;
        }

        if (presionoBotonDash && timeCooldownDash <= 0 && playerController.dashDisponibles > 0)
        {
            timeRecuperacionDash = recuperacionDash;
            playerController.dashDisponibles--;

            liveController.VolverInvulnerable(duracionDash + 0.25f);

            haciendoDash = true;
            timeDuracionDash = 0.0f;
            print("Dash");
        }
    }

    private void RecuperacionDash()
    {
        timeRecuperacionDash -= Time.deltaTime;

        if (timeRecuperacionDash < 0)
        {
            timeRecuperacionDash = recuperacionDash;

            playerController.ObtenerDash(1);
        }
    }
}
