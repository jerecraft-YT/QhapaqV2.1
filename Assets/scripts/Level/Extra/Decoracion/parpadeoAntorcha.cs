using UnityEngine;

public class parpadeoAntorcha : MonoBehaviour
{
    float timeChangeGlowScale;
    bool EscalarGlow;
    Vector3 GlowEscalaMinima = new Vector3(0.9f, 0.9f, 0.0f);
    Vector3 GlowEscalaMaxima = new Vector3(1.25f, 1.25f, 0.0f);
    float velocidadEscalado = 0.5f;

    void Start()
    {
        //para que cada efecto tenga un inicio diferente y varien un poquito
        timeChangeGlowScale = Random.Range(0.0f, 1.0f);
        transform.localScale = new Vector3(1.0f,1.0f,0.0f) * Random.Range(0.85f, 1.25f);
    }

    // Update is called once per frame
    void Update()
    {
        timeChangeGlowScale += Time.deltaTime;

        if (EscalarGlow == false)
        {
            transform.localScale -= new Vector3(1.0f, 1.0f ,0.0f) * velocidadEscalado * Time.deltaTime;

            //limitamos la escala del efecto para que no sea muy pequeño
            if (transform.localScale.x < GlowEscalaMinima.x)
            {
                transform.localScale = GlowEscalaMinima;
            }

            if (timeChangeGlowScale > 1.0f)
            {
                EscalarGlow = true;
                timeChangeGlowScale = 0.0f;
            }
        }
        else
        {
            transform.localScale += new Vector3(velocidadEscalado, velocidadEscalado, 0.0f) * Time.deltaTime;


            //limitamos la escala del efecto para que no sea muy grande
            if (transform.localScale.x > GlowEscalaMaxima.x)
            {
                transform.localScale = GlowEscalaMaxima;
            }

            if (timeChangeGlowScale > 1.0f)
            {
                EscalarGlow = false;
                timeChangeGlowScale = 0.0f;
            }
        }
    }
}
