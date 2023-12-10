using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    private string backgroundImagePath = "Menu/wallpapers/mainMenuBackground"; // Ruta del sprite de la imagen de fondo en Resources
    private string button1SpritePath = "Menu/buttons/ButtonArcade"; // Ruta del sprite del primer botón en Resources
    private string button2SpritePath = "Menu/buttons/ButtonSurvival"; // Ruta del sprite del segundo botón en Resources
    private string button3SpritePath = "Menu/buttons/ButtonPvP"; // Ruta del sprite del tercer botón en Resources

    void Start()
    {
        // Crear la imagen de fondo
        CreateImage(backgroundImagePath);

        // Crear los tres botones con lógicas diferentes
        Vector2 button1position = new Vector2(-Screen.width / 4, -Screen.height / 3.5f);
        Vector2 button2position = new Vector2(0f, -Screen.height / 3.5f);
        Vector2 button3position = new Vector2(+Screen.width / 4, -Screen.height / 3.5f);
        CreateButton("ButtonArcade", button1SpritePath, button1position, OnButtonClick1);
        CreateButton("ButtonSurvival", button2SpritePath, button2position, OnButtonClick2);
        CreateButton("ButtonPvP", button3SpritePath, button3position, OnButtonClick3);
    }

    void CreateImage(string imagePath)
    {
        Vector2 size = new Vector2(Screen.width, Screen.height);

        GameObject imageObject = new GameObject("BackgroundImage");
        Image backgroundImage = imageObject.AddComponent<Image>();

        backgroundImage.sprite = Resources.Load<Sprite>(imagePath);

        RectTransform imageRectTransform = imageObject.GetComponent<RectTransform>();
        imageRectTransform.anchoredPosition = new Vector2(0f, 0f);
        imageRectTransform.sizeDelta = size;
        imageObject.transform.SetParent(transform, false);
    }

    void CreateButton(string buttonName, string spritePath, Vector2 anchoredPosition, UnityAction buttonAction)
    {
        GameObject buttonObject = new GameObject(buttonName);
        Image buttonImage = buttonObject.AddComponent<Image>();

        buttonImage.sprite = Resources.Load<Sprite>(spritePath);

        RectTransform buttonRectTransform = buttonObject.GetComponent<RectTransform>();
        buttonRectTransform.anchoredPosition = anchoredPosition;
        buttonRectTransform.sizeDelta = new Vector2(Screen.width / 8, Screen.width / 8);
        buttonObject.transform.SetParent(transform, false);

        Button button = buttonObject.AddComponent<Button>();
        button.onClick.AddListener(buttonAction);
    }

    void OnButtonClick1()
    {
        Debug.Log("Se hizo clic en el Botón 1");
        CallToSelecInfo.ExecutionID = 0;
        SceneManager.LoadScene("PlayerSelectMenu");
    }

    void OnButtonClick2()
    {
        Debug.Log("Se hizo clic en el Botón 2");
        CallToSelecInfo.ExecutionID = 1;
        SceneManager.LoadScene("PlayerSelectMenu");
    }

    void OnButtonClick3()
    {
        Debug.Log("Se hizo clic en el Botón 3");
        CallToSelecInfo.ExecutionID = 2;
        SceneManager.LoadScene("PlayerSelectMenu");
    }
}





