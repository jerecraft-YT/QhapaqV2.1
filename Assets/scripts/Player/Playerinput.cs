using UnityEngine;

public class Playerinput : MonoBehaviour
{
    public bool presionoBotonDash;
    public bool presionoBotonAtacar;
    public bool presionoBotonCambiarArma;

    public Vector2 direccionMovimiento;
    
    private void Update()
    {
        DetectarMovimiento();
        DetectarBotones();
    }
    private void DetectarMovimiento()
    {
        float direccionX = Input.GetAxisRaw("Horizontal");
        float direccionY = Input.GetAxisRaw("Vertical");
        direccionMovimiento = new Vector2(direccionX, direccionY).normalized;
    }

    private void DetectarBotones()
    {
        presionoBotonDash = Input.GetKey(KeyCode.LeftShift);
        presionoBotonAtacar = Input.GetMouseButton(0);
        presionoBotonCambiarArma = Input.GetKey(KeyCode.Tab);
    }
}
