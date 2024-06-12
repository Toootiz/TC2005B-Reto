using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlaySoundOnSelect : MonoBehaviour, ISelectHandler
{
    public AudioSource audioSource;  // Referencia al AudioSource
    public AudioClip soundInDeckScene;  // Sonido para la escena 1
    public AudioClip soundInBoardScene;  
    public void OnSelect(BaseEventData eventData)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        switch (currentSceneName)
        {
            case "DeckScene":
                audioSource.clip = soundInDeckScene;
                break;
            case "BoardScene":
                audioSource.clip = soundInBoardScene;
                break;
            // Agrega más casos para otras escenas
            default:
                Debug.LogWarning("Escena no reconocida. No se asignó sonido.");
                return;
        }

        // Reproducir el sonido asignado
        audioSource.Play();
    }
}
