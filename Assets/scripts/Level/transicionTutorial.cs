using UnityEngine;
using UnityEngine.UI;

public class transicionTutorial : MonoBehaviour
{
    public Image transicion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transicion = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Color color = transicion.color;

        color.a -= Time.deltaTime;

        transicion.color = color;

        if (color.a < 0)
        {
            Destroy(gameObject);
        }
    }
}
