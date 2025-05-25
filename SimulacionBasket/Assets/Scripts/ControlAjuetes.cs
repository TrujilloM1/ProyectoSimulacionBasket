using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlAjustes : MonoBehaviour
{
    public GameObject panelAjustes;
    public Slider sliderVolumen;
    public TMP_Dropdown dropdownCalidad;
    public Toggle togglePantallaCompleta;

    public AudioSource audioJuego; // Referencia al AudioSource del juego

    void Start()
    {
        // Validaciones para detectar campos sin asignar
        if (panelAjustes == null) Debug.LogError("panelAjustes no está asignado en el Inspector");
        if (sliderVolumen == null) Debug.LogError("sliderVolumen no está asignado en el Inspector");
        if (dropdownCalidad == null) Debug.LogError("dropdownCalidad no está asignado en el Inspector");
        if (togglePantallaCompleta == null) Debug.LogError("togglePantallaCompleta no está asignado en el Inspector");

        // Si no has asignado el AudioSource en el Inspector, lo busca automáticamente
        if (audioJuego == null)
        {
            GameObject audioObj = GameObject.Find("AudioMusica"); // Cambia el nombre si tu objeto es otro
            if (audioObj != null)
            {
                audioJuego = audioObj.GetComponent<AudioSource>();
            }
            else
            {
                Debug.LogWarning("No se encontró el objeto 'AudioMusica' para asignar AudioSource");
            }
        }

        // Cargar preferencias guardadas
        if (sliderVolumen != null)
            sliderVolumen.value = PlayerPrefs.GetFloat("volumen", 1f);
        if (dropdownCalidad != null)
            dropdownCalidad.value = PlayerPrefs.GetInt("calidad", 2);
        if (togglePantallaCompleta != null)
            togglePantallaCompleta.isOn = PlayerPrefs.GetInt("pantallaCompleta", 1) == 1;

        // Suscribirse al evento para actualizar volumen al mover el slider
        if (sliderVolumen != null)
            sliderVolumen.onValueChanged.AddListener(ActualizarVolumen);

        AplicarAjustes();
    }

    void ActualizarVolumen(float valor)
    {
        AudioListener.volume = valor;  // Volumen global
        if (audioJuego != null)
        {
            audioJuego.volume = valor;  // Volumen específico del AudioSource
        }
    }

    public void AplicarAjustes()
    {
        if (sliderVolumen != null)
            ActualizarVolumen(sliderVolumen.value);
        if (dropdownCalidad != null)
            QualitySettings.SetQualityLevel(dropdownCalidad.value);
        if (togglePantallaCompleta != null)
            Screen.fullScreen = togglePantallaCompleta.isOn;
    }

    public void GuardarYSalir()
    {
        if (sliderVolumen != null)
            PlayerPrefs.SetFloat("volumen", sliderVolumen.value);
        if (dropdownCalidad != null)
            PlayerPrefs.SetInt("calidad", dropdownCalidad.value);
        if (togglePantallaCompleta != null)
            PlayerPrefs.SetInt("pantallaCompleta", togglePantallaCompleta.isOn ? 1 : 0);

        PlayerPrefs.Save();

        AplicarAjustes();
        if (panelAjustes != null)
            panelAjustes.SetActive(false);
    }

    public void AbrirPanel()
    {
        if (panelAjustes != null)
            panelAjustes.SetActive(true);
    }
}
