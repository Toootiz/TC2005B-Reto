/*
Este código se encarga de manejar la gestión de decks en el juego TCG de dinosaurios.
Fecha: 09/06/24
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class DeckManagment : MonoBehaviour
{
    public GameObject CardPrefab; // Referencia al prefab de la carta
    public Transform selectedCards; // Panel para cartas seleccionadas
    public Transform allCards; // Panel para todas las cartas
    public TMP_InputField deckNameInput; // Campo de texto para el nombre del deck
    public TMP_InputField deckDescriptionInput; // Campo de texto para la descripción del deck
    CardInfo2 cards; // Información de las cartas
    private int userId;
    public string apiResult; // Resultado de la llamada a la API
    public TMP_Text estado; // Referencia al TextMeshPro para mensajes de estado
    public CanvasGroup estadoCanvasGroup; // Referencia al CanvasGroup para manejar la visibilidad del mensaje de estado

    // Esta función se llama al iniciar el script.
    // Se encarga de cargar los datos de las cartas y generar las cartas en la interfaz.
    void Start()
    {
        cards = GameObject.FindGameObjectWithTag("CardData").GetComponent<CardInfo2>();
        selectedCards = GameObject.FindGameObjectWithTag("SelectedCards").transform;
        allCards = GameObject.FindGameObjectWithTag("CardsDesck").transform;
        LoadCardData();
        GenerateAllCards(); // Generar todas las cartas
        estadoCanvasGroup.alpha = 0; // Asegurarse de que el mensaje de estado esté oculto al inicio
    }

    // Esta función carga los datos de las cartas desde PlayerPrefs.
    void LoadCardData()
    {
        if (PlayerPrefs.HasKey("CardData"))
        {
            apiResult = PlayerPrefs.GetString("CardData");
            cards.Data = apiResult;
            cards.MakeList();
        }
        else
        {
            ShowMessage("No se encontraron datos de cartas en PlayerPrefs", true);
        }
    }

    // Esta función genera todas las cartas en el panel de todas las cartas.
    public void GenerateAllCards()
    {
        for (int i = 0; i < cards.listaCartas.cards.Length; i++)
        {
            InstantiateCard(i, 0, 0);
        }
    }

    // Esta función instancia una carta en la interfaz.
    public void InstantiateCard(int id, float posX, float posY)
    {
        GameObject newcard = Instantiate(CardPrefab, allCards);
        newcard.transform.localScale = Vector3.one; // Ajustar la escala a 1 para evitar problemas de escala

        TextMeshProUGUI nameText = newcard.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI lifeText = newcard.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI attackText = newcard.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI costText = newcard.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI HabilidadText = newcard.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        Image cardImage = newcard.transform.GetChild(5).GetComponent<Image>();

        nameText.text = cards.listaCartas.cards[id].Nombre;
        lifeText.text = cards.listaCartas.cards[id].Puntos_de_Vida.ToString();
        attackText.text = cards.listaCartas.cards[id].Puntos_de_ataque.ToString();
        costText.text = cards.listaCartas.cards[id].Coste_en_elixir.ToString();
        HabilidadText.text = cards.listaCartas.cards[id].HabilidadDescripcion.ToString();

        Sprite cardSprite = Resources.Load<Sprite>($"DinoImages/{id}");

        if (cardSprite != null)
        {
            cardImage.sprite = cardSprite;
        }
        else
        {
            ShowMessage($"Imagen {id} no encontrada en Resources/IMG/", true);
        }

        CardScript2 cardScript = newcard.GetComponent<CardScript2>();
        cardScript.CardId = cards.listaCartas.cards[id].id_carta; // Asigna el id de la carta
        cardScript.CardName = cards.listaCartas.cards[id].Nombre;
        cardScript.CardAttack = cards.listaCartas.cards[id].Puntos_de_ataque;
        cardScript.CardLife = cards.listaCartas.cards[id].Puntos_de_Vida;
        cardScript.CardCost = cards.listaCartas.cards[id].Coste_en_elixir;
        cardScript.CardHabilidad = cards.listaCartas.cards[id].HabilidadDescripcion;
        cardScript.Cardvenenodmg = cards.listaCartas.cards[id].venenodmg;
        cardScript.Cardquemadodmg = cards.listaCartas.cards[id].quemadodmg;
        cardScript.Cardsangradodmg = cards.listaCartas.cards[id].sangradodmg;
        cardScript.Cardmordidadmg = cards.listaCartas.cards[id].mordidadmg;
        cardScript.Cardcolatazodmg = cards.listaCartas.cards[id].colatazodmg;
        cardScript.Cardboostvida = cards.listaCartas.cards[id].boostvida;
        cardScript.Cardboostataquedmg = cards.listaCartas.cards[id].boostataquedmg;
        cardScript.Cardboostcosto = cards.listaCartas.cards[id].boostcosto;
        cardScript.Cardduracion = cards.listaCartas.cards[id].duracion;
        cardScript.CardArt = cardImage;
    }

    // Opcional: realiza alguna acción adicional cuando se agrega una carta a la selección.
    public void AddCardToSelected(CardScript2 card)
    {
        // Implementar según sea necesario.
    }

    // Opcional: realiza alguna acción adicional cuando se elimina una carta de la selección.
    public void RemoveCardFromSelected(CardScript2 card)
    {
        // Implementar según sea necesario.
    }

    [System.Serializable]
    public class DeckData
    {
        public int id_jugador;
        public string nombre_deck;
        public string descripcion_deck;
        public int id_carta1;
        public int id_carta2;
        public int id_carta3;
        public int id_carta4;
        public int id_carta5;
        public int id_carta6;
        public int id_carta7;
        public int id_carta8;
        public int id_carta9;
        public int id_carta10;
    }

    // Esta función guarda las cartas seleccionadas en PlayerPrefs y envía los datos al servidor.
    public void SaveSelectedCards()
    {
        string deckName = deckNameInput.text;
        string deckDescription = deckDescriptionInput.text;

        if (string.IsNullOrEmpty(deckName))
        {
            ShowMessage("Falta nombre del deck", true);
            return;
        }

        if (string.IsNullOrEmpty(deckDescription))
        {
            ShowMessage("Falta descripción del deck", true);
            return;
        }

        if (selectedCards.childCount != 10)
        {
            ShowMessage("Debe seleccionar exactamente 10 cartas", true);
            return;
        }

        List<int> selectedCardIds = new List<int>();

        foreach (Transform cardTransform in selectedCards)
        {
            CardScript2 cardScript = cardTransform.GetComponent<CardScript2>();
            if (cardScript != null)
            {
                selectedCardIds.Add(cardScript.CardId);
            }
        }

        int userId = PlayerPrefs.GetInt("userId", 0);

        Debug.Log(userId);

        DeckData deckData = new DeckData
        {
            id_jugador = userId, // Cargar el id del jugador desde PlayerPrefs
            nombre_deck = deckName,
            descripcion_deck = deckDescription,
            id_carta1 = selectedCardIds[0],
            id_carta2 = selectedCardIds[1],
            id_carta3 = selectedCardIds[2],
            id_carta4 = selectedCardIds[3],
            id_carta5 = selectedCardIds[4],
            id_carta6 = selectedCardIds[5],
            id_carta7 = selectedCardIds[6],
            id_carta8 = selectedCardIds[7],
            id_carta9 = selectedCardIds[8],
            id_carta10 = selectedCardIds[9]
        };

        string json = JsonUtility.ToJson(deckData);
        Debug.Log("Selected Cards JSON: " + json);

        PlayerPrefs.SetString("SelectedCards", json);
        PlayerPrefs.Save();

        StartCoroutine(PostDeckData("http://localhost:3000/api/guardardeck", json));
    }

    // Corrutina que envía los datos del deck al servidor.
    IEnumerator PostDeckData(string url, string json)
    {
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            ShowMessage("Error al guardar el deck: " + request.error, true);
            Debug.Log("Error: " + request.error);
            Debug.Log("Response: " + request.downloadHandler.text); // Imprimir la respuesta del servidor
        }
        else
        {
            ShowMessage("Deck guardado exitosamente: " + request.downloadHandler.text);
            Debug.Log("Deck saved successfully: " + request.downloadHandler.text);
        }
    }

    // Esta función limpia las cartas seleccionadas.
    public void ClearSelectedCards()
    {
        foreach (Transform child in selectedCards)
        {
            child.SetParent(allCards);
        }
    }

    // Esta función guarda las cartas seleccionadas y cambia a la escena "SelectedDeck".
    public void SaveAndChangeScene()
    {
        SaveSelectedCards();
        SceneManager.LoadScene("SelectedDeck");
    }

    // Esta función cambia a la escena "SelectedDeck".
    public void DeckScene()
    {
        SceneManager.LoadScene("DeckSeleccionado");
    }
    
    public void PlayScene()
    {
        SceneManager.LoadScene("Board");
    }

    // Esta función vuelve al menú inicial.
    public void Volver()
    {
        SceneManager.LoadScene("MenuInicial");
    }

    // Método para mostrar mensajes de estado y ocultarlos después de 1 segundo.
    void ShowMessage(string message, bool isError = false)
    {
        estado.text = message;
        StartCoroutine(ShowMessageCoroutine());
    }

    // Corrutina que maneja la animación de mostrar y ocultar el mensaje de estado.
    IEnumerator ShowMessageCoroutine()
    {
        estadoCanvasGroup.alpha = 1;
        yield return new WaitForSeconds(1);
        estadoCanvasGroup.alpha = 0;
    }
}