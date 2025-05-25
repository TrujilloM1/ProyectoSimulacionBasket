using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlBotonesMenu : MonoBehaviour
{
    public Button botonDeJugar;
    public Button botonDeAjustes;
    public Button botonDeInstrucciones;
    public GameObject panelInstrucciones;  // Panel con las instrucciones

    void Start()
    {
        if (botonDeJugar != null)
            botonDeJugar.onClick.AddListener(CargarEscenaJumble);

        if (botonDeAjustes != null)
            botonDeAjustes.onClick.AddListener(CargarEscenaAjustes);

        if (botonDeInstrucciones != null)
            botonDeInstrucciones.onClick.AddListener(MostrarInstrucciones);
    }

    public void CargarEscenaJumble()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void CargarEscenaAjustes()
    {
        SceneManager.LoadScene("AJUSTES");
    }

    public void MostrarInstrucciones()
    {
        if (panelInstrucciones != null)
            panelInstrucciones.SetActive(true);
    }

    public void OcultarInstrucciones()
    {
        if (panelInstrucciones != null)
            panelInstrucciones.SetActive(false);
    }
}
