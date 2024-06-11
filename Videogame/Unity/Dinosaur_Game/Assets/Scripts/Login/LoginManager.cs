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
    public TMP_Text estado; // Referencia al TextMeshPro para mensajes de estado
    public CanvasGroup estadoCanvasGroup; // Referencia al CanvasGroup para manejar la visibilidad del mensaje de estado

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

        estadoCanvasGroup.alpha = 0; // Asegurarse de que el mensaje de estado esté oculto al inicio
    }

    // Método para registrar un nuevo usuario
    void Register()
    {
        if (usernameInputFieldReg == null || passwordInputFieldReg == null)
        {
            ShowMessage("Uno de los campos de registro es null.", true);
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
                ShowMessage(www.error, true);
            }
            else
            {
                ShowMessage("Usuario registrado exitosamente", false);
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
                ShowMessage(www.error, true);
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

                        ShowMessage("Usuario autenticado", false);

                        SceneManager.LoadScene("MenuInicial");
                    }
                    else
                    {
                        ShowMessage("Error al analizar el userId", true);
                        Debug.LogError("Error parsing userId");
                    }
                }
                else
                {
                    ShowMessage("Usuario no autenticado", true);
                    Debug.Log("Usuario no autenticado");
                }
            }
        }
    }

    // Método para mostrar mensajes de estado y ocultarlos después de 1 segundo
    void ShowMessage(string message, bool isError)
    {
        estado.text = message;
        estado.color = isError ? Color.red : Color.white; // Color rojo para errores, negro para mensajes informativos
        StartCoroutine(ShowMessageCoroutine());
    }

    IEnumerator ShowMessageCoroutine()
    {
        estadoCanvasGroup.alpha = 1;
        yield return new WaitForSeconds(1);
        estadoCanvasGroup.alpha = 0;
    }
}
