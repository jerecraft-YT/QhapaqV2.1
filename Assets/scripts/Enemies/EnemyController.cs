using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyState
    {
        //no hacer nada
        None,
        //quieto
        idle,
        //atacar
        Attack,
        //perseguir
        Chase,
        //patrullar
        Patrol
    }

    private PlayerController playerController;

    public EnemyState state;

    [SerializeField] private SpriteRenderer spriteEnemy;

    [SerializeField] private float enemySpeed = 2.0f;
    public float vidaEnemigo = 10;

    [SerializeField] private float distanceForAttack = 1f;
    [SerializeField] private float distanceForMove = 5.0f;

    //cosas para que el personaje patrulle
    [SerializeField] private float timeForPatrol = 2.0f;
    [SerializeField] private float distanceForPatrol = 2.0f;
    //para que se frene si esta muy cerca del punto que se le establecio para patrullar
    [SerializeField] private float distanceForStopPatrol = 0.1f;
    //punto al cual se movera para patrullar
    private Vector3 patrolPoint;
    private float timePatrol;

    private float TimeHit;
    private bool cambiarColor;

    public bool tieneLlave = false;

    //cosas para el ataque
    [SerializeField] private float cooldownAttack = 0.5f;
    private float timeCooldownAttack = 1.0f;

    public float tiempoQuieto = 0f;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        LiveController();
        SpriteController();

        if (tiempoQuieto > 0f)
        {
            tiempoQuieto -= Time.deltaTime;
            return;
        }

        EnemyStateController();
    }

    private void LiveController()
    {
        if (vidaEnemigo <= 0)
        {
            if (tieneLlave)
            {
                playerController.llaveObtenida = true;
            }

            Destroy(gameObject);
        }

        if (TimeHit > 0)
        {
            cambiarColor = true;
            spriteEnemy.color = Color.red;
        }
        else
        {
            if (cambiarColor)
            {
                spriteEnemy.color = Color.white;
                cambiarColor = false;
            }

        }

        TimeHit -= Time.deltaTime;
    }

    private void SpriteController()
    {
        if (playerController.transform.position.y > transform.position.y)
        {
            spriteEnemy.sortingOrder = 1;
        }
        else
        {
            spriteEnemy.sortingOrder = -1;
        }

        if (state == EnemyState.Chase || state == EnemyState.Attack)
        {
            if (playerController.transform.position.x > transform.position.x)
            {
                spriteEnemy.flipX = false;
            }
            else
            {
                spriteEnemy.flipX = true;
            }
        }


    }

    public void EnemigoGolpeado()
    {
        TimeHit = 0.1f;
    }

    private void EnemyStateController()
    {   
        //si no se establecio un target entonces salimos de la funcion para no tener errores
        if (playerController == null)
        {
            return;
        }

        switch (state)
        {
            case EnemyState.None:
                state = EnemyState.idle;
                break;
            case EnemyState.idle:
                //acciones que hara cuando este quieto
                EnemyIdle();
                break;
            case EnemyState.Attack:
                //acciones que hara cuando este atacando
                EnemyAttack();
                break;
            case EnemyState.Chase:
                //acciones que hara cuando te este persiguiendo
                EnemyChase();
                break;
            case EnemyState.Patrol:
                //acciones que hara cuando este patrullando
                EnemyPatrol();
                break;
            default:
                state = EnemyState.idle;
                break;
        }
    }

    private void EnemyIdle()
    {
        //control para que pueda salir de ese estado de quieto
        Vector3 target = playerController.transform.position;
        Vector3 myPos = transform.position;

        float distance = Vector3.Distance(myPos, target);

        if (distance < distanceForMove)
        {
            
            state = EnemyState.Chase;
            //para que el tiempo de patrullaje no se acumule
            timePatrol = 0;
        }

        //abajo irian otras cosas
        timePatrol += Time.deltaTime;

        if (timePatrol > timeForPatrol)
        {
            timePatrol = 0;
            state = EnemyState.Patrol;

            patrolPoint = new Vector3(myPos.x + Random.Range(-distanceForPatrol, distanceForPatrol), myPos.y + Random.Range(-distanceForPatrol, distanceForPatrol), 0.0f);
        }
    }

    private void EnemyChase()
    {
        //control para que pueda salir de ese estado de persiguiendo
        Vector3 target = playerController.transform.position;
        Vector3 myPos = transform.position;

        float distance = Vector3.Distance(myPos, target);

        if (distance > distanceForMove)
        {
            state = EnemyState.idle;
        }

        if (distance < distanceForAttack)
        {
            state = EnemyState.Attack;
        }

        //abajo irian otras cosas para que persiga
        timeCooldownAttack = 0.0f;

        Vector3 direccionMovimiento = (target - myPos).normalized;

        transform.position += Time.deltaTime * enemySpeed * direccionMovimiento;
    }

    private void EnemyAttack()
    {
        //control para que pueda salir de ese estado de atacando
        Vector3 target = playerController.transform.position;
        Vector3 myPos = transform.position;

        float distance = Vector3.Distance(myPos, target);

        if (distance > distanceForAttack)
        {
            state = EnemyState.Chase;
        }

        //abajo irian otras cosas para que ataque

        //esto es un tiempo de espera entre cada ataque
        timeCooldownAttack += Time.deltaTime;

        if (timeCooldownAttack > cooldownAttack)
        {
            playerController.QuitarVida();
            print("te atacaron");
            timeCooldownAttack = 0.0f;
        }
    }

    private void EnemyPatrol()
    {
        //control para que pueda salir de ese estado de patrullando
        Vector3 myPos = transform.position;

        float distance = Vector3.Distance(myPos, patrolPoint);

        Vector3 target = playerController.transform.position;

        float toPlayerDistance = Vector3.Distance(myPos, target);

        if (distance < distanceForStopPatrol)
        {
            state = EnemyState.idle;
        }

        if (toPlayerDistance < distanceForMove)
        {
            state = EnemyState.Chase;
        }

        //abajo irian otras cosas para que patrulle

        Vector3 direccionMovimiento = (patrolPoint - myPos).normalized;

        transform.position += Time.deltaTime * enemySpeed * direccionMovimiento;
    }
}
