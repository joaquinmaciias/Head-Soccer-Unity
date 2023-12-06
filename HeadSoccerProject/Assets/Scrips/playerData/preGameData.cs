using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class preGameData : ScriptableObject
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
