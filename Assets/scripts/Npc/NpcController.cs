using TMPro;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public string[] nombresNpc = new string[1];
    public string[] dialogosNpc = new string[1];
    public int maxDialogIndex;
    public int actualDialogView = 0;
    private PlayerController player;
    public float distanceToInteract;
    public bool quiereDialogar;
    public GameObject cuadroDeTexto;
    public TMP_Text textoDialogo;
    public bool dialogando = false;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        maxDialogIndex = dialogosNpc.Length;
    }

    void Update()
    {
        DialogControl();

        if (player.transform.position.y > transform.position.y)
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
        Vector3 playerPos = player.transform.position;

        float distance = Vector3.Distance(myPos, playerPos);

        if (dialogando == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                actualDialogView++;

                if (actualDialogView == maxDialogIndex)
                {
                    actualDialogView = 0;
                    player.puedeDialogar = false;
                    quiereDialogar = false;
                    cuadroDeTexto.SetActive(false);
                    Time.timeScale = 1.0f;
                    dialogando = false;
                    return;
                }

                textoDialogo.text = nombresNpc[actualDialogView] + ":\n" + dialogosNpc[actualDialogView];
            }
        }
        if (distance < distanceToInteract)
        {
            player.puedeDialogar = true;
            quiereDialogar = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogando = true;
                Time.timeScale = 0.0f;
                cuadroDeTexto.SetActive(true);
                textoDialogo.text = nombresNpc[actualDialogView] + ":\n" + dialogosNpc[actualDialogView];
            }
        }
        if (distance > distanceToInteract && quiereDialogar == true)
        {
            //print("no quiere dialogar :c");
            player.puedeDialogar = false;
            quiereDialogar = false;
            cuadroDeTexto.SetActive(false);
        }
    }
}
