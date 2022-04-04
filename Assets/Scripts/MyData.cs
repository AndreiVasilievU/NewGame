using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct MyData
{
    public float health;
    public float positionX;
    public float positionY;
    public float positionZ;
    public string levelName;
    public int score;
    public int lvlScore;

    public int[] coins;
}
