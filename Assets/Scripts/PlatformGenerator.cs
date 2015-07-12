using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlatformGenerator : MonoBehaviour {

    public Text scoreText;
    public Text gameOverText;
    public Texture blockTexture;

    public GameObject ball;
    public float width;
    public float degrees;

    private float radians;
    private ScoreManager scoreManager;

    private float xPosition;
    private float yPosition;

    private Dictionary<long, GameObject[]> rows = new Dictionary<long, GameObject[]>();
    private long rowNumber = 1;

    private int titleFontSize;
    private int scoreFontSize;

    private bool gameOver = false;
    private float gameOverTime = 0f;

	void Start ()
    {
        radians = degrees * Mathf.Deg2Rad;
        scoreManager = (ScoreManager) ball.GetComponent(typeof(ScoreManager));

        titleFontSize = (int)(((float)Screen.height) * (0.25f));
        scoreFontSize = (int)(((float)Screen.height) * (0.08f));

        xPosition = -2 * (width * Mathf.Cos(radians));
        yPosition = -2 * (width * Mathf.Sin(radians));
	}

    void Update()
    {
        if (rowNumber - scoreManager.getScore() < 30)
        {
            xPosition = xPosition - (width * Mathf.Cos(radians));
            yPosition = yPosition - (width * Mathf.Sin(radians));

            GameObject[] row = createRowOfBlocks();
            rows.Add(rowNumber, row);
            rowNumber++;
        }

        IList rowsToDelete = new ArrayList();

        foreach(long rowKey in rows.Keys){
            if (scoreManager.getScore() - 2 > rowKey)
            {
                rowsToDelete.Add(rowKey);
            }
        }

        foreach (long rowKey in rowsToDelete)
        {
            destroyRowOfBlocks(rowKey);
        }

        if (hasBallFallenOffPlatform())
        {
            scoreText.rectTransform.anchoredPosition = new Vector2((float)Screen.width / 2, (float)Screen.height * 11f / 20f);
            scoreText.rectTransform.sizeDelta = new Vector2((float)Screen.width / 3, (float)Screen.height / 10);
            scoreText.fontSize = scoreFontSize;

            gameOverText.rectTransform.anchoredPosition = new Vector2((float)Screen.width / 2f, (float)Screen.height * 3f / 4f);
            gameOverText.rectTransform.sizeDelta = new Vector2(Screen.width, (float)Screen.height / 3f);
            gameOverText.fontSize = titleFontSize;

            if(!gameOver){
                gameOverTime = Time.time;
                gameOver = true;
            }

            if (Time.time - gameOverTime > 2)
            {
                unlockAchievements();
                updateTopScore();
                Application.LoadLevel("MainMenu");
            }
        }
	}

    public void pressRetryButton()
    {
        Application.LoadLevel("MainMenu");
    }

    private GameObject[] createRowOfBlocks()
    {
        GameObject[] row = new GameObject[5];

        for (int zPosition = 1; zPosition < 6; zPosition++)
        {
            int randomNumber = Random.Range(0, 2);
            if (randomNumber == 1)
            {
                GameObject newBlock = createNewBlock(zPosition);
                row[zPosition - 1] = newBlock;
            }
        }
        return row;
    }

    private GameObject createNewBlock(long zPosition)
    {
        GameObject newBlock = GameObject.CreatePrimitive(PrimitiveType.Cube);
        newBlock.transform.localScale = new Vector3(width, 1, width);
        newBlock.transform.eulerAngles = new Vector3(0, 0, degrees);
        newBlock.GetComponent<Renderer>().material.mainTexture = blockTexture;
        newBlock.tag = "Platform";
        newBlock.transform.position = new Vector3(xPosition, yPosition, zPosition * width);
        newBlock.name = "Block_" + rowNumber + "_row";

        return newBlock;
    }

    private void destroyRowOfBlocks(long rowKey)
    {
        GameObject[] row = new GameObject[5];
        rows.TryGetValue(rowKey, out row);
        rows.Remove(rowKey);

        foreach (GameObject blockToBeDeleted in row)
        {
            if (blockToBeDeleted != null)
            {
                Debug.Log(blockToBeDeleted.name);
                GameObject.Destroy(blockToBeDeleted);
            }
        }
    }

    private bool hasBallFallenOffPlatform()
    {
        if (ball.transform.position.y < yPosition)
        {
            return true;
        }

        return false;
    }

    private void unlockAchievements()
    {
        if (scoreManager.getScore() >= 10)
        {
            Social.ReportProgress("CgkIxdCoi9cSEAIQAQ", 100.0f, (bool success) => { });
        }

        if (scoreManager.getScore() >= 50)
        {
            Social.ReportProgress("CgkIxdCoi9cSEAIQAg", 100.0f, (bool success) => { });
        } 
        
        if (scoreManager.getScore() >= 100)
        {
            Social.ReportProgress("CgkIxdCoi9cSEAIQAw", 100.0f, (bool success) => { });
        }

        if (scoreManager.getScore() >= 200)
        {
            Social.ReportProgress("CgkIxdCoi9cSEAIQBA", 100.0f, (bool success) => { });
        }

        if (scoreManager.getScore() >= 500)
        {
            Social.ReportProgress("CgkIxdCoi9cSEAIQBQ", 100.0f, (bool success) => { });
        }
    }

    private void updateTopScore()
    {
        Social.ReportScore(scoreManager.getScore(), "CgkIxdCoi9cSEAIQAA", (bool success) => { });
    }
}
