using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator Animator;

    public PlayerController playerController;
    public PlayerLiveController LiveController;

    public bool playerDeath = false;


    // Update is called once per frame
    void Update()
    {
        AnimationController();
    }

    private void AnimationController()
    {
        if (playerController.enabled == false)
        {
            //si el script del jugador no esta activo hacemos que este quieto
            Animator.SetBool("estaCaminando", false);
        }
        else
        {
            //si puede moverse hacemos que eso dependa del input del jugador
            Animator.SetBool("estaCaminando", playerController.estaMoviendose);
        }

        if (LiveController.isDeath && !playerDeath)
        {
            playerDeath = true;
            Animator.SetTrigger("OnDeath");
        }
    }
}
