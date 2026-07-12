using UnityEngine;

public class PlayerDialogController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer burbujaDialogo;

    public bool puedeInteractuar = false;

    void Update()
    {
        ControlDialogo();
    }

    private void ControlDialogo()
    {
        if (puedeInteractuar == true)
        {
            burbujaDialogo.enabled = true;
        }
        if (puedeInteractuar == false)
        {
            burbujaDialogo.enabled = false;
        }
    }
}
