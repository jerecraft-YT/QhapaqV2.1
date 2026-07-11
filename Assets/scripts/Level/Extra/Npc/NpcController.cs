using TMPro;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public string[] nombresNpc = new string[1];
    public string[] dialogosNpc = new string[1];
    public int maxDialogIndex;
    public int actualDialogView = 0;

    private PlayerDialogController playerDialog;
    public float distanceToInteract;
    public bool jugadorEstaCerca;
    public bool dialogando = false;

    public GameObject cuadroDeTexto;
    public TMP_Text textoDialogo;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        playerDialog = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDialogController>();

        maxDialogIndex = dialogosNpc.Length;
    }

    void Update()
    {
        DialogControl();

        ControlOrden();
    }

    private void ControlOrden()
    {
        if (playerDialog.transform.position.y > transform.position.y)
        {
            sprite.sortingOrder = 1;
        }
        else
        {
            sprite.sortingOrder = -1;
        }
    }

    private void DialogControl()
    {
        Vector3 myPos = transform.position;
        Vector3 playerPos = playerDialog.transform.position;

        float distance = Vector3.Distance(myPos, playerPos);

        if (dialogando == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                actualDialogView++;

                //detecta el ultimo dialogo
                if (actualDialogView == maxDialogIndex)
                {
                    actualDialogView = 0;

                    playerDialog.puedeInteractuar = false;

                    jugadorEstaCerca = false;
                    dialogando = false;

                    cuadroDeTexto.SetActive(false);
                    Time.timeScale = 1.0f;

                    return;
                }

                MostrarDialogo();
            }
        }

        if (distance < distanceToInteract)
        {
            playerDialog.puedeInteractuar = true;
            jugadorEstaCerca = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                MostrarPrimerDialogo();
            }
        }

        if (distance > distanceToInteract && jugadorEstaCerca == true)
        {
            //print("no quiere dialogar :c");
            playerDialog.puedeInteractuar = false;
            jugadorEstaCerca = false;
            cuadroDeTexto.SetActive(false);
        }
    }

    private void MostrarDialogo()
    {
        textoDialogo.text = nombresNpc[actualDialogView] + ":\n" + dialogosNpc[actualDialogView];
    }

    private void MostrarPrimerDialogo()
    {
        dialogando = true;
        Time.timeScale = 0.0f;
        cuadroDeTexto.SetActive(true);

        MostrarDialogo();
    }
}
