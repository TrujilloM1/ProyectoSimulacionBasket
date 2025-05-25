using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlPausa : MonoBehaviour
{
    public GameObject panelPausa;   // Panel que se muestra cuando el juego está pausado
    private bool juegoPausado = false;

    void Start()
    {
        if (panelPausa != null)
            panelPausa.SetActive(false); // Ocultar panel al inicio
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
                ReanudarJuego();
            else
                PausarJuego();
        }
    }

    public void PausarJuego()
    {
        if (panelPausa != null)
            panelPausa.SetActive(true);
        Time.timeScale = 0f;
        juegoPausado = true;
    }

    public void ReanudarJuego()
    {
        if (panelPausa != null)
            panelPausa.SetActive(false);
        Time.timeScale = 1f;
        juegoPausado = false;
    }

    public void VolverMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // Carga la escena con índice 0
    }

    public void SalirJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
        
    }
}
