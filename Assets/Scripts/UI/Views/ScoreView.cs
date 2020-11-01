using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreView : AbstractText
{
    public enum ScoreType
    {

    }

    public void UpdateScore()
    {
        text.text = PlayerPrefs.GetInt("Score", 0).ToString();
    }
}
