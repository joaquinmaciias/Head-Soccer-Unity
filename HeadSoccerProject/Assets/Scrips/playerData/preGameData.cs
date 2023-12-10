using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class PreGameData : ScriptableObject
{
    private static Dictionary<string, string> player1Info;      // ["name", "country"]
    private static Dictionary<string, Sprite> player1Sprites;   // ["head", "flag", "shirt", "boots", "stars"]

    private static Dictionary<string, string> player2Info;
    private static Dictionary<string, Sprite> player2Sprites;

    private static string gameMode;

    // Getters and setters for player1Info
    public static Dictionary<string, string> Player1Info
    {
        get { return player1Info; }
        set { player1Info = value; }
    }

    // Getters and setters for player1Sprites
    public static Dictionary<string, Sprite> Player1Sprites
    {
        get { return player1Sprites; }
        set { player1Sprites = value; }
    }

    // Getters and setters for player2Info
    public static Dictionary<string, string> Player2Info
    {
        get { return player2Info; }
        set { player2Info = value; }
    }

    // Getters and setters for player2Sprites
    public static Dictionary<string, Sprite> Player2Sprites
    {
        get { return player2Sprites; }
        set { player2Sprites = value; }
    }

    // Getters and setters for gameMode
    public static string GameMode
    {
        get { return gameMode; }
        set { gameMode = value; }
    }
}


/*

[CreateAssetMenu]

public class PreGameData : ScriptableObject
{
    public string P1Name;
    public string P2Name;
    public Sprite P1HeadSprite;
    public Sprite P2HeadSprite;
    public Sprite P1BodySprite;
    public Sprite P2BodySprite;
    public Sprite P1FlagSprite;
    public Sprite P2FlagSprite;

    public string gameMode; // "survival", "1vs1", "arcade" -> en caso de querer hacer un torneo, pasarlo como arcade (el juego como tal, es el mismo)

}

*/
