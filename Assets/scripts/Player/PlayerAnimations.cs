using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator Animator;

    public PlayerController Controller;


    // Update is called once per frame
    void Update()
    {
        Animator.SetBool("estaCaminando", Controller.estaMoviendose);

    }
}
