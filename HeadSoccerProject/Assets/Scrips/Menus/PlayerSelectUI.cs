using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class PlayerSelectUI : MonoBehaviour
{
    // Load Required Sprites
    private Sprite wallpaperSp;
    private Sprite goBackButtonSp;
    private Sprite startButtonSp;
    private Sprite nextButtonSp;
    private Sprite lossSp;
    private Sprite drawSp;
    private Sprite victSp;


    // Execution control
    private int executionID = 0;
    // 0 - Arcade
    // 1 - Survival
    // 2 - PvP
    // 3 - Any Result: go back to main menu
    // 5 - Victory:    start a new game
    private string resultState; // victory / draw / loss


    // Other
    public SingleSelection singleSelectionPrefab; // = FindObjectOfType<SingleSelection>();
    GameObject buttonNext;

    private List<SingleSelection> subMenusList = new List<SingleSelection>();



    private void Awake()
    {
        // Find game objects

        // Create game objects
        this.buttonNext = new GameObject("ButtonNext");
        this.executionID = CallToSelecInfo.ExecutionID;
        //this.resultState = CallToSelecInfo.ResultState;
        this.resultState = "loss";

        // Load Sprites
        this.wallpaperSp    = PlayerResourceManager.LoadUI("SelectMenuBackground");
        this.goBackButtonSp = PlayerResourceManager.LoadUI("ButtonBack");
        this.startButtonSp  = PlayerResourceManager.LoadUI("ButtonStart");
        this.nextButtonSp = PlayerResourceManager.LoadUI("ButtonNext");

        this.lossSp = PlayerResourceManager.LoadUI("LossMessage");
        this.drawSp = PlayerResourceManager.LoadUI("DrawMessage");
        this.victSp = PlayerResourceManager.LoadUI("VictoryMessage");
        Scene scene = SceneManager.GetActiveScene();
}

    // Start is called before the first frame update
    void Start()
    {
        this.executionID = CallToSelecInfo.ExecutionID;
        DisplayContent();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(SceneManager.sceneCount);
        
        Image buttonNextImage = this.buttonNext.GetComponent<Image>();

        if (!CheckPlayersAreSelected())
        {
            Color currentColor = buttonNextImage.color;
            currentColor.a = 0.2f;
            buttonNextImage.color = currentColor;
        }
        else
        {
            Color currentColor = buttonNextImage.color;
            currentColor.a = 1f;
            buttonNextImage.color = currentColor;
        }
    }

    private void DisplayTemplate()
    {
        GameObject bgImage = new GameObject("BgImage");
        bgImage.transform.position = Vector2.zero;

        Image bgImageImg = bgImage.AddComponent<Image>();
        bgImageImg.sprite = this.wallpaperSp;

        RectTransform bgImageRectTransform = bgImage.GetComponent<RectTransform>();
        bgImageRectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        bgImage.transform.SetParent(transform, false);



        GameObject buttonBack = new GameObject("ButtonBack");
        buttonBack.transform.position = new Vector2((int)Screen.width * -0.41f, (int)Screen.height * 0.38f);

        Image buttonBackImage = buttonBack.AddComponent<Image>();
        buttonBackImage.sprite = this.goBackButtonSp;

        RectTransform buttonBackRectTransform = buttonBack.GetComponent<RectTransform>();
        buttonBackRectTransform.sizeDelta = new Vector2( (int) Screen.width * 0.05f, (int) Screen.width * 0.05f);
        buttonBack.transform.SetParent(transform, false);

        Button buttonBackButton = buttonBack.AddComponent<Button>();
        buttonBackButton.onClick.AddListener(GoBackToMainMenu);




        this.buttonNext.transform.position = new Vector2((int)Screen.width * 0.37f, (int)Screen.height * -0.4f);

        Image buttonNextImage = this.buttonNext.AddComponent<Image>();
        buttonNextImage.sprite = this.startButtonSp;

        RectTransform buttonNextRectTransform = this.buttonNext.GetComponent<RectTransform>();
        buttonNextRectTransform.sizeDelta = new Vector2((int)Screen.width * 0.11f, (int)Screen.width * 0.05f);
        this.buttonNext.transform.SetParent(transform, false);

        Button buttonNextButton = buttonNext.AddComponent<Button>();
        buttonNextButton.onClick.AddListener(StartGame);
    }


    private void DisplayContent()
    {
      
        if (this.executionID == 0 || this.executionID == 2)
        {
            DisplayTemplate();

            Vector2 boxPosition1 = new Vector2((int)Screen.width * -0.2f, 0);
            SingleSelection singleSelection1 = Instantiate(this.singleSelectionPrefab);
            singleSelection1.CreatePlayerSelectionBox(boxPosition1, transform);
            this.subMenusList.Add(singleSelection1);

            Vector2 boxPosition2 = new Vector2((int)Screen.width * 0.2f, 0);
            SingleSelection singleSelection2 = Instantiate(this.singleSelectionPrefab);
            singleSelection2.CreatePlayerSelectionBox(boxPosition2, transform);
            this.subMenusList.Add(singleSelection2);
        }
        else if (this.executionID == 1)
        {
            DisplayTemplate();

            Vector2 boxPosition = Vector2.zero;
            SingleSelection singleSelection = Instantiate(this.singleSelectionPrefab);
            singleSelection.CreatePlayerSelectionBox(boxPosition, transform);

            this.subMenusList.Add(singleSelection);
        }
        else if (this.executionID == 3)
        {
            //Debug.Log("result State " + this.resultState);
            // Generic Template
            DisplayTemplate();

            // Result Message
            GameObject message = new GameObject("Message");
            message.transform.position = Vector2.zero;

            Image messageImg = message.AddComponent<Image>();
            Vector2 messageSize;

            if (this.resultState == "victory")
            {
                messageSize = new Vector2((int)Screen.width * 0.4f, (int)Screen.height * 0.12f);
                messageImg.sprite = this.victSp;
            }
            else if (this.resultState == "draw")
            {
                messageSize = new Vector2((int)Screen.width * 0.4f, (int)Screen.height * 0.12f);
                messageImg.sprite = this.drawSp;
            }
            else // if (this.resultState == "loss")
            {
                messageSize = new Vector2((int)Screen.width * 0.4f, (int)Screen.height * 0.12f);
                messageImg.sprite = this.lossSp;
            }

            RectTransform messageRectTransform = message.GetComponent<RectTransform>();
            messageRectTransform.sizeDelta = messageSize;
            message.transform.SetParent(transform, false);

            // Hide button to start next game
            this.buttonNext.SetActive(false);
        }
        else if (this.executionID == 4)
        {
            // Generic Template
            DisplayTemplate();

            // Victory Message
            GameObject message = new GameObject("Message");
            message.transform.position = Vector2.zero;

            Image messageImg = message.AddComponent<Image>();
            Vector2 messageSize = new Vector2((int)Screen.width * 0.4f, (int)Screen.height * 0.12f);
            messageImg.sprite = this.victSp;

            RectTransform messageRectTransform = message.GetComponent<RectTransform>();
            messageRectTransform.sizeDelta = messageSize;
            message.transform.SetParent(transform, false);

            // Chage button sprite from "Start" to "Next"
            Image buttonNextImage = this.buttonNext.GetComponent<Image>();
            buttonNextImage.sprite = this.nextButtonSp;
        }

    }


    private bool CheckPlayersAreSelected()
    {
        bool allSelected = true;

        foreach (SingleSelection subMenu in this.subMenusList)
        {
            allSelected = allSelected && subMenu.IsPlayerSelected;
        }

        return allSelected;
    }


    private void GoBackToMainMenu()
    {
        //Debug.Log("Go back to Main Menu");
        SceneManager.LoadScene("MainMenu");



    }

    private void StartGame()
    {
        //Debug.Log("Start Game");
        
        if (CheckPlayersAreSelected())
        {

            // Para saber cómo tienes que cargar el juego tendrás que consultar: this.executionID
            if (executionID == 0 || executionID == 2)
            {
                // Add player 1 data
                SingleSelection subMenu1 = subMenusList[0];
                PreGameData.Player1Info = subMenu1.PlayerInfo;
                PreGameData.Player1Sprites = subMenu1.PlayerSprites;

                // Add player 2 data
                SingleSelection subMenu2 = subMenusList[1];
                PreGameData.Player2Info    = subMenu2.PlayerInfo;
                PreGameData.Player2Sprites = subMenu2.PlayerSprites;

                // We put the players subMenusList to inintial state

                subMenusList = new List<SingleSelection>();

                if (executionID == 0)
                {
                    PreGameData.GameMode = "arcade";
                }
                else
                {
                    PreGameData.GameMode = "pvp";
                }
                
                SceneManager.LoadScene("GameScene");

            }
            else if (executionID == 1)
            {
                // Add player 1 data
                SingleSelection subMenu1 = subMenusList[0];
                PreGameData.Player1Info = subMenu1.PlayerInfo;
                PreGameData.Player1Sprites = subMenu1.PlayerSprites;


                // Generate player 2 data
                int numberOfAvailablePlayers = PlayerResourceManager.GetNumberOfAvailablePlayers();
                System.Random random = new System.Random();
                int randomPlayerID = random.Next(1, numberOfAvailablePlayers + 1);
                PreGameData.Player2Info = PlayerResourceManager.LoadPlayerInfo(randomPlayerID);
                PreGameData.Player2Sprites = PlayerResourceManager.LoadPlayerSprites(randomPlayerID);

                subMenusList = new List<SingleSelection>();

                PreGameData.GameMode = "survival";
                SceneManager.LoadSceneAsync("GameScene");


            }
            else if (executionID == 3)
            {
                SceneManager.LoadScene("MainMenu");
            }

            else if (executionID == 4)
            {
                SceneManager.LoadSceneAsync("GameScene");

            }

            
        }


    }



}
