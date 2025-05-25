using UnityEngine;

public class AudioPersistente : MonoBehaviour
{
    private static AudioPersistente instancia;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Evita duplicados si ya existe uno
        }
    }
}