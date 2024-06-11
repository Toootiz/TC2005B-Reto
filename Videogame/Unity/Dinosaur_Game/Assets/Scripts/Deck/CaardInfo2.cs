/*
Este código se encarga de manejar la información de las cartas y su instancia en el juego TCG de dinosaurios.
Fecha: 09/06/24
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfo2 : MonoBehaviour
{
    public GameObject cardPrefab; // Prefab de la carta que se va a instanciar
    public Transform cardParent; // Padre que se tomara para instanciar las cartas 
    public string Data; // Texto que contiene los datos JSON recibidos de la API

    // Clase interna para representar una carta
    [System.Serializable]
    public class Card
    {
        // Atributos de la carta
        public int id_carta;
        public string Nombre;
        public int Puntos_de_Vida;
        public int Puntos_de_ataque;
        public int Coste_en_elixir;
        public int HabilidadDescripcion;
        public int id_habilidad;
        public int venenodmg;
        public int quemadodmg;
        public int sangradodmg;
        public int mordidadmg;
        public int colatazodmg;
        public int boostvida;
        public int boostataquedmg;
        public int boostcosto;
        public int duracion;
        public string imagen;
    }

    // Clase interna para representar una lista de cartas
    [System.Serializable]
    public class CardList
    {
        // Lista de objetos de las cartas
        public Card[] cards;
    }

    // Instancia de la lista de cartas
    public CardList listaCartas = new CardList();

    // Esta función se llama al iniciar el script.
    void Start()
    {
        // Nada de momento
    }

    // Método para convertir el JSON recibido en una lista de cartas
    public void MakeList()
    {
        listaCartas = JsonUtility.FromJson<CardList>(Data); // Convierte el JSON en una instancia de CardList
    }

    // Método para instanciar las cartas en el juego
    public void CreateCards()
    {
        // Recorre cada carta en la lista de cartas
        foreach (var cardData in listaCartas.cards)
        {
            // Instancia el prefab de la carta como hijo del cardParent
            GameObject newCard = Instantiate(cardPrefab, cardParent);
            // Asegúrate de que se configuran los datos de la carta correctamente
            // Puedes añadir aquí la configuración específica de la carta (textos, imágenes, etc.)
        }
    }
}
