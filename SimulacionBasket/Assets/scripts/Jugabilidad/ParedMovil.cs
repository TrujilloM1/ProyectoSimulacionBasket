using UnityEngine;

public class ParedMovil : MonoBehaviour
{
    public float amplitud = 3f;   // Distancia máxima arriba y abajo
    public float velocidad = 2f;  // Velocidad de movimiento
    private Vector3 posInicial;

    void Start()
    {
        posInicial = transform.position;
    }

    void Update()
    {
        float nuevoY = posInicial.y + Mathf.Sin(Time.time * velocidad) * amplitud;
        transform.position = new Vector3(transform.position.x, nuevoY, transform.position.z);
    }
}
