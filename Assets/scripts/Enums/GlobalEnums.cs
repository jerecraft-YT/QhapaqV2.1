using UnityEngine;

public class GlobalEnums : MonoBehaviour
{
    public enum lootCaja
    {
        None,
        RecargaDash,
        Vida,
        Arco,
    }

    public enum lootEnemigo
    {
        None,
        RecargaDash, 
        Vida,
        Arco,
        llave
    }

    public lootEnemigo tipoLoot;

    private void Start()
    {
        switch (tipoLoot)
        {
            case lootEnemigo.None:
                break;
            case lootEnemigo.RecargaDash:
                print("obtuviste una recarga de dash");
                break;
            case lootEnemigo.Vida:
                print("obtuviste una vida");
                break;
            case lootEnemigo.Arco:
                print("obtuviste un arco");
                break;
            case lootEnemigo.llave:
                print("obtuviste una llave");
                break;
            default:
                break;
        }



    }
}
