using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class spriteSelection : MonoBehaviour
{
    // Start is called before the first frame update

    // We are going to change: 
    // - Player 1 / Player 2 Sprites -> we need to change body and head
    // - Player Names -> check if AI to add at the end (depedns on mode)
    // - Stadium -> depends on mode
    // - Flags will depend on players

    IDictionary<string, string> playerCountries = new Dictionary<string, string>();
    IDictionary<string, string> playerSprites = new Dictionary<string, string>();

    public GameObject player1;
    public GameObject player2;
    public GameObject stadium;

    public GameObject[] textPlayer1;
    public GameObject[] textPlayer2;

    public GameObject[] flagPlayer1;
    public GameObject[] flagPlayer2;

    public PreGameData preGameData;

    public string spritesBodyPath;
    public string spritesHeadPath;
    public string spritesFlagPath;
    public string spritesStadiumPath;

    void Awake()
    {
        //textPlayer1 = GameObject.FindGameObjectsWithTag("NameP1");
        //textPlayer2 = GameObject.FindGameObjectsWithTag("NameP2");

        player2 = GameObject.FindGameObjectWithTag("Player1");
        player1 = GameObject.FindGameObjectWithTag("Player2");
        stadium = GameObject.FindGameObjectWithTag("stadiumBackground");


        flagPlayer2 = GameObject.FindGameObjectsWithTag("FlagP1");
        flagPlayer1 = GameObject.FindGameObjectsWithTag("FlagP2");



        spritesBodyPath = "body_image/body_";
        spritesHeadPath = "head_image/";
        spritesFlagPath = "flag_image/";
        spritesStadiumPath = "stadiums_image/";

        setSprites();
    }


    void Dicts()
    {

        // Now we add every country and player
        playerCountries.Add("Bale", "wales");
        playerCountries.Add("Ibrahimovic", "sweden");
        playerCountries.Add("De Bruyne", "belgium");
        playerCountries.Add("Rooney", "england");
        playerCountries.Add("Ribery", "france");
        playerCountries.Add("Iniesta", "spain");
        playerCountries.Add("Neymar", "brazil");
        playerCountries.Add("C. Ronaldo", "portugal");
        playerCountries.Add("Messi", "argentina");
        playerCountries.Add("Hazard", "belgium");

        //
        playerCountries.Add("Falcao", "colombia");
        playerCountries.Add("Kane", "england");
        playerCountries.Add("Ramos", "spain");
        playerCountries.Add("Amunike", "nigeria");
        playerCountries.Add("Reus", "germany");
        playerCountries.Add("Salah", "egypt");
        playerCountries.Add("Pogba", "france");
        playerCountries.Add("Griezmann", "france");
        playerCountries.Add("Suarez", "uruguay");
        //playerCountries.Add("Stera", "????");

        //
        playerCountries.Add("Balotelli", "italy");
        playerCountries.Add("Ozil", "germany");
        playerCountries.Add("Benzema", "france");
        playerCountries.Add("Marcelo", "brazil");
        playerCountries.Add("Modric", "croatia");
        playerCountries.Add("Aguero", "argentina");
        playerCountries.Add("Cavani", "uruguay");
        playerCountries.Add("Rashford", "england");
        playerCountries.Add("D. Costa", "spain");
        //playerCountries.Add("Tourist", "???");
        //
        playerCountries.Add("Beckham", "england");
        playerCountries.Add("Cantona", "france");
        playerCountries.Add("Maradona", "argentina");
        playerCountries.Add("Pirlo", "italy");
        playerCountries.Add("Pele", "brazil");
        playerCountries.Add("Ronaldo", "brazil");
        playerCountries.Add("Ronaldinho", "brazil");
        playerCountries.Add("Zidane", "france");

        // Now we add every country and player
        playerSprites.Add("Bale", "4_Galibale");
        playerSprites.Add("Ibrahimovic", "4_Ibraham");
        playerSprites.Add("De Bruyne", "4_K.Debug");
        playerSprites.Add("Rooney", "4_Romi");
        playerSprites.Add("Ribery", "5_Liberty");
        playerSprites.Add("Iniesta", "5_Inestea");
        playerSprites.Add("Neymar", "5_Namec");
        playerSprites.Add("C. Ronaldo", "6_C.Ranodal");
        playerSprites.Add("Messi", "6_L.Massa");
        playerSprites.Add("Hazard", "5_Hala");

        //
        playerSprites.Add("Falcao", "2_Fanceo");
        playerSprites.Add("Kane", "2_Heri Kean");
        playerSprites.Add("Ramos", "2_Romast");
        playerSprites.Add("Amunike", "2_S.Amunike");
        playerSprites.Add("Reus", "3_M.Ruses");
        playerSprites.Add("Salah", "3_M.Sadia");
        playerSprites.Add("Pogba", "3_Poppa");
        playerSprites.Add("Griezmann", "2_Gman");
        playerSprites.Add("Suarez", "3_L.Serzua");
        //playerSprites.Add("Stera", "3_Stera");

        //
        playerSprites.Add("Balotelli", "0_Bolatale");
        playerSprites.Add("Ozil", "0_Mezan Ozol");
        playerSprites.Add("Benzema", "1_Bamzomo");
        playerSprites.Add("Marcelo", "1_Macedono");
        playerSprites.Add("Modric", "2_L.Momo");
        playerSprites.Add("Aguero", "1_Agugiegio");
        playerSprites.Add("Cavani", "0_Canvas");
        playerSprites.Add("Rashford", "0_M.Rofast");
        playerSprites.Add("D. Costa", "1_D.Casto");
        //playerSprites.Add("Tourist", "1_Tourist");
        //
        playerSprites.Add("Beckham", "7_Backeng");
        playerSprites.Add("Cantona", "7_Canoda");
        playerSprites.Add("Maradona", "7_Morado");
        playerSprites.Add("Pirlo", "7_Picol");
        playerSprites.Add("Pele", "7_Pilic");
        playerSprites.Add("Ronaldo", "7_Rodlima");
        playerSprites.Add("Ronaldinho", "7_Ronho");
        playerSprites.Add("Zidane", "7_Zido");


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSprites()
    {
        // We set the players sprites

        Dictionary<string, string> player1Info = PreGameData.Player1Info;
        Dictionary<string, string> player2Info = PreGameData.Player2Info;
        Dictionary<string, Sprite> player1Sprites = PreGameData.Player1Sprites;
        Dictionary<string, Sprite> player2Sprites = PreGameData.Player2Sprites;
        string gameMode = PreGameData.GameMode;

        SpriteRenderer Head1SpriteRenderer = player1.transform.Find("Root").Find("Head").GetComponent<SpriteRenderer>();
        Head1SpriteRenderer.sprite = player1Sprites["head"];

        SpriteRenderer Head2SpriteRenderer = player2.transform.Find("Root").Find("Head").GetComponent<SpriteRenderer>();
        Head2SpriteRenderer.sprite = player2Sprites["head"];

        SpriteRenderer Body1SpriteRenderer = player1.transform.Find("Root").Find("Body").GetComponent<SpriteRenderer>();
        Body1SpriteRenderer.sprite = player1Sprites["shirt"];

        SpriteRenderer Body2SpriteRenderer = player2.transform.Find("Root").Find("Body").GetComponent<SpriteRenderer>();
        Body2SpriteRenderer.sprite = player2Sprites["shirt"];

        SpriteRenderer Shoe1SpriteRenderer = player1.transform.Find("Root").Find("Shoe").GetComponent<SpriteRenderer>();
        Shoe1SpriteRenderer.sprite = player1Sprites["boots"];

        Debug.Log(player1Sprites["boots"]);

        SpriteRenderer Shoe2SpriteRenderer = player2.transform.Find("Root").Find("Shoe").GetComponent<SpriteRenderer>();
        Shoe2SpriteRenderer.sprite = player2Sprites["boots"];

        // Now the set the flags 
        // There is no need to store them as they will not change throughout the game

        foreach (var obj in textPlayer1) 
        {
            if (obj != null)
            {
                var name = obj.GetComponent<TextMeshProUGUI>();
                name.text = player1Info["name"];
            }
        }

        foreach (var obj in textPlayer2)
        {
            if (obj != null)
            {
                var name = obj.GetComponent<TextMeshProUGUI>();

                if (PreGameData.GameMode == "PvP")
                {
                    name.text = player2Info["name"];
                }
                else
                {
                    name.text = player2Info["name"] + " (AI)";
                }
            }
        }

        foreach (var obj in flagPlayer1)
        {
            if (obj != null)
            {
                var flag = obj.transform.GetComponent<SpriteRenderer>();
                flag.sprite = player1Sprites["flag"];
            }
        }

        foreach (var obj in flagPlayer2)
        {
            if (obj != null)
            {
                var flag = obj.transform.GetComponent<SpriteRenderer>();
                flag.sprite = player2Sprites["flag"];
            }
        }


        // We set the stadium sprite

        SpriteRenderer stadiumSpriteRenderer = stadium.GetComponent<SpriteRenderer>();

        if (gameMode == "survival")
        {
            stadiumSpriteRenderer.sprite = Resources.Load<Sprite>(spritesStadiumPath + "stadBg2");

            
        }
        else if (gameMode == "1vs1")
        {
            stadiumSpriteRenderer.sprite = Resources.Load<Sprite>(spritesStadiumPath + "stadBg3");
        }
        else
        {
            
            stadiumSpriteRenderer.sprite = Resources.Load<Sprite>(spritesStadiumPath + "stadBg4");
            
        }
    }
}
