using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    // Referencias a los campos de entrada de usuario y contraseña para registro y login
    public TMP_InputField usernameInputFieldReg;
    public TMP_InputField passwordInputFieldReg;
    public TMP_InputField usernameInputFieldLog;
    public TMP_InputField passwordInputFieldLog;
    public Button registerButton;
    public Button loginButton;

    void Start()
    {
        // Asignar listeners a los botones de registro y login
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

    // Método para registrar un nuevo usuario
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

    // Método para iniciar sesión
    void Login()
    {
        string nombre = usernameInputFieldLog.text;
        string contrasena = passwordInputFieldLog.text;

        StartCoroutine(LoginUser(nombre, contrasena));
    }

    // Corrutina para registrar un usuario en el servidor
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

    // Corrutina para iniciar sesión en el servidor
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
                    // Extraer user ID de la respuesta
                    string userIdStr = www.downloadHandler.text.Split(':')[1];
                    int userId;
                    if (int.TryParse(userIdStr, out userId))
                    {
                        PlayerPrefs.SetInt("userId", userId);
                        Debug.Log("UserId: " + userId);
                        PlayerPrefs.SetString("nombre", nombre);

                        Debug.Log("Usuario autenticado");

                        SceneManager.LoadScene("MenuInicial");
                    }
                    else
                    {
                        Debug.LogError("Error parsing userId");
                    }
                }
                else
                {
                    Debug.Log("Usuario no autenticado");
                }
            }
        }
    }
}