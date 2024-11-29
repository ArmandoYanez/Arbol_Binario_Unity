using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class startFame : MonoBehaviour
{
    
    public Volume postProcessingVolume; // Volumen de Post-Processing
    private LensDistortion lensDistortion; // Referencia al efecto Lens Distortion
    
// Referencia al componente Animator
    public Animator smoke_animator;
    void Start()
    {

    }

    void Update()
    {
        // Verifica si se presiona la tecla "A"
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Activa el trigger o parámetro en el Animator
            StartCoroutine(PlayAnimation(smoke_animator, "smoke"));
        }
    }
    
    IEnumerator PlayAnimation(Animator current_Animator, string animationName)
    {
        // Activa la animación
        current_Animator.Play(animationName);

        // Obtiene la información del estado actual del Animator
        yield return null; // Espera un frame para asegurarse de que la animación haya comenzado
        AnimatorStateInfo stateInfo = smoke_animator.GetCurrentAnimatorStateInfo(0);

        // Espera el tiempo que dura la animación
        yield return new WaitForSeconds(stateInfo.length);

        // Código que se ejecuta después de que termine la animación
        Debug.Log($"La animación {animationName} ha terminado.");
    }
}
