using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NotificadorUI : MonoBehaviour
{
    public Text textoNotificacion;      // Referencia al texto UI
    public float tiempoVisible = 2f;    // Tiempo que el texto se muestra

    private Coroutine coroutine;

    public void MostrarNotificacion(string mensaje)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        textoNotificacion.text = mensaje;
        textoNotificacion.gameObject.SetActive(true);
        coroutine = StartCoroutine(OcultarDespues());
    }

    private IEnumerator OcultarDespues()
    {
        yield return new WaitForSeconds(tiempoVisible);
        textoNotificacion.gameObject.SetActive(false);
    }
}
