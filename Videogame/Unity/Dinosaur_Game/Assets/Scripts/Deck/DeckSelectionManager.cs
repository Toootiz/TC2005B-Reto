/*
Este código se encarga de manejar la selección de decks en el juego TCG de dinosaurios.
Fecha: 09/06/24
*/

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeckSelectionManager : MonoBehaviour
{
    public TextMeshProUGUI selectedDeckText; // Texto para mostrar el mensaje de deck seleccionado
    public CanvasGroup selectedDeckCanvasGroup; // CanvasGroup para manejar la visibilidad del mensaje de deck seleccionado
    private int selectedDeckId;

    void Start()
    {
        selectedDeckCanvasGroup.alpha = 0; // Asegurarse de que el mensaje de estado esté oculto al inicio
    }

    // Esta función se llama cuando el jugador selecciona un deck.
    // Actualiza el texto de la interfaz y guarda el ID del deck seleccionado en PlayerPrefs.
    public void SelectDeck(string deckName, int deckId)
    {
        selectedDeckId = deckId;
        selectedDeckText.text = $"Deck {deckName} seleccionado";
        PlayerPrefs.SetInt("SelectedDeckId", deckId);
        PlayerPrefs.Save();

        // Mostrar el mensaje y ocultarlo después de 1 segundo
        ShowMessage(selectedDeckText.text, false);
    }

    // Esta función se llama cuando el jugador confirma la selección del deck.
    // Cambia a la escena del menú inicial si un deck ha sido seleccionado.
    public void ConfirmSelection()
    {
        if (selectedDeckId != 0)
        {
            SceneManager.LoadScene("MenuInicial"); // Cambia "MenuInicial" por el nombre de tu escena de menú
        }
        else
        {
            ShowMessage("No deck selected!", true);
        }
    }

    // Esta función se llama cuando el jugador decide volver a la selección de decks.
    // Cambia a la escena de selección de decks.
    public void GoBack()
    {
        SceneManager.LoadScene("Deck"); // Cambia "Deck" por el nombre de tu escena de selección de decks
    }

    // Método para mostrar mensajes de estado y ocultarlos después de 1 segundo
    void ShowMessage(string message, bool isError)
    {
        selectedDeckText.text = message;
        StartCoroutine(ShowMessageCoroutine());
    }

    IEnumerator ShowMessageCoroutine()
    {
        selectedDeckCanvasGroup.alpha = 1;
        yield return new WaitForSeconds(1);
        selectedDeckCanvasGroup.alpha = 0;
    }
}
