using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLiveController : MonoBehaviour
{
    public PlayerController playerController;

    public bool isDeath;

    public float timeDeath;
    public float opacidadDeath;
    public float velocidadOpacidadDeath = 2.0f;

    public float cooldownRecibirDaño = 1.0f;
    private float timeCooldownRecibirDaño;

    public int vidaJugador = 3;

    private bool subirBajarOpacidad;
    public float velocidadParpadeo;

    private SpriteRenderer spritePlayer;

    private void Start()
    {
        spritePlayer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDeath)
        {
            DeathController();
            return;
        }

        LiveController();

    }

    //sube y baja la opacidad del jugador mientras no puede recibir daño
    private void CooldownEffect()
    {
        Color colorBase = spritePlayer.color;

        if (subirBajarOpacidad)
        {
            colorBase.a -= Time.deltaTime * velocidadParpadeo;

            if (colorBase.a <= 0.0f)
            {
                subirBajarOpacidad = false;
            }
        }
        else
        {
            colorBase.a += Time.deltaTime * velocidadParpadeo;

            if (colorBase.a >= 1.0f)
            {
                subirBajarOpacidad = true;
            }
        }

        spritePlayer.color = colorBase;

    }

    private void LiveController()
    {
        timeCooldownRecibirDaño -= Time.deltaTime;

        if (timeCooldownRecibirDaño > 0.0f)
        {
            CooldownEffect();
        }
        else
        {
            //reestablece el color del jugador a como se ve normalmente
            spritePlayer.color = Color.white;
        }

        if (vidaJugador <= 0)
        {
            playerController.spritePlayer.sortingLayerName = "Foreground";
            playerController.spritePlayer.sortingOrder = 100;
            isDeath = true;

            playerController.UpdateHUD();

            playerController.enabled = false;
        }
    }
    public void QuitarVida()
    {
        if (timeCooldownRecibirDaño < 0)
        {
            print("te atacaron");

            timeCooldownRecibirDaño = cooldownRecibirDaño;
            vidaJugador -= 1;
            playerController.cameraPlayer.ScreenShake(0.25f, 0.2f);
        }
    }

    private void DeathController()
    {
        timeDeath += Time.deltaTime;

        if (timeDeath > 2)
        {
            opacidadDeath += Time.deltaTime * velocidadOpacidadDeath;

            Color colorOpacidad = playerController.hudController.transicionMorir.color;

            colorOpacidad.a = opacidadDeath;

            playerController.hudController.transicionMorir.color = colorOpacidad;
        }

        if (timeDeath > 4)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void VolverInvulnerable(float duracion)
    {
        timeCooldownRecibirDaño = duracion;
    }

    public void GanarVida(int cantidad)
    {
        vidaJugador += cantidad;

        if (vidaJugador > 3)
        {
            vidaJugador = 3;
        }
    }
}
