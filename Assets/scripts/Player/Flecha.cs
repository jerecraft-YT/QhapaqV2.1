using UnityEngine;

public class Flecha : MonoBehaviour
{
    private float speed = 10.0f;

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }
}
