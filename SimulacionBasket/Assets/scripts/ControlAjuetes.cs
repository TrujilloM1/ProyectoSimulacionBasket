using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ControlAjustes : MonoBehaviour
{
    public GameObject panelAjustes;
    public Slider sliderVolumen;
    public TMP_Dropdown dropdownCalidad;
    public Toggle togglePantallaCompleta;

    public AudioSource audioJuego; // Referencia al AudioSource del juego

    void Start()
    {
        if (panelAjustes == null) Debug.LogError("panelAjustes no está asignado en el Inspector");
        if (sliderVolumen == null) Debug.LogError("sliderVolumen no está asignado en el Inspector");
        if (dropdownCalidad == null) Debug.LogError("dropdownCalidad no está asignado en el Inspector");
        if (togglePantallaCompleta == null) Debug.LogError("togglePantallaCompleta no está asignado en el Inspector");

        if (audioJuego == null)
        {
            GameObject audioObj = GameObject.Find("AudioMusica");
            if (audioObj != null)
            {
                audioJuego = audioObj.GetComponent<AudioSource>();
            }
            else
            {
                Debug.LogWarning("No se encontró el objeto 'AudioMusica' para asignar AudioSource");
            }
        }

        if (sliderVolumen != null)
            sliderVolumen.value = PlayerPrefs.GetFloat("volumen", 1f);
        if (dropdownCalidad != null)
            dropdownCalidad.value = PlayerPrefs.GetInt("calidad", 2);
        if (togglePantallaCompleta != null)
            togglePantallaCompleta.isOn = PlayerPrefs.GetInt("pantallaCompleta", 1) == 1;

        if (sliderVolumen != null)
            sliderVolumen.onValueChanged.AddListener(ActualizarVolumen);

        AplicarAjustes();
    }

    void ActualizarVolumen(float valor)
    {
        AudioListener.volume = valor;
        if (audioJuego != null)
        {
            audioJuego.volume = valor;
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

        // Cambia aquí el nombre de la escena a "menu inicial" (todo en minúsculas y con espacio)
        SceneManager.LoadScene("menu inicial");
    }

    public void VolverMenuInicial()
    {
        Debug.Log("Intentando cargar la escena 'menu inicial'...");
        if (SceneManager.GetSceneByName("menu inicial").IsValid())
        {
            SceneManager.LoadScene("menu inicial");
        }
        else
        {
            Debug.LogError("La escena 'menu inicial' NO está agregada en Build Settings o el nombre es incorrecto.");
        }
    }
}