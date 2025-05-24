using UnityEngine;

public class DetectorCanasta : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Lanzador lanzador = other.GetComponent<Lanzador>();
        if (lanzador != null && lanzador.GetVelocidad().y < 0)
        {
            lanzador.RegistrarPunto();
        }
    }
}