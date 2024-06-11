using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuInicial : MonoBehaviour
{
    public AudioMixer audioMixer;

    void Start()
    {
        int userId = PlayerPrefs.GetInt("userId", 0);
        int deckId = PlayerPrefs.GetInt("SelectedDeckId", 0);
        Debug.Log(userId);
        Debug.Log(deckId);
    }

    public void Jugar()
    {
        SceneManager.LoadScene("Board");
    }

    public void Salir()
    {
        Debug.Log("Salir");
        SceneManager.LoadScene("Login");
        //Application.Quit();
    }

    public void Deck()
    {
        SceneManager.LoadScene("Deck");
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
