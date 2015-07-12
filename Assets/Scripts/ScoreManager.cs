using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ScoreManager : MonoBehaviour {

    public GameObject ScoreText;
    private long score;

	void Start () {
        score = 0;
        ScoreText.GetComponent<Text>().fontSize = (int)(((float)Screen.height) * (0.05f));
        ScoreText.GetComponent<RectTransform>().anchoredPosition = new Vector2((float)Screen.width / 2f, (float)Screen.height * 7f / 8f);
        ScoreText.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, (float)Screen.height / 4f);
	}
	
	void Update () {
        string scoreText = "Score: " + getScore();
        ScoreText.GetComponent<Text>().text = scoreText;
	}

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Platform")
        {
            string[] nameSplit = collisionInfo.collider.name.Split('_');
            score = long.Parse(nameSplit[1]);
        }
    }

    public long getScore()
    {
        return score;
    }
}
