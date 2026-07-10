using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public SpriteRenderer flechaArco;
    public SpriteRenderer flechaManiqui;
    public SpriteRenderer flechaDash;

    public bool tutoArcoCompletado;
    public bool tutoDashCompletado;
    public bool tutoAtacarCompletado;

    public Transform referenciaDash;
    public Transform referenciaArco;

    public PlayerController playerController;

    public EnemyController maniqui;

    // Update is called once per frame
    void Update()
    {
        if (tutoArcoCompletado && tutoDashCompletado && tutoAtacarCompletado)
        {
            playerController.llaveObtenida = true;
        }

        if (referenciaArco == null)
        {
            tutoArcoCompletado = true;
        }
        if (referenciaDash == null)
        {
            tutoDashCompletado = true;
        }

        if (maniqui.vidaEnemigo < 0)
        {
            tutoAtacarCompletado = true;
        }

        ControlFlechas();
    }

    private void ControlFlechas()
    {
        if (!tutoArcoCompletado)
        {
            flechaArco.enabled = true;
        }
        else
        {
            flechaArco.enabled = false;
        }

        if (!tutoDashCompletado && tutoArcoCompletado)
        {
            flechaDash.enabled = true;
        }
        else
        {
            flechaDash.enabled = false;
        }

        if (!tutoAtacarCompletado && tutoDashCompletado && tutoArcoCompletado)
        {
            flechaManiqui.enabled = true;
        }
        else
        {
            flechaManiqui.enabled = false;
        }
    }
}
