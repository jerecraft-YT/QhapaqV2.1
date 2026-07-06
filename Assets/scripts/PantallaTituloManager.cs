using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaTituloManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene("Mazmorra1");
        }
    }
}
