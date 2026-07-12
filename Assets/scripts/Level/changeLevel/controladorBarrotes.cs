using UnityEngine;

public class ControladorBarrotes : MonoBehaviour
{
    private PlayerController playerController;
    public GameObject oscuridad;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.llaveObtenida)
        {
            oscuridad.SetActive(true);
            Destroy(gameObject);
        }
    }
}
