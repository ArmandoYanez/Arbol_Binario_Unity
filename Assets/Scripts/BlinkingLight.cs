using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    // Referencia a la luz
    public Light blinkingLight;

    // Referencia al AudioSource
    public AudioSource blinkSound;

    // Tiempo de encendido y apagado
    public float blinkInterval = 0.5f;

    // Control interno del estado de la luz
    private bool isLightOn = true;

    void Start()
    {
        if (blinkingLight == null || blinkSound == null)
        {
            Debug.LogError("Por favor asigna la luz y el AudioSource en el inspector.");
            enabled = false;
            return;
        }

        // Inicia el parpadeo
        InvokeRepeating(nameof(Blink), 0f, blinkInterval);
    }

    void Blink()
    {
        // Alterna el estado de la luz
        isLightOn = !isLightOn;
        blinkingLight.enabled = isLightOn;

        // Reproduce el sonido cuando la luz parpadea
        if (blinkSound != null)
        {
            blinkSound.Stop();
        }
    }
}