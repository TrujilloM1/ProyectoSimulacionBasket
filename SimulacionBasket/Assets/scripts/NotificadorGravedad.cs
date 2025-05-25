using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NotificadorGravedad : MonoBehaviour
{
    public GameObject textoGravedad;  // Objeto de texto que muestra la notificación
    public float tiempoVisible = 100f;  // Tiempo que se muestra la notificación

    private Coroutine coroutine;

    public void MostrarNotificacion()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        textoGravedad.SetActive(true);
        coroutine = StartCoroutine(OcultarDespues());
    }

    private IEnumerator OcultarDespues()
    {
        yield return new WaitForSeconds(tiempoVisible);
        textoGravedad.SetActive(false);
    }
}
