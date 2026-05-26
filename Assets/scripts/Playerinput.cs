using UnityEngine;

public class Playerinput : MonoBehaviour
{
    //como estos valores si los necesitamos para usar en otros lados los hacemos publicos

    public bool presionoBotonAccion;

    //se usa un vector2 para agrupar el eje x,y en uno solo y asi sea mas comodo en vez de llamar a dos valores
    //diferentes que encima no estaran normalizados
    public Vector2 direccionMovimiento;
    
    //cuando hacemos privada una funcion significa que no podremos llamarla desde otro lado
    //esto es util para luego no llamar a una funcion que solo se ejecuta una vez por accidente
    private void Update()
    {
        //se pone todo en funciones porque es mas practico para arreglar algo si pasa un error
        DetectarMovimiento();
        DetectarBotones();
    }
    //si creamos funciones por cosas diferentes podemos tener un codigo mas organizado
    private void DetectarMovimiento()
    {
        //declaramos la direccion x,y aqui porque es mejor usar el valor normalizado
        //que evita que vayas mas rapido en diagonales
        float direccionX = Input.GetAxisRaw("Horizontal");
        float direccionY = Input.GetAxisRaw("Vertical");

        //establecemos el vector a los dos valores que obtenemos y luego no normalizamos
        direccionMovimiento = new Vector2(direccionX, direccionY).normalized;

        //print(direccionX);

    }

    private void DetectarBotones()
    {
        //obtenemos el estado del boton de accion, como es boleano no hay necesidad de hacer nada mas
        presionoBotonAccion = Input.GetButton("Accion");

        //print(presionoBotonAccion);
    }
}
