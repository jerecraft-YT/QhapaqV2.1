using UnityEngine;

public class Flecha : MonoBehaviour
{
    public float speed = 10.0f;
    public float timeLive = 3.0f;
    private void Start()
    {
        Destroy(gameObject,timeLive);
    }

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }
}
