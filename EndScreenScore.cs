using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenScore : MonoBehaviour {

    private void Start()
    {
        Text thisText = GetComponent<Text>();
        thisText.text = ScoreKeeper.score.ToString();
        ScoreKeeper.ResetPoints();
    }
}
