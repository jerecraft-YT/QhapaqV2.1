using UnityEngine;

public class fijarManiqui : MonoBehaviour
{
    public Transform basePosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        basePosition = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = basePosition.position;
        transform.rotation = basePosition.rotation;
    }
}
