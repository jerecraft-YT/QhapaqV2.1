using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CambiarNivel : MonoBehaviour
{
    private PlayerController playerController;
    private Image transicionCambiarEscena;
    private float opacidad;
    public string nivelACargar;

    private bool cambiarEscena;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && playerController.llaveObtenida)
        {
            print("Superaste el nivel");

            transicionCambiarEscena = playerController.hudController.transicionMorir;
            playerController.enabled = false;
            cambiarEscena = true;
        }
    }

    private void Update()
    {
        if (cambiarEscena)
        {
            opacidad += Time.deltaTime;

            Color colorOpacidad = transicionCambiarEscena.color;

            colorOpacidad.a = opacidad;

            transicionCambiarEscena.color = colorOpacidad;

            if (colorOpacidad.a > 1.2f)
            {
                SceneManager.LoadScene(nivelACargar);
            }
        }
}
}
