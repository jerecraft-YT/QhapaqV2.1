using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform targetCamera;

    [SerializeField] private float CameraSpeed = 2.0f;

    [SerializeField] private float distanceForMove = 1f;

    private float fuerzaShake;

    private Vector3 shakePosition;

    private Vector3 finalPos;

    private float shakeDuration;

    private void Start()
    {
        finalPos = transform.position;
    }

    private void Update()
    {
        CameraMovement();


        transform.position = finalPos + shakePosition;
    }

    private void FixedUpdate()
    {
        ShakeController();
    }

    private void ShakeController()
    {
        shakePosition = new Vector3(Random.Range(-fuerzaShake, fuerzaShake), Random.Range(-fuerzaShake, fuerzaShake),0.0f);
        shakeDuration -= Time.deltaTime;

        if (shakeDuration < 0.0f)
        {
            shakeDuration = 0.0f;
            fuerzaShake = 0.0f;
        }
    }

    public void ScreenShake(float fuerza,float duracion)
    {
        fuerzaShake = fuerza; 
        shakeDuration = duracion;
    }
    private void CameraMovement()
    {
        if (targetCamera == null) return;

        //este codigo antes era de un enemigo XD

        Vector3 targetPos = targetCamera.position;
        Vector3 myPos = transform.position;

        float distance = Vector2.Distance(myPos, targetPos);

        if (distance < distanceForMove)
        {
            return;
        }

        Vector3 direccionMovimiento = (targetPos - myPos).normalized;

        direccionMovimiento.z = 0.0f;

        finalPos += Time.deltaTime * CameraSpeed * direccionMovimiento;
    }
}
