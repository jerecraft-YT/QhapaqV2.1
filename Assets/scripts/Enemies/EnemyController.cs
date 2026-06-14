using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //se que por como esta programado el enemigo puede atravesar paredes pero pidieron simple :P

    public Transform targetEnemy;
    [SerializeField] private float enemySpeed = 2.0f;
    [SerializeField] private float distanceForAttack = 1f;
    [SerializeField] private float distanceForMove = 5.0f;
    [SerializeField] private float cooldownAttack = 0.5f;

    //esto se usara para poder hacer que el ataque no se haga a cada rato
    private float timeCooldownAttack;

    private void Update()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        //si no se establecio un target entonces salimos de la funcion para no tener errores
        if (targetEnemy == null)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position,targetEnemy.position);

        //comprobamos esto primero ya que si esta muy cerca del jugador podemos evitar calcular el movimiento
        if (distance < distanceForAttack)
        {
            //por organizacion hacemos que el codigo del ataque este en otra funcion
            EnemyAttack();
            //si esta muy cerca del jugador podemos abandonar antes esta funcion ya que no necesita moverse
            return;
        }

        if (distance < distanceForMove)
        {
            timeCooldownAttack = 0.0f;

            Vector3 direccionMovimiento = (targetEnemy.position - transform.position).normalized;

            transform.position += Time.deltaTime * enemySpeed * direccionMovimiento;
        }
    }

    private void EnemyAttack()
    {
        //esto es un tiempo de espera entre cada ataque
        timeCooldownAttack += Time.deltaTime;

        if (timeCooldownAttack > cooldownAttack)
        {
            //print("attack");
            timeCooldownAttack = 0.0f;
        }
    }
}
