using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField usernameInputFieldReg;
    public TMP_InputField passwordInputFieldReg;
    public TMP_InputField usernameInputFieldLog;
    public TMP_InputField passwordInputFieldLog;
    public Button registerButton;
    public Button loginButton;

   void Start()
{
    if (registerButton != null)
    {
        registerButton.onClick.AddListener(Register);
    }
    else
    {
        Debug.LogError("registerButton no está asignado en el inspector.");
    }

    if (loginButton != null)
    {
        loginButton.onClick.AddListener(Login);
    }
    else
    {
        Debug.LogError("loginButton no está asignado en el inspector.");
    }
}

    void Register()
{
    if (usernameInputFieldReg == null || passwordInputFieldReg == null)
    {
        Debug.LogError("Uno de los campos de registro es null.");
        return;
    }

    string nombre = usernameInputFieldReg.text;
    string contrasena = passwordInputFieldReg.text;

    StartCoroutine(RegisterUser(nombre, contrasena));
}


    void Login()
    {
        string nombre = usernameInputFieldLog.text;
        string contrasena = passwordInputFieldLog.text;

        StartCoroutine(LoginUser(nombre, contrasena));
    }

    IEnumerator RegisterUser(string nombre, string contrasena)
    {
        string payload = "{\"nombre\":\"" + nombre + "\",\"contrasena\":\"" + contrasena + "\"}";

        using (UnityWebRequest www = UnityWebRequest.Put("http://localhost:3000/register", payload))
        {
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Usuario registrado exitosamente");
            }
        }
    }

    IEnumerator LoginUser(string nombre, string contrasena)
    {
        string payload = "{\"nombre\":\"" + nombre + "\",\"contrasena\":\"" + contrasena + "\"}";

        using (UnityWebRequest www = UnityWebRequest.Put("http://localhost:3000/login", payload))
        {
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.downloadHandler.text.Contains("Usuario autenticado"))
                {
                    // Extract user ID from response
                    string userId = www.downloadHandler.text.Split(':')[1];
                    PlayerPrefs.SetString("userId", userId);
                    PlayerPrefs.SetString("nombre", nombre);

                    Debug.Log("Usuario autenticado");
                    SceneManager.LoadScene("MenuInicial");
                }
                else
                {
                    Debug.Log("Usuario no autenticado");
                }
            }
        }
    }
}
