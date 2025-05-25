using UnityEngine;

public class Lanzador : MonoBehaviour
{

    [Header("Colision Bordes de Aro")]
    public Transform bordeIzquierdo;
    public Transform bordeDerecho;
    public float anchoBorde = 0.1f;
    public float altoBorde = 0.3f;
    public float reboteBorde = 0.6f;

    [Header("Zona de canasta")]
    public bool haAnotado = false;
    public int puntos = 0;

    [Header("Sistema de dificultad dinámica")]
    public float gravedadBase = -9.8f;           // Gravedad inicial
    public float gravedadMaxima = -20f;          // Gravedad límite (máxima)
    public float incrementoGravedadPorPunto = -5f;  // Cuánto se incrementa la gravedad por cada punto
    public int incrementoCadaXPuntos = 3;  // Por ejemplo, cada 3 puntos

    [Header("Límites del área de juego")]
    public float limiteIzquierdo = -8f;
    public float limiteDerecho = 8f;
    public float limiteSuperior = 5f;

    public Vector2 velocidad;
    public float gravedad = -9.8f;
    public float velocidadMinima = 0.05f;
    public float groundY = -3f; // altura del suelo

    [Header("Colisión con tablero")]
    public Transform tablero;           // Asigna el objeto "Tablero" desde el Inspector
    public float anchoTablero = 0.5f;   // Mitad del ancho del tablero
    public float altoTablero = 1.5f;    // Mitad del alto del tablero
    public float radioPelota = 0.25f;   // Tamaño de la pelota
    public float reboteTablero = 0.6f;  // Rebote horizontal

    [Header("Pared móvil")]
    public Transform paredMovil;
    public float anchoPared = 0.5f;  // Mitad del ancho de la pared
    public float altoPared = 1.5f;   // Mitad del alto de la pared
    public float rebotePared = 0.6f; // Coeficiente de rebote
    public GameObject paredMovilGO; // referencia al GameObject completo de la pared móvil


    private Vector2 posicion;
    private bool enMovimiento = true;

    void Start()
    {
        posicion = transform.position;
        velocidad = Vector2.zero;
        enMovimiento = false;
        transform.position = posicion;

        gravedad = gravedadBase;
    }

    void Update()
    {
        if (enMovimiento)
        {
            // Aplicar gravedad
            velocidad.y += gravedad * Time.deltaTime;

            // Mover la pelota manualmente
            posicion += velocidad * Time.deltaTime;

            // Verificar colisión con el suelo (Y mínima)
            if (posicion.y <= groundY)
            {
                posicion.y = groundY;
                velocidad.y *= -0.6f;       // rebote vertical
                velocidad.x *= 0.9f;        // fricción
            }

            // Colisión con el tablero (tipo cuadrado)
            if (tablero != null)
            {
                Vector2 posPelota = posicion;
                Vector2 posTablero = tablero.position;

                float dx = Mathf.Abs(posPelota.x - posTablero.x);
                float dy = Mathf.Abs(posPelota.y - posTablero.y);

                bool colisionX = dx < (anchoTablero + radioPelota);
                bool colisionY = dy < (altoTablero + radioPelota);

                if (colisionX && colisionY)
                {
                    Debug.Log("Colisión con el tablero");

                    float overlapX = (anchoTablero + radioPelota) - dx;
                    float overlapY = (altoTablero + radioPelota) - dy;

                    if (overlapX > overlapY)
                    {
                        // Rebote vertical
                        if (posPelota.y > posTablero.y)
                        {
                            posicion.y = posTablero.y + altoTablero + radioPelota;
                        }
                        else
                        {
                            posicion.y = posTablero.y - altoTablero - radioPelota;
                        }

                        velocidad.y *= -reboteTablero;
                    }
                    else
                    {
                        // Rebote horizontal
                        if (posPelota.x > posTablero.x)
                        {
                            posicion.x = posTablero.x + anchoTablero + radioPelota;
                        }
                        else
                        {
                            posicion.x = posTablero.x - anchoTablero - radioPelota;
                        }

                        velocidad.x *= -reboteTablero;
                    }
                }
            }

            // Colisión con pared móvil por todas las caras
            if (paredMovil != null)
            {
                Vector2 posPelota = posicion;
                Vector2 posPared = paredMovil.position;

                float dx = posPelota.x - posPared.x;
                float dy = posPelota.y - posPared.y;

                float overlapX = (anchoPared + radioPelota) - Mathf.Abs(dx);
                float overlapY = (altoPared + radioPelota) - Mathf.Abs(dy);

                if (overlapX > 0 && overlapY > 0)
                {
                    // Hay colisión

                    if (overlapX < overlapY)
                    {
                        // Rebote horizontal

                        if (dx > 0)
                            posicion.x = posPared.x + anchoPared + radioPelota;  // A la derecha
                        else
                            posicion.x = posPared.x - anchoPared - radioPelota;  // A la izquierda

                        velocidad.x *= -rebotePared;
                    }
                    else
                    {
                        // Rebote vertical

                        if (dy > 0)
                            posicion.y = posPared.y + altoPared + radioPelota;  // Arriba
                        else
                            posicion.y = posPared.y - altoPared - radioPelota;  // Abajo

                        velocidad.y *= -rebotePared;
                    }

                    Debug.Log("Colisión con pared móvil en alguna cara");
                }
            }

            // Colisión con borde izquierdo
            if (bordeIzquierdo != null)
            {
                Vector2 posBorde = bordeIzquierdo.position;
                float dx = Mathf.Abs(posicion.x - posBorde.x);
                float dy = Mathf.Abs(posicion.y - posBorde.y);

                bool colisionX = dx < (anchoBorde + radioPelota);
                bool colisionY = dy < (altoBorde + radioPelota);

                if (colisionX && colisionY)
                {
                    Debug.Log("Colisión con borde izquierdo");

                    // Ajustar posición fuera del borde
                    posicion.x = posBorde.x + (posicion.x < posBorde.x ? -(anchoBorde + radioPelota) : (anchoBorde + radioPelota));

                    // Rebote horizontal con límite de velocidad mínima
                    velocidad.x = -velocidad.x * reboteBorde;
                    if (Mathf.Abs(velocidad.x) < 0.5f)
                        velocidad.x = Mathf.Sign(velocidad.x) * 0.5f;

                    // Suavizar velocidad vertical
                    velocidad.y *= 0.8f;
                }
            }

            // Colisión con borde derecho
            if (bordeDerecho != null)
            {
                Vector2 posBorde = bordeDerecho.position;
                float dx = Mathf.Abs(posicion.x - posBorde.x);
                float dy = Mathf.Abs(posicion.y - posBorde.y);

                bool colisionX = dx < (anchoBorde + radioPelota);
                bool colisionY = dy < (altoBorde + radioPelota);

                if (colisionX && colisionY)
                {
                    Debug.Log("Colisión con borde derecho");

                    // Ajustar posición fuera del borde
                    posicion.x = posBorde.x + (posicion.x < posBorde.x ? -(anchoBorde + radioPelota) : (anchoBorde + radioPelota));

                    // Rebote horizontal con límite de velocidad mínima
                    velocidad.x = -velocidad.x * reboteBorde;
                    if (Mathf.Abs(velocidad.x) < 0.5f)
                        velocidad.x = Mathf.Sign(velocidad.x) * 0.5f;

                    // Suavizar velocidad vertical
                    velocidad.y *= 0.8f;
                }
            }

            // Detener si la velocidad es muy baja
            if (velocidad.magnitude < velocidadMinima)
            {
                velocidad = Vector2.zero;
                enMovimiento = false;
            }

            transform.position = posicion;
        }

        // Limitar horizontalmente
        if (posicion.x <= limiteIzquierdo)
        {
            posicion.x = limiteIzquierdo;
            velocidad.x *= -0.6f; // rebote
        }

        if (posicion.x >= limiteDerecho)
        {
            posicion.x = limiteDerecho;
            velocidad.x *= -0.6f;
        }

        // Limitar verticalmente
        if (posicion.y >= limiteSuperior)
        {
            posicion.y = limiteSuperior;
            velocidad.y *= -0.6f;
        }
    }




    public Vector2 GetVelocidad()
    {
        return velocidad;
    }

    public void ReiniciarDesdeInicio()
    {
        velocidad = Vector2.zero;
        posicion = transform.position;
        transform.position = posicion;
        enMovimiento = false;
    }

    public void RelanzarDesdePosicion()
    {
        velocidad = new Vector2(6f, 8f); // o alguna velocidad predeterminada
        enMovimiento = true;
    }

    
    public void RegistrarPunto()
    {
        if (!haAnotado)
        {
            puntos++;
            haAnotado = true;
            Debug.Log($"¡Canasta! Puntos: {puntos}");

            // Aumenta la gravedad solo cuando puntos es múltiplo de incrementoCadaXPuntos
            if (puntos % incrementoCadaXPuntos == 0)
            {
                gravedad = Mathf.Max(gravedadMaxima, gravedad + incrementoGravedadPorPunto);
                Debug.Log($"Gravedad ajustada: {gravedad}");
            }
            // Activa la pared móvil cuando se llegue a 9 puntos (o más)
            if (puntos >= 9 && paredMovilGO != null && !paredMovilGO.activeSelf)
            {
                paredMovilGO.SetActive(true);
                Debug.Log("Pared móvil activada por dificultad.");
            }
        }
    }
    public void ActualizarVelocidad(Vector2 nuevaVelocidad)
    {
        velocidad = nuevaVelocidad;
        enMovimiento = true;
        haAnotado = false;
    }
    public int GetPuntos()
    {
        return puntos;
    }


}
