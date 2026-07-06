using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator Animator;

    public PlayerController Controller;

    public bool playerDeath = false;


    // Update is called once per frame
    void Update()
    {
        if (Controller.enabled == false)
        {
            Animator.SetBool("estaCaminando", false);
        }
        else
        {
            Animator.SetBool("estaCaminando", Controller.estaMoviendose);
        }
        if (Controller.isDeath && !playerDeath)
        {
            playerDeath = true;
            Animator.SetTrigger("OnDeath");
        }
    }
}
