using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
//using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;


public class SingleSelection : MonoBehaviour
{
    // Load Sprites
    private Sprite greenBoxSp;
    private Sprite infoBoxSp;
    private Sprite topTitleSp;
    private Sprite selectButtonSp;
    private Sprite cancelButtonSp;
    private Sprite leftButtonSp;
    private Sprite rightButtonSp;

    private Vector2 position = Vector2.zero;

    private int selectedPlayer = 1;
    private bool isPlayerSelected = false;
    private Dictionary<string, string> playerInfo;
    private Dictionary<string, Sprite> playerSprites;

    // valores variables
    GameObject playerNameText;
    GameObject playerNameBox;
    GameObject playerHead;
    GameObject playerHeadBg;
    GameObject playerSwitchLeft;
    GameObject playerSwitchRight;
    GameObject playerFlag;
    GameObject playerStars;
    GameObject playerSelect;




    void Awake()
    {

        // Create game objects
        this.playerNameText = new GameObject("PlayerName");
        this.playerNameBox = new GameObject("PlayerNameBox");
        this.playerHead = new GameObject("PlayerHead");
        this.playerHeadBg = new GameObject("PlayerHeadBg");
        this.playerSwitchRight = new GameObject("PlayerSwitchRight");
        this.playerSwitchLeft = new GameObject("PlayerSwitchLeft");
        this.playerFlag = new GameObject("PlayerFlag");
        this.playerStars = new GameObject("PlayerStars");
        this.playerSelect = new GameObject("PlayerSelect");


        // Load Sprites
        this.greenBoxSp = PlayerResourceManager.LoadUI("GreenBox");
        this.infoBoxSp = PlayerResourceManager.LoadUI("InfoBox");
        this.topTitleSp = PlayerResourceManager.LoadUI("LevelBox");
        this.selectButtonSp = PlayerResourceManager.LoadUI("ButtonSelect");
        this.cancelButtonSp = PlayerResourceManager.LoadUI("ButtonCancel");
        this.leftButtonSp = PlayerResourceManager.LoadUI("ButtonLeft");
        this.rightButtonSp = PlayerResourceManager.LoadUI("ButtonRight");

        this.playerInfo = PlayerResourceManager.LoadPlayerInfo(this.selectedPlayer);
        this.playerSprites = PlayerResourceManager.LoadPlayerSprites(this.selectedPlayer);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    public void UpdateUI()
    {
        this.playerInfo = PlayerResourceManager.LoadPlayerInfo(this.selectedPlayer);
        this.playerSprites = PlayerResourceManager.LoadPlayerSprites(this.selectedPlayer);

        // Update Player Info
        TextMeshProUGUI playerNameTextDisplay = this.playerNameText.GetComponent<TextMeshProUGUI>();
        playerNameTextDisplay.text = playerInfo["name"];
        //SpriteRenderer playerHeadImg = this.playerHead.GetComponent<SpriteRenderer>();
        Image playerHeadImg = this.playerHead.GetComponent<Image>();
        playerHeadImg.sprite = this.playerSprites["head"];
        Image playerFlagImg = this.playerFlag.GetComponent<Image>();
        playerFlagImg.sprite = this.playerSprites["flag"];
        Image playerStarsImg = this.playerStars.GetComponent<Image>();
        playerStarsImg.sprite = this.playerSprites["stars"];


        this.playerNameText.SetActive(true);
        this.playerNameBox.SetActive(true);
        this.playerHead.SetActive(true);
        this.playerHeadBg.SetActive(true);
        this.playerFlag.SetActive(true);
        this.playerStars.SetActive(true);
        this.playerSelect.SetActive(true);

        if (this.isPlayerSelected)
        {
            this.playerSwitchRight.SetActive(false);
            this.playerSwitchLeft.SetActive(false);
        }
        else
        {
            this.playerSwitchRight.SetActive(true);
            this.playerSwitchLeft.SetActive(true);
        }
    }

    public void UpdatePlayerSelectionBoxContent()
    {
        TextMeshProUGUI playerNameDisplay = this.playerNameText.GetComponentInChildren<TextMeshProUGUI>();
        playerNameDisplay.text = this.playerInfo["name"];

    }

    public void CreatePlayerSelectionBox(Vector2 position, Transform canvasTransform)
    {

        // Infobox

        Vector2 infoBoxImgSize = new Vector2((int)(Screen.width * (2f / 7f)), (int)(Screen.height * 0.7f));

        GameObject infoBox = new GameObject("InfoBox");
        Image infoBoxImg = infoBox.AddComponent<Image>();
        infoBoxImg.sprite = this.infoBoxSp;

        RectTransform infoBoxImgRectTransform = infoBox.GetComponent<RectTransform>();
        infoBoxImgRectTransform.anchoredPosition = position;
        infoBoxImgRectTransform.sizeDelta = infoBoxImgSize;
        infoBox.transform.SetParent(canvasTransform, false);




        // Player name
        Vector2 playerNameDisplacement = new Vector2(0, (int)(infoBoxImgSize.y * 0.42f));
        this.playerNameText.transform.position = position + playerNameDisplacement;
        this.playerNameBox.transform.position = position + playerNameDisplacement;

        //      Img
        Vector2 playerNameImgSize = new Vector2((int)(infoBoxImgSize.x * 0.9f), (int)(infoBoxImgSize.y * 0.1f));

        Image playerNameImg = this.playerNameBox.AddComponent<Image>();
        playerNameImg.sprite = this.topTitleSp;

        RectTransform playerNameImgRectTransform = playerNameImg.GetComponent<RectTransform>();
        //playerNameImgRectTransform.anchoredPosition = position;
        playerNameImgRectTransform.sizeDelta = playerNameImgSize;
        playerNameImg.transform.SetParent(canvasTransform, false);

        //      Text
        TextMeshProUGUI playerNameTextDisplay = this.playerNameText.AddComponent<TextMeshProUGUI>();
        playerNameTextDisplay.text = this.playerInfo["name"];
        playerNameTextDisplay.fontSize = 11;
        playerNameTextDisplay.color = Color.white;
        playerNameText.transform.SetParent(canvasTransform, false);
        playerNameTextDisplay.alignment = TextAlignmentOptions.Center;




        // Player Head Image
        Vector2 playerHeadDisplacement = new Vector2(0, (int)(infoBoxImgSize.y * 0.08f));
        this.playerHead.transform.position = position + playerHeadDisplacement;
        this.playerHeadBg.transform.position = position + playerHeadDisplacement;

        //      Img Sizes
        Vector2 playerHeadImgSize = new Vector2((int)infoBoxImgSize.x * 0.5f, (int)(Screen.height * 0.25f));
        Vector2 playerHeadBgImgSize = new Vector2((int)infoBoxImgSize.x * 0.7f, (int)(Screen.height * 0.38f));

        //      Bg
        Image playerHeadBgImg = this.playerHeadBg.AddComponent<Image>();
        playerHeadBgImg.sprite = this.greenBoxSp;

        RectTransform playerHeadBgImgRectTransform = playerHeadBgImg.GetComponent<RectTransform>();
        playerHeadBgImgRectTransform.sizeDelta = playerHeadBgImgSize;
        playerHeadBgImg.transform.SetParent(canvasTransform, false);

        ////      Head
        Image playerHeadImg = this.playerHead.AddComponent<Image>();
        playerHeadImg.sprite = this.playerSprites["head"];

        RectTransform playerHeadImgRectTransform = playerHeadImg.GetComponent<RectTransform>();
        playerHeadImgRectTransform.sizeDelta = playerHeadImgSize;
        playerHeadImg.transform.SetParent(canvasTransform, false);






        // Left & Right button
        this.playerSwitchRight.transform.position = position + playerHeadDisplacement + new Vector2((int)infoBoxImgSize.x * 0.42f, 0);
        this.playerSwitchLeft.transform.position = position + playerHeadDisplacement + new Vector2((int)infoBoxImgSize.x * -0.42f, 0);
        Button rightButton = this.playerSwitchRight.AddComponent<Button>();
        Button leftButton = this.playerSwitchLeft.AddComponent<Button>();

        //      Agregate method
        rightButton.onClick.AddListener(SwitchPlayerRight);
        leftButton.onClick.AddListener(SwitchPlayerLeft);

        //      Agregate sprite
        Image rightButtonImg = this.playerSwitchRight.AddComponent<Image>();
        Image leftButtonImg = this.playerSwitchLeft.AddComponent<Image>();
        rightButtonImg.sprite = rightButtonSp;
        leftButtonImg.sprite = leftButtonSp;

        //      Change sprite
        Vector2 buttonSize = new Vector2((int)infoBoxImgSize.x * 0.06f, (int)(Screen.height * 0.07f));

        RectTransform rightButtonRectTransform = this.playerSwitchRight.GetComponent<RectTransform>();
        rightButtonRectTransform.sizeDelta = buttonSize;
        rightButton.transform.SetParent(canvasTransform, false);

        RectTransform leftButtonRectTransform = this.playerSwitchLeft.GetComponent<RectTransform>();
        leftButtonRectTransform.sizeDelta = buttonSize;
        leftButton.transform.SetParent(canvasTransform, false);




        // Player Flag and Stars
        Vector2 playerFlagDisplacement = new Vector2(0, (int)(infoBoxImgSize.y * -0.27f));
        this.playerFlag.transform.position = position + playerFlagDisplacement + new Vector2((int)infoBoxImgSize.x * -0.23f, 0);
        this.playerStars.transform.position = position + playerFlagDisplacement + new Vector2((int)infoBoxImgSize.x * 0.16f, 0);

        //      Flag
        Image playerFlagImg = this.playerFlag.AddComponent<Image>();
        playerFlagImg.sprite = this.playerSprites["flag"];

        RectTransform playerFlagImgRectTransform = playerFlagImg.GetComponent<RectTransform>();
        playerFlagImgRectTransform.sizeDelta = new Vector2((int)infoBoxImgSize.x * 0.18f, (int)infoBoxImgSize.y * 0.08f);
        playerFlagImg.transform.SetParent(canvasTransform, false);


        //      Stars
        Image playerStarsImg = this.playerStars.AddComponent<Image>();
        playerStarsImg.sprite = this.playerSprites["stars"];

        RectTransform playerStarsImgRectTransform = playerStarsImg.GetComponent<RectTransform>();
        playerStarsImgRectTransform.sizeDelta = new Vector2((int)infoBoxImgSize.x * 0.45f, (int)infoBoxImgSize.y * 0.06f);
        playerStarsImg.transform.SetParent(canvasTransform, false);




        // Select/Cancel button        
        Vector2 playerSelectDisplacement = new Vector2(0, (int)(infoBoxImgSize.y * -0.4f));
        this.playerSelect.transform.position = position + playerSelectDisplacement;


        //      img
        Image playerSelectImg = this.playerSelect.AddComponent<Image>();
        playerSelectImg.sprite = this.selectButtonSp;

        RectTransform playerSelectImgRectTransform = playerSelectImg.GetComponent<RectTransform>();
        playerSelectImgRectTransform.sizeDelta = new Vector2((int)infoBoxImgSize.x * 0.34f, (int)infoBoxImgSize.y * 0.1f);
        playerSelectImg.transform.SetParent(canvasTransform, false);

        //      button
        Button playerSelectButton = this.playerSelect.AddComponent<Button>();
        playerSelectButton.onClick.AddListener(Select);

    }


    public void SwitchPlayerRight()
    {
        this.selectedPlayer = PlayerResourceManager.ObtainNextUnlockedPlayer(this.selectedPlayer, true);
        UpdateUI();
    }
    public void SwitchPlayerLeft()
    {
        this.selectedPlayer = PlayerResourceManager.ObtainNextUnlockedPlayer(this.selectedPlayer, false);
        UpdateUI();
    }

    public void Select()
    {

        Image playerSelectImg = this.playerSelect.GetComponent<Image>();

        if (this.isPlayerSelected)
        {
            playerSelectImg.sprite = this.selectButtonSp;
        }
        else
        {
            playerSelectImg.sprite = this.cancelButtonSp;
        }

        this.isPlayerSelected = !this.isPlayerSelected;
        UpdateUI();

    }


    public bool IsPlayerSelected
    {
        get
        {
            return this.isPlayerSelected;
        }
    }

    public Dictionary<string, string> PlayerInfo
    {
        get
        {
            return this.playerInfo;
        }
    }

    public Dictionary<string, Sprite> PlayerSprites
    {
        get
        {
            return this.playerSprites;
        }
    }

}
