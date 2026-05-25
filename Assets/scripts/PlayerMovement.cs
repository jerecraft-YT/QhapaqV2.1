using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Playerinput pInput;
    public float speed;
    // Update is called once per frame
    void Update()
    {
        // rb.AddForceX(pInput.direccionX * speed);
        rb.linearVelocityX = pInput.direccionX * speed;
    }
}
