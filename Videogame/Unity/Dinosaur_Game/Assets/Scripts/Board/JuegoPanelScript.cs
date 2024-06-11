using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JuegoPanelScript : MonoBehaviour, IDropHandler //manejo de eventos drop
{
    public List<GameObject> cards = new List<GameObject>();
    public int maxCards = 5;

    public void OnDrop(PointerEventData eventData)
    {
        CardScript card = eventData.pointerDrag.GetComponent<CardScript>();
        if (card != null && card.isEnemyCard)
        {
            Debug.Log("No se pueden soltar cartas enemigas aquí.");
            return;
        }

        if (cards.Count >= maxCards)
        {
            Debug.Log("El panel ya tiene el máximo de cartas permitidas.");
            return;
        }

        GameManagement gameManagement = GameObject.FindGameObjectWithTag("GameManagement").GetComponent<GameManagement>();
        if (gameManagement.ambar < card.CardCost)
        {
            Debug.Log("¡No tienes suficiente energía!");
            return;
        }

        // Añadir la carta a la lista del panel
        if (!cards.Contains(eventData.pointerDrag))
        {
            cards.Add(eventData.pointerDrag);
            Debug.Log("Carta agregada al panel de juego.");
        }
    }

    public void RemoveCard(GameObject card)
    {
        if (cards.Contains(card))
        {
            cards.Remove(card);
            Debug.Log("Carta removida del panel de juego.");
        }
    }
}
