using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlBotonesMenu : MonoBehaviour
{
    public Button botonDeJugar;
    public Button botonDeAjustes;

    void Start()
    {
        if (botonDeJugar != null)
            botonDeJugar.onClick.AddListener(CargarEscenaJumble);

        if (botonDeAjustes != null)
            botonDeAjustes.onClick.AddListener(CargarEscenaAjustes);
    }

    // Las funciones deben ser públicas para aparecer en el inspector
    public void CargarEscenaJumble()
    {
        SceneManager.LoadScene("jumble"); // Cambia "jumble" si tu escena tiene otro nombre
    }

    public void CargarEscenaAjustes()
    {
        SceneManager.LoadScene("AJUSTES"); // Cambia "AJUSTES" si tu escena tiene otro nombre
    }
}