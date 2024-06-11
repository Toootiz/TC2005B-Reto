using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInfo : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform cardParent;
    public string Data;

    [System.Serializable]
    public class Card
    {
        public int id_carta;
        public string Nombre;
        public int Puntos_de_Vida;
        public int Puntos_de_ataque;
        public int Coste_en_elixir;
        public string descripcion;
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

    [System.Serializable]
    public class CardList
    {
        public Card[] cards;
    }

    public CardList listaCartas = new CardList();

    void Start()
    {
    }

    public void MakeList()
    {
        if (!string.IsNullOrEmpty(Data))
        {
            listaCartas = JsonUtility.FromJson<CardList>(Data);
            Debug.Log("Lista de cartas creada con " + listaCartas.cards.Length + " cartas.");
        }
        else
        {
            Debug.LogError("Data is null or empty. Cannot create card list.");
        }
    }

    public void CreateCards()
    {
        foreach (var cardData in listaCartas.cards)
        {
            GameObject newCard = Instantiate(cardPrefab, cardParent);
        }
    }

    public void InstantiateCard(int id, float posX, float posY)
    {
        if (id < 0 || id >= listaCartas.cards.Length)
        {
            Debug.LogError("ID de carta fuera de rango: " + id);
            return;
        }

        GameObject newcard = Instantiate(cardPrefab, new Vector3(posX, posY, 0), Quaternion.identity);
        newcard.transform.SetParent(cardParent.transform, false);
        newcard.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);

        TextMeshProUGUI nameText = newcard.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI lifeText = newcard.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI attackText = newcard.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI costText = newcard.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI habilidadText = newcard.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        Image cardImage = newcard.transform.GetChild(5).GetComponent<Image>();

        Card cardData = listaCartas.cards[id];

        // Asignar valores de texto a la UI de la carta
        nameText.text = cardData.Nombre;
        lifeText.text = cardData.Puntos_de_Vida.ToString();
        attackText.text = cardData.Puntos_de_ataque.ToString();
        costText.text = cardData.Coste_en_elixir.ToString();
        habilidadText.text = cardData.descripcion;

        CardScript cardScript = newcard.GetComponent<CardScript>();

        // Asignar valores al script de la carta
        cardScript.CardId = cardData.id_carta;
        cardScript.CardName = cardData.Nombre;
        cardScript.CardAttack = cardData.Puntos_de_ataque;
        cardScript.CardLife = cardData.Puntos_de_Vida;
        cardScript.CardCost = cardData.Coste_en_elixir;
        cardScript.descripcion = cardData.descripcion;
        cardScript.Cardvenenodmg = cardData.venenodmg;
        cardScript.Cardquemadodmg = cardData.quemadodmg;
        cardScript.Cardsangradodmg = cardData.sangradodmg;
        cardScript.Cardmordidadmg = cardData.mordidadmg;
        cardScript.Cardcolatazodmg = cardData.colatazodmg;
        cardScript.Cardboostvida = cardData.boostvida;
        cardScript.Cardboostataquedmg = cardData.boostataquedmg;
        cardScript.Cardboostcosto = cardData.boostcosto;
        cardScript.Cardduracion = cardData.duracion;
        cardScript.CardArt = cardImage;
    }
}
