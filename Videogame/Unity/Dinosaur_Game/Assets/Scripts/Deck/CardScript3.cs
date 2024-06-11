/*
Este código se encarga de manejar la lógica de las cartas y su actualización en la interfaz de usuario en el juego TCG de dinosaurios.
Fecha: 09/06/24
*/

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardScript3 : MonoBehaviour
{
    // Variables para guardar los datos que la carta recibe
    public int CardId, CardLife, CardAttack, CardCost, CardHabilidad;
    public string CardName, descripcion;
    public Image CardArt;

    // Referencias a los componentes de la interfaz
    public TextMeshProUGUI cardNameText;
    public TextMeshProUGUI cardLifeText;
    public TextMeshProUGUI cardAttackText;
    public TextMeshProUGUI cardCostText;
    public TextMeshProUGUI cardHabilidadText;

    // Esta función se llama al iniciar el script.
    // Se encarga de actualizar la interfaz de la carta.
    void Start()
    {
        UpdateCardUI();
    }

    // Método para actualizar la interfaz de la carta
    public void UpdateCardUI()
    {
        if (cardNameText != null) cardNameText.text = CardName;
        if (cardLifeText != null) cardLifeText.text = CardLife.ToString();
        if (cardAttackText != null) cardAttackText.text = CardAttack.ToString();
        if (cardCostText != null) cardCostText.text = CardCost.ToString();
        if (cardHabilidadText != null) cardHabilidadText.text = descripcion;  // Actualizar con la descripción
        // Asegúrate de que la imagen está configurada correctamente
        if (CardArt != null) CardArt.sprite = Resources.Load<Sprite>($"DinoImages/{CardId}");
    }
}
