using UnityEngine;

public class ControlParedMovil : MonoBehaviour
{
    public Vector3 posicionInicial = new Vector3(0, 10f, 0);  // Fuera de cámara (quieta)
    public Vector3 posicionFinal = new Vector3(0, 1.5f, 0);   // Posición visible
    public float velocidadDescenso = 2f;                      // Velocidad para bajar
    public float amplitudOscilacion = 0.5f;                   // Amplitud de la oscilación vertical
    public float velocidadOscilacion = 2f;                    // Velocidad de oscilación

    private bool moviendose = false;
    private bool oscilando = false;
    private Vector3 posBaseOscilacion;

    void Start()
    {
        transform.position = posicionInicial;
        moviendose = false;
        oscilando = false;
        
    }

    void Update()
    {
        if (moviendose)
        {
            // Baja suavemente hacia la posición visible
            transform.position = Vector3.MoveTowards(transform.position, posicionFinal, velocidadDescenso * Time.deltaTime);
           

            if (transform.position == posicionFinal)
            {
                moviendose = false;
                oscilando = true;
                posBaseOscilacion = transform.position;
              
            }
        }
        else if (oscilando)
        {
            float nuevoY = posBaseOscilacion.y + Mathf.Sin(Time.time * velocidadOscilacion) * amplitudOscilacion;
            transform.position = new Vector3(transform.position.x, nuevoY, transform.position.z);
            
        }
    }

    // Método para activar la bajada
    public void ActivarMovimiento()
    {
        moviendose = true;
        oscilando = false;
    }
}
