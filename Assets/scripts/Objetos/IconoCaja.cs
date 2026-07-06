using UnityEngine;

public class IconoCaja : MonoBehaviour
{
    public Sprite[] iconos;
    public lootCaja contenidoCaja;
    public SpriteRenderer sprite;
    public float speed = 2.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        switch (contenidoCaja)
        {
            case lootCaja.None:
                break;
            case lootCaja.RecargaDash:
                sprite.sprite = iconos[2];
                break;
            case lootCaja.Vida:
                sprite.sprite = iconos[0];
                break;
            case lootCaja.Arco:
                sprite.sprite = iconos[1];
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + Vector3.up * Time.deltaTime * speed;
    }
}
