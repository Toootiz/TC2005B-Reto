/*
Este código se encarga de manejar la interfaz de usuario para los decks en el juego TCG de dinosaurios.
Fecha: 09/06/24
*/

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class DeckUI : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI deckNameText; // Texto para el nombre del deck
    public TextMeshProUGUI deckDescriptionText; // Texto para la descripción del deck
    public Transform cardContainer; // Contenedor para las cartas del deck
    public GameObject cardPrefab; // Prefab que representa una carta
    private string deckName;
    private int deckId;

    // Referencia al DeckSelectionManager que maneja la lógica de selección de decks
    private DeckSelectionManager deckSelectionManager;

    // Esta función se llama al iniciar el script.
    // Se encarga de obtener la referencia al DeckSelectionManager.
    void Start()
    {
        deckSelectionManager = FindObjectOfType<DeckSelectionManager>();
    }

    // Método para establecer el nombre del deck y su ID.
    public void SetDeckName(string name, int id)
    {
        deckName = name;
        deckId = id;
        deckNameText.text = name;
        Debug.Log(deckName + deckId);
    }

    // Método para establecer la descripción del deck.
    public void SetDeckDescription(string description)
    {
        deckDescriptionText.text = description;
    }

    // Método para agregar una carta al contenedor de cartas del deck.
    public void AddCard(DeckLoader.Card card)
    {
        GameObject cardObject = Instantiate(cardPrefab, cardContainer);
        cardObject.transform.localScale = new Vector3(0.20f, 0.20f, 0.20f);

        CardScript3 cardScript3 = cardObject.GetComponent<CardScript3>();

        cardScript3.CardId = card.id_carta;
        cardScript3.CardName = card.Nombre;
        cardScript3.CardLife = card.Puntos_de_Vida;
        cardScript3.CardAttack = card.Puntos_de_ataque;
        cardScript3.CardCost = card.Coste_en_elixir;

        int habilidad;
        if (int.TryParse(card.HabilidadDescripcion, out habilidad))
        {
            cardScript3.CardHabilidad = habilidad;
        }
        else
        {
            cardScript3.CardHabilidad = 0;
        }

        cardScript3.cardNameText = cardObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        cardScript3.cardLifeText = cardObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        cardScript3.cardAttackText = cardObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        cardScript3.cardCostText = cardObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        cardScript3.cardHabilidadText = cardObject.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        cardScript3.CardArt = cardObject.transform.GetChild(5).GetComponent<Image>();

        cardScript3.UpdateCardUI();
    }

    // Esta función se llama cuando se hace clic en el deck.
    // Se encarga de notificar al DeckSelectionManager sobre la selección del deck.
    public void OnPointerClick(PointerEventData eventData)
    {
        if (deckSelectionManager != null)
        {
            deckSelectionManager.SelectDeck(deckName, deckId);
        }
    }
}
