using UnityEngine;

public class parpadeoSprite : MonoBehaviour
{
    public NpcController npcController;
    public EnemyController enemyController;

    private Animator animator;

    public bool parpadear;

    private bool puedeReActivarse = true;

    public float timeToReset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (npcController.dialogando && puedeReActivarse)
        {
            puedeReActivarse = false;
            parpadear = true;
            timeToReset = 3.0f;
        }

        if (parpadear)
        {
            timeToReset -= Time.deltaTime;

            animator.SetBool("estaParpadeando", true);

            if (timeToReset < 0.0f)
            {
                puedeReActivarse = true;

                parpadear = false;

                animator.SetBool("estaParpadeando", false);
            }
        }


        //para el maniqui y que no se reactive su parpadeo
        if (enemyController == null) return;

        if (enemyController.tiempoQuieto > 0)
        {
            parpadear = false;
            animator.SetBool("estaParpadeando", false);
        }
    }
}
