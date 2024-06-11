/*
Este código se encarga de manejar la lógica de las cartas en el juego TCG de dinosaurios.
Fecha: 09/06/24
*/

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardScript2 : MonoBehaviour, IPointerClickHandler
{
    // Variables para guardar los datos que la carta recibe
    public int CardId, CardLife, CardAttack, CardCost, CardHabilidad, Cardvenenodmg, Cardquemadodmg, Cardsangradodmg, Cardmordidadmg, Cardcolatazodmg, Cardboostvida, Cardboostataquedmg, Cardboostcosto, Cardduracion;
    public string CardName;
    public Image CardArt;
    private RectTransform rectTransform;
    private Transform originalParent;

    // Referencia al script de gestión del juego
    DeckManagment gameManagement;

    // Esta función se llama al iniciar el script.
    // Se encarga de obtener las referencias necesarias.
    void Start()
    {
        // Obtener la referencia al componente con tag de DeckManagment
        gameManagement = GameObject.FindGameObjectWithTag("DeckManagment").GetComponent<DeckManagment>();
        rectTransform = GetComponent<RectTransform>();
        originalParent = transform.parent;
    }

    // Esta función se llama cuando se hace clic en la carta.
    // Se encarga de mover la carta entre el panel de todas las cartas y el panel de cartas seleccionadas.
    public void OnPointerClick(PointerEventData eventData)
    {
        if (transform.parent.CompareTag("CardsDesck"))
        {
            if (gameManagement.selectedCards.childCount < 10)
            {
                MoveToSelected();
            }
            else
            {
                Debug.Log("No puedes seleccionar más de 10 cartas.");
            }
        }
        else if (transform.parent.CompareTag("SelectedCards"))
        {
            MoveToAll();
        }
    }

    // Esta función mueve la carta al panel de cartas seleccionadas.
    void MoveToSelected()
    {
        originalParent = transform.parent;
        transform.SetParent(GameObject.FindGameObjectWithTag("SelectedCards").transform);
        gameManagement.AddCardToSelected(this);
    }

    // Esta función mueve la carta al panel de todas las cartas.
    void MoveToAll()
    {
        originalParent = transform.parent;
        transform.SetParent(GameObject.FindGameObjectWithTag("CardsDesck").transform);
        gameManagement.RemoveCardFromSelected(this);
    }
}
