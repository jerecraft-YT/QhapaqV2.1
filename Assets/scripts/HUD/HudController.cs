using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    [SerializeField] private Image arco;
    [SerializeField] private Image espada;
    [SerializeField] private Image corazon1;
    [SerializeField] private Image corazon2;
    [SerializeField] private Image corazon3;
    [SerializeField] private Image medidorDash;
    [SerializeField] private Image llave;
    [SerializeField] private Sprite[] medidorDashSprite;
    [SerializeField] private TMP_Text[] numeroFlechasText;
    [SerializeField] private Sprite corazonVivoSprite;
    [SerializeField] private Sprite corazonNoVivoSprite;
    [SerializeField] private Sprite espadaSeleccionadoSprite;
    [SerializeField] private Sprite espadaNoSeleccionadoSprite;
    [SerializeField] private Sprite arcoSeleccionadoSprite;
    [SerializeField] private Sprite arcoNoSeleccionadoSprite;

    public int dashDisponibles;
    public int vida;
    public bool espadaOArcoSeleccionado;
    public bool llaveObtenida;
    public int numeroFlechas;

    private int oldDashDisponibles = -1;
    private int oldVida = -1;
    private bool oldEspadaOArcoSeleccionado;

    // Update is called once per frame
    void Update()
    {
        if (dashDisponibles != oldDashDisponibles)
        {
            medidorDash.sprite = medidorDashSprite[dashDisponibles];
            oldDashDisponibles = dashDisponibles;
        }

        if (vida != oldVida)
        {
            switch (vida)
            {
                case 0:
                    corazon1.sprite = corazonNoVivoSprite;
                    corazon2.sprite = corazonNoVivoSprite;
                    corazon3.sprite = corazonNoVivoSprite;
                    break;
                case 1:
                    corazon1.sprite = corazonVivoSprite;
                    corazon2.sprite = corazonNoVivoSprite;
                    corazon3.sprite = corazonNoVivoSprite;
                    break;
                case 2:
                    corazon1.sprite = corazonVivoSprite;
                    corazon2.sprite = corazonVivoSprite;
                    corazon3.sprite = corazonNoVivoSprite;
                    break;
                case 3:
                    corazon1.sprite = corazonVivoSprite;
                    corazon2.sprite = corazonVivoSprite;
                    corazon3.sprite = corazonVivoSprite;
                    break;
                default:
                    break;
            }

            oldVida = vida;
        }

        if (espadaOArcoSeleccionado != oldEspadaOArcoSeleccionado)
        {
            if (espadaOArcoSeleccionado)
            {
                espada.sprite = espadaSeleccionadoSprite;
                arco.sprite = arcoNoSeleccionadoSprite;
            }
            else
            {
                espada.sprite = espadaNoSeleccionadoSprite;
                arco.sprite = arcoSeleccionadoSprite;
            }
            oldEspadaOArcoSeleccionado = espadaOArcoSeleccionado;
        }

        if (llaveObtenida)
        {
            llave.gameObject.SetActive(true);
        }
        else
        {
            llave.gameObject.SetActive(false);
        }

        foreach (var numeroFlecha in numeroFlechasText)
        {
            numeroFlecha.text = numeroFlechas.ToString();
        }
    }
}
