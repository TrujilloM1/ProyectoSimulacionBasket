using UnityEngine;

public class ColisionConBorde : MonoBehaviour
{
    public Lanzador lanzador;
    public float reboteFactor = 0.6f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Borde"))
        {
            Vector2 nuevaVelocidad = lanzador.GetVelocidad();
            nuevaVelocidad.x *= -reboteFactor;

            lanzador.ActualizarVelocidad(nuevaVelocidad);
        }
    }
}
