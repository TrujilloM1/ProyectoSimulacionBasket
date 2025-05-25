using UnityEngine;
using UnityEngine.UI;  // Asegúrate de usar esto para UI Text

public class UIManager : MonoBehaviour
{
    public Text puntosText;    // Texto para mostrar cantidad de cestas
    public Text scoreText;     // Texto para mostrar el puntaje total
    public Lanzador lanzador;  // Referencia al script Lanzador

    void Update()
    {
        if (lanzador != null)
        {
            puntosText.text = "Cestas: " + lanzador.puntos.ToString();
            scoreText.text = "Score: " + lanzador.score.ToString();
        }
    }
}
