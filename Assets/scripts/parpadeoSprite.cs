using UnityEngine;

public class parpadeoSprite : MonoBehaviour
{
    public NpcController npcController;
    public EnemyController enemyController;
    private SpriteRenderer spriteRenderer;
    public bool parpadear;
    private bool puedeActivarse = true;
    public bool subirBajarColor = false;
    public float timeToReset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (npcController.dialogando && puedeActivarse)
        {
            puedeActivarse = false;
            parpadear = true;
            timeToReset = 2.0f;
        }

        if (parpadear)
        {
            timeToReset -= Time.deltaTime;

            Color color = spriteRenderer.color;

            if (subirBajarColor)
            {
                color.a += Time.deltaTime * 2.0f;
            }
            else
            {
                color.a -= Time.deltaTime * 2.0f;
            }

            if (color.a < 0.5f)
            {
                subirBajarColor = true;
            }

            if (color.a > 1.1f)
            {
                subirBajarColor = false;
            }
            spriteRenderer.color = color;
        }

        if (timeToReset < 0.0f && parpadear == true)
        {
            puedeActivarse = true;
            parpadear = false;
            Color color = spriteRenderer.color;

            color.a = 1.0f;

            spriteRenderer.color = color;
        }

        if (enemyController == null)
        {
            return;
        }

        if (enemyController.tiempoQuieto > 0)
        {
            parpadear = false;
        }
    }
}
