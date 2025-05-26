using UnityEngine;
using UnityEngine.UI;

public class ControlLanzamiento : MonoBehaviour
{
    public Transform pelota;             // Referencia a la pelota
    public LineRenderer linea;           // Línea guía del lanzamiento
    public Slider barraFuerza;           // Slider UI para la fuerza
    public float fuerzaMaxima = 15f;     // Fuerza máxima del lanzamiento
    public float velocidadCarga = 1f;    // Velocidad de carga de la barra

    private Vector2 direccion;           // Dirección del lanzamiento
    private bool cargandoFuerza = false;
    private float fuerzaActual = 0f;

    void Start()
    {
        if (linea != null)
            linea.positionCount = 2;

        if (barraFuerza != null)
            barraFuerza.value = 0;
    }

    void Update()
    {
        // Obtener dirección del mouse respecto a la pelota
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (Vector2)(mousePos - pelota.position);
        direccion = dir.normalized;

        // Actualizar línea guía
        if (linea != null)
        {
            linea.SetPosition(0, pelota.position);
            linea.SetPosition(1, pelota.position + (Vector3)(direccion * fuerzaActual));
        }

        // Control de la barra de fuerza con barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cargandoFuerza = true;
            fuerzaActual = 0f;
        }
        if (Input.GetKey(KeyCode.Space) && cargandoFuerza)
        {
            fuerzaActual += velocidadCarga * Time.deltaTime;
            fuerzaActual = Mathf.Clamp(fuerzaActual, 0, fuerzaMaxima);

            if (barraFuerza != null)
                barraFuerza.value = fuerzaActual / fuerzaMaxima;
        }
        if (Input.GetKeyUp(KeyCode.Space) && cargandoFuerza)
        {
            cargandoFuerza = false;

            // Lanzar la pelota con la dirección y fuerza calculadas
            LanzarPelota(direccion * fuerzaActual);

            fuerzaActual = 0f;

            if (barraFuerza != null)
                barraFuerza.value = 0;
        }
    }

    void LanzarPelota(Vector2 velocidad)
    {
        // Aquí debes llamar el método de tu script Lanzador para actualizar velocidad
        var lanzador = pelota.GetComponent<Lanzador>();
        if (lanzador != null)
        {
            lanzador.ActualizarVelocidad(velocidad);
        }
    }
}
