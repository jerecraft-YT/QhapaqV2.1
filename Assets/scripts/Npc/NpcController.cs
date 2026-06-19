using TMPro;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public string nombreNpc = "";
    public string dialogoNpc = "";
    public PlayerController player;
    public float distanceToInteract;
    public bool quiereDialogar;
    public GameObject cuadroDeTexto;
    public TMP_Text textoDialogo;

    void Update()
    {
        Vector3 myPos = transform.position;
        Vector3 playerPos = player.transform.position;

        float distance = Vector3.Distance(myPos, playerPos);

        if (distance < distanceToInteract)
        {
            player.puedeDialogar = true;
            quiereDialogar = true;

            if (Input.GetKey(KeyCode.E))
            {
                cuadroDeTexto.SetActive(true);
                textoDialogo.text = nombreNpc + ":\n" + dialogoNpc;
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
