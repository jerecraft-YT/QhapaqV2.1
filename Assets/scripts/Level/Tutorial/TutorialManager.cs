using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public SpriteRenderer flechaArco;
    public SpriteRenderer flechaManiqui;
    public SpriteRenderer flechaDash;

    public bool tutoArcoCompletado;
    public bool tutoDashCompletado;
    public bool tutoAtacarCompletado;
    public bool tutoMoverseCompletado;

    public Transform referenciaDash;
    public Transform referenciaArco;

    public PlayerController playerController;
    public PlayerInput playerInput;

    public EnemyController maniqui;

    public GameObject tutoMoverse;

    // Update is called once per frame
    void Update()
    {
        ControlProgresoTuto();

        ControlFlechas();

        ControlTutoMoverse();
    }

    private void ControlProgresoTuto()
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
    }

    private void ControlTutoMoverse()
    {
        if (tutoMoverseCompletado) return;

        if (playerInput.direccionMovimiento != Vector2.zero)
        {
            tutoMoverseCompletado = true;
            Destroy(tutoMoverse);
        }
    }

    private void ControlFlechas()
    {
        if (!tutoArcoCompletado && tutoMoverseCompletado)
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
