using System.Collections;
using System.Collections.Generic;
using System.IO;
//using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerResourceManager")]
public class PlayerResourceManager : ScriptableObject
{

    private static Dictionary<int, bool> availablePlayers = new Dictionary<int, bool>
    {
        {1, true },
        {2, true},
        {3, true},
        {4, true},
        {5, true},
        {6, true},
        {7, true},
        {8, true},
        {9, true},
        {10, true},
    };

    // Crear el diccionario que contiene la infrormación de los jugadores
    private static Dictionary<int, Dictionary<string, object>> PlayerInfo = new Dictionary<int, Dictionary<string, object>>
        {
            {
                1, new Dictionary<string, object>
                {
                    { "name", "Iniesta" },
                    { "spritePath", "Player/head/League A/5_Inestea" },
                    { "country", "Spain" },
                    { "stars",  "4"},
                    { "bootsID", 1}
                }
            },
            {
                2, new Dictionary<string, object>
                {
                    { "name", "C.Ronaldo" },
                    { "spritePath", "Player/head/League A/6_C.Ranodal" },
                    { "country", "Portugal" },
                    { "stars",  "5"},
                    { "bootsID", 1}
                }
            },

            {
                3, new Dictionary<string, object>
                {
                    { "name", "Hazard" },
                    { "spritePath", "Player/head/League A/5_Hala" },
                    { "country", "Belgium" },
                    { "stars",  "3.5"},
                    { "bootsID", 1}
                }
            },

            {
                4, new Dictionary<string, object>
                {
                    { "name", "Ribery" },
                    { "spritePath", "Player/head/League A/5_Liberty" },
                    { "country", "France" },
                    { "stars",  "4"},
                    { "bootsID", 1}
                }
            },

            {
                5, new Dictionary<string, object>
                {
                    { "name", "Bale" },
                    { "spritePath", "Player/head/League A/4_Galibale" },
                    { "country", "Wales" },
                    { "stars",  "4"},
                    { "bootsID", 1}
                }
            },
            {
                6, new Dictionary<string, object>
                {
                    { "name", "De Bruyne" },
                    { "spritePath", "Player/head/League A/4_K.Debug" },
                    { "country", "Belgium" },
                    { "stars",  "4"},
                    { "bootsID", 1}
                }
            },

            {
                7, new Dictionary<string, object>
                {
                    { "name", "Messi" },
                    { "spritePath", "Player/head/League A/6_L.Massa" },
                    { "country", "Argentina" },
                    { "stars",  "5"},
                    { "bootsID", 1}
                }
            },

            {
                8, new Dictionary<string, object>
                {
                    { "name", "Neymar" },
                    { "spritePath", "Player/head/League A/5_Namec" },
                    { "country", "Brazil" },
                    { "stars",  "4.5"},
                    { "bootsID", 1}
                }
            },

            {
                9, new Dictionary<string, object>
                {
                    { "name", "Rooney" },
                    { "spritePath", "Player/head/League A/4_Romi" },
                    { "country", "England" },
                    { "stars",  "5"},
                    { "bootsID", 1}
                }
            },

            {
                10, new Dictionary<string, object>
                {
                    { "name", "Ibrahimovic" },
                    { "spritePath", "Player/head/League A/4_Ibraham" },
                    { "country", "Sweden" },
                    { "stars",  "4"},
                    { "bootsID", 1}
                }
            },

        };



    private static Dictionary<string, string> UIinfo = new Dictionary<string, string>
        {
            // Main Menu
            {"MainMenuBackground", "path" },
            {"ButtonArcade", "path"},
            {"ButtonSurvival", "path" },
            {"ButtonPvP", "path"},

            // Select Player Menu
            {"SelectMenuBackground", "Menu/wallpapers/select_player_bg"},
            {"GreenBox", "Menu/extra/greenpack"},
            {"InfoBox", "Menu/extra/infobox"},
            {"TopTitleBox", "Menu/extra/toptitle"},
            {"LevelBox", "Menu/extra/lv_box"},


            // Generic Buttons
            {"ButtonBack", "Menu/buttons/btnBack"},
            {"ButtonCancel", "Menu/buttons/btnCancel 1"},
            {"ButtonCancelSquared", "Menu/buttons/btnCancel" },
            {"ButtonContinue", "Menu/buttons/btnContinue"},
            {"ButtonExit", "Menu/buttons/btnExit"},
            {"ButtonMenu", "Menu/buttons/btnMenu"},
            {"ButtonNext", "Menu/buttons/btnNext" },
            {"ButtonReplayGreen", "Menu/buttons/btnReplay"},
            {"ButtonReplayYellow", "Menu/buttons/btnReplay 1"},
            {"ButtonSelect", "Menu/buttons/btnSelect"},
            {"ButtonStart", "Menu/buttons/btnStart"},
            {"ButtonLeft", "Menu/buttons/left" },
            {"ButtonRight", "Menu/buttons/right"},

            // Text Messages
            {"DrawMessage", "Menu/extra/draw"},
            {"LossMessage", "Menu/extra/Not-Good!"},
            {"VictoryMessage", "Menu/extra/Well-Done!"},

            // Stars
            {"1.5", "Menu/extra/1.5star"},
            {"2",   "Menu/extra/2star"},
            {"2.5", "Menu/extra/2.5star"},
            {"3",   "Menu/extra/3star"},
            {"3.5", "Menu/extra/3.5star"},
            {"4",   "Menu/extra/4star"},
            {"4.5", "Menu/extra/4.5star"},
            {"5",   "Menu/extra/5star"},

        };

    private static Dictionary<int, string> bootsInfo = new Dictionary<int, string>
        {
            {1, "Player/boots/shoe1" },
            {2, "Player/boots/shoe2" },
            {3, "Player/boots/shoe3" },
            {4, "Player/boots/shoe4" },
        };


    private static Dictionary<string, Dictionary<string, string>> countryInfo = new Dictionary<string, Dictionary<string, string>>
        {
            {
                "Argentina", new Dictionary<string, string>
                {
                    { "flag", "Player/flag/argentina" },
                    { "shirt", "Player/shirt/body_arghen" },
                }
            },
            {
                "Belgium", new Dictionary<string, string>
                {
                    { "flag", "Player/flag/belgium" },
                    { "shirt", "Player/shirt/body_belgium" },
                }
            },
            {
                "Brazil", new Dictionary<string, string>
                {
                    { "flag", "Player/flag/brazil" },
                    { "shirt", "Player/shirt/body_brazil" },
                }
            },
            {
                "Colombia", new Dictionary<string, string>
                {
                    { "flag", "Player/flag/colombia" },
                    { "shirt", "Player/shirt/body_colombia" },
                }
            },
            {
                "Croatia", new Dictionary<string, string>
                {
                    { "flag", "Player/flag/croatia" },
                    { "shirt", "Player/shirt/body_croatia" },
                }
            },
            {
                "Egypt", new Dictionary<string, string>
                {
                    { "flag", "Player/flag/egypt" },
                    { "shirt", "Player/shirt/body_egypt" },
                }
            },
            {
                "England", new Dictionary<string, string>
                {
                    { "flag", "Player/flag/england" },
                    { "shirt", "Player/shirt/body_england" },
                }
            },
            {
                "France", new Dictionary<string, string>
                {
                    { "flag", "Player/flag/france" },
                    { "shirt", "Player/shirt/body_france" },
                }
            },
            {
                "Germany", new Dictionary<string, string>
                {
                    { "flag", "Player/flag/germany" },
                    { "shirt", "Player/shirt/body_germany" },
                }
            },
            {
                "Italy", new Dictionary<string, string>
                {
                    { "flag", "Player/flag/italy" },
                    { "shirt", "Player/shirt/body_italy" },
                }
            },
            {
                "Nigeria", new Dictionary<string, string>
                {
                    { "flag", "Player/flag/nigeria" },
                    { "shirt", "Player/shirt/body_nigieria" },
                }
            },
            {
                "Portugal", new Dictionary<string, string>
                {
                    { "flag", "Player/flag/portugal" },
                    { "shirt", "Player/shirt/body_portugal" },
                }
            },
            {
                "Spain", new Dictionary<string, string>
                {
                    { "flag", "Player/flag/spain" },
                    { "shirt", "Player/shirt/body_spain" },
                }
            },
            {
                "Sweden", new Dictionary<string, string>
                {
                    { "flag", "Player/flag/sweden" },
                    { "shirt", "Player/shirt/body_sweden" },
                }
            },
            {
                "Uruguay", new Dictionary<string, string>
                {
                    { "flag", "Player/flag/uruguay" },
                    { "shirt", "Player/shirt/body_urugoay" },
                }
            },
            {
                "Wales", new Dictionary<string, string>
                {
                    { "flag", "Player/flag/wales" },
                    { "shirt", "Player/shirt/body_wale" },
                }
            },
        };





    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static Sprite LoadUI(string spriteName)
    {
        string spritePath = UIinfo[spriteName];
        Sprite requiredSprite = Resources.Load<Sprite>(spritePath);

        return requiredSprite;
    }

    public static Dictionary<string, string> LoadPlayerInfo(int playerID)
    {

        string playerName = PlayerInfo[playerID]["name"].ToString();
        string playerCountrry = PlayerInfo[playerID]["country"].ToString();

        Dictionary<string, string> playerStringInfo = new Dictionary<string, string>
        {
            {"name", playerName },
            {"country", playerCountrry},
        };

        return playerStringInfo;
    }

    public static Dictionary<string, Sprite> LoadPlayerSprites(int playerID)
    {
        // Load Player Head
        string playerHeadPath = PlayerInfo[playerID]["spritePath"].ToString();
        Debug.Log(playerHeadPath);
        Sprite playerHeadSprite = Resources.Load<Sprite>(playerHeadPath);

        // Load Player Country [Flag & Shirt]
        //      Get the country
        string playerCountry = PlayerInfo[playerID]["country"].ToString();
        //      Get the paths
        string playerCountryFlagPath = countryInfo[playerCountry]["flag"];
        string playerCountryShirtPath = countryInfo[playerCountry]["shirt"];
        //      Load the Sprites
        Sprite playerCountryFlagSprite = Resources.Load<Sprite>(playerCountryFlagPath);
        Sprite playerCountryShirtSprite = Resources.Load<Sprite>(playerCountryShirtPath);

        // Load Player Boots
        int playerBootsID = (int)PlayerInfo[playerID]["bootsID"];
        string playerBootsPath = bootsInfo[playerBootsID];
        Sprite playerBootsSprite = Resources.Load<Sprite>(playerBootsPath);

        // Load Player Stars
        string playerStars = PlayerInfo[playerID]["stars"].ToString();
        Sprite playerStarsSprite = LoadUI(playerStars);

        Dictionary<string, Sprite> playerSprites = new Dictionary<string, Sprite>
        {
            {"head", playerHeadSprite},
            {"flag", playerCountryFlagSprite},
            {"shirt", playerCountryShirtSprite},
            {"boots", playerBootsSprite},
            {"stars", playerStarsSprite }
        };

        return playerSprites;
    }

    public static bool CheckPlayerAvailability(int playerID)
    {
        bool isAvailable = availablePlayers[playerID];
        return isAvailable;
    }

    public static void unlockPlayer(int playerID)
    {
        availablePlayers[playerID] = true;
    }

    public static void lockPlayer(int playerID)
    {
        availablePlayers[playerID] = false;
    }

    public static int ObtainNextUnlockedPlayer(int initialID, bool goForward)
    {
        int currentID = initialID;
        int step;
        if (goForward) { step = 1; } else { step = -1; }


        while (true)
        {
            if (currentID + step >= availablePlayers.Count)
            {
                currentID = 0;
            }
            else if (currentID + step <= 1)
            {
                currentID = availablePlayers.Count + 1;
            }

            currentID += step;

            if (availablePlayers[currentID])
            {
                return currentID;
            }


        }
    }

    public static int GetNumberOfAvailablePlayers()
    {
        return availablePlayers.Count;
    }
}
