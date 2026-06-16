using UnityEngine;

public class CameraController : MonoBehaviour
{
    //se que por como esta programado el enemigo puede atravesar paredes pero pidieron simple :P

    public Transform targetCamera;

    [SerializeField] private float CameraSpeed = 2.0f;

    [SerializeField] private float distanceForMove = 1f;

    private void Update()
    {
        CameraMovement();
    }

    private void CameraMovement()
    {
        //este codigo antes era de un enemigo XD

        float distance = Vector2.Distance(transform.position,targetCamera.position);

        //comprobamos esto primero ya que si esta muy cerca del jugador podemos evitar calcular el movimiento
        if (distance < distanceForMove)
        {
            return;
        }

        Vector3 direccionMovimiento = (targetCamera.position - transform.position).normalized;

        Vector3 direccionSinZ = new Vector3(direccionMovimiento.x, direccionMovimiento.y, 0.0f);

        transform.position += Time.deltaTime * CameraSpeed * direccionSinZ;
    }
}
