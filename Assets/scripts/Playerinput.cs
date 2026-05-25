using UnityEngine;

public class Playerinput : MonoBehaviour
{
    public float direccionX;
    

    void Update()
    {
        DetectarMovimiento();
    }

    private void DetectarMovimiento()
    {
        direccionX = Input.GetAxisRaw("Horizontal");
        print(direccionX);

    }
}
