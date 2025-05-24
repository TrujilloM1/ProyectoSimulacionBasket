using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ArrastreLanzamiento : MonoBehaviour
{
    public float powerFactor = 5f;         // Multiplica la distancia arrastrada para obtener potencia
    public Lanzador lanzador;              // Referencia al script de movimiento
    public LineRenderer linea;             // Línea que muestra dirección

    private bool arrastrando = false;
    private Vector2 puntoInicio;

    void Start()
    {
        if (linea == null)
            linea = GetComponent<LineRenderer>();

        linea.positionCount = 2;
        linea.enabled = false;
    }

    void OnMouseDown()
    {
        if (lanzador != null && lanzador.GetVelocidad() == Vector2.zero)
        {
            puntoInicio = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            arrastrando = true;
            linea.enabled = true;
        }
    }

    void OnMouseDrag()
    {
        if (!arrastrando) return;

        Vector2 puntoActual = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direccion = puntoInicio - puntoActual;

        // Mostrar línea guía
        linea.SetPosition(0, transform.position);
        linea.SetPosition(1, (Vector2)transform.position + direccion);
    }

    void OnMouseUp()
    {
        if (!arrastrando) return;

        Vector2 puntoFinal = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direccion = puntoInicio - puntoFinal;

        Vector2 velocidad = direccion * powerFactor;

        lanzador.ActualizarVelocidad(velocidad);

        arrastrando = false;
        linea.enabled = false;
    }
}
