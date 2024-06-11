/*
Este código se encarga de cargar y mostrar los decks del usuario en el juego TCG de dinosaurios.
Fecha: 09/06/24
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DeckLoader : MonoBehaviour
{
    public GameObject deckPrefab; // Prefab que representa un deck
    public Transform deckContainer; // Contenedor donde se instanciarán los decks
    private int userId; // ID del usuario

    // Esta función se llama al iniciar el script.
    // Se encarga de cargar el ID del usuario desde PlayerPrefs y de iniciar la corrutina para cargar los decks.
    void Start()
    {
        StartCoroutine(LoadDecks());
    }

    // Corrutina que envía una solicitud al servidor para obtener los decks del usuario.
    IEnumerator LoadDecks()
    {
        int userId = PlayerPrefs.GetInt("userId", 0);
        string url = $"http://localhost:3000/api/decks/{userId}";
        Debug.Log(url);
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            string json = request.downloadHandler.text;
            Debug.Log("JSON Response: " + json); // Para ver la respuesta JSON y verificar su formato

            DeckList deckList = JsonUtility.FromJson<DeckList>(json);
            DisplayDecks(deckList.decks);
        }
    }

    // Método para mostrar los decks en la interfaz de usuario.
    void DisplayDecks(List<Deck> decks)
    {
        foreach (Deck deck in decks)
        {
            GameObject deckObject = Instantiate(deckPrefab, deckContainer);
            deckObject.transform.localScale = new Vector3(5, 5, 5); // Escala adecuada del panel

            DeckUI deckUI = deckObject.GetComponent<DeckUI>();

            deckUI.SetDeckName(deck.nombre_deck, deck.id_deck); // Asegúrate de que `id_deck` se pase aquí
            deckUI.SetDeckDescription(deck.descripcion_deck);

            for (int i = 0; i < deck.cards.Count && i < 5; i++)
            {
                deckUI.AddCard(deck.cards[i]);
            }
        }
    }

    // Clase interna para representar un deck.
    [System.Serializable]
    public class Deck
    {
        public int id_deck; // ID del deck
        public string nombre_deck;
        public string descripcion_deck;
        public List<Card> cards;
    }

    // Clase interna para representar una lista de decks.
    [System.Serializable]
    public class DeckList
    {
        public List<Deck> decks;
    }

    // Clase interna para representar una carta.
    [System.Serializable]
    public class Card
    {
        public int id_carta;
        public string Nombre;
        public int Puntos_de_Vida;
        public int Puntos_de_ataque;
        public int Coste_en_elixir;
        public string HabilidadDescripcion;
        public string imagen;
    }
}
