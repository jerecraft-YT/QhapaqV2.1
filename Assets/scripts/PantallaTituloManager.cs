using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PantallaTituloManager : MonoBehaviour
{
    public Image transicion;

    public bool loadLevel = false;
    public bool loadingLevel = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            loadLevel = true;

        }

        if (loadLevel)
        {
            Color opacidad = transicion.color;

            if (opacidad.a >= 1.1f && !loadingLevel)
            {
                loadingLevel = true;
                SceneManager.LoadScene("Tutorial");
            }

            opacidad.a += Time.deltaTime;

            transicion.color = opacidad;
        }
    }
}
