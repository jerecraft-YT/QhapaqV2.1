using UnityEngine;

public class Playerinput : MonoBehaviour
{
    public bool presionoBotonDash;
    public bool presionoBotonAtacar;

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
        presionoBotonDash = Input.GetKey(KeyCode.LeftControl);
        presionoBotonAtacar = Input.GetMouseButtonDown(0);
    }
}
