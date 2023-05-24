using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreBoard : MonoBehaviour
{
    int score;
    TMP_Text scoreText;

    public void IncreaseScore(int enemyPoints)
    {
        score += enemyPoints;
        scoreText.text = score.ToString();
    }

    // Start is called before the first frame update
    private void Start()
    {

        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "0000000000";
    }

    // Update is called once per frame
    private void Update()
    {

    }
}
