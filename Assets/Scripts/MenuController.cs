using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class MenuController : MonoBehaviour
{

    public Text titleText;

    public Image startGameButton;
    public Image leaderboardButton;
    public Image customiseBallButton;
    public Image exitGameButton;
    public Image muteButton;

    public GameObject ball;
    public Image leftArrowButton;
    public Image rightArrowButton;
    public Image backButton;

    private int titleFontSize;
    private int buttonFontSize;

    private string[] skins = new string[8];
    private int skinIndex = 0;

    void Start()
    {
        titleFontSize = (int)(((float)Screen.height) * (0.25f));
        buttonFontSize = (int)(((float)Screen.height) * (0.05f));

        PopulateMaterialList();
        SetupMainMenu();
        setSavedSkin();
        renderBallSkin();

        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) => {});

        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
        }
    }

    void Update()
    {

    }

    public void StartGame()
    {
        Application.LoadLevel("Game");
    }

    public void ShowLeaderboard()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIxdCoi9cSEAIQAA");
    }

    public void CustomiseBall()
    {
        startGameButton.rectTransform.anchoredPosition = new Vector2((float)Screen.width * 2, (float)Screen.height * 8f / 18);
        leaderboardButton.rectTransform.anchoredPosition = new Vector2((float)Screen.width * 2, (float)Screen.height * 6f / 18);
        customiseBallButton.rectTransform.anchoredPosition = new Vector2((float)Screen.width * 2, (float)Screen.height * 4f / 18);
        exitGameButton.rectTransform.anchoredPosition = new Vector2((float)Screen.width * 2, (float)Screen.height * 2f / 18);
        muteButton.rectTransform.anchoredPosition = new Vector2((float)Screen.width * 2, (float)Screen.height * 2f / 18);

        leftArrowButton.rectTransform.anchoredPosition = new Vector2((float)Screen.width / 4, (float)Screen.height * 7f / 18);
        leftArrowButton.rectTransform.sizeDelta = new Vector2(Screen.width / 4, (float)Screen.height / 4f);

        rightArrowButton.rectTransform.anchoredPosition = new Vector2((float)Screen.width * 3 / 4, (float)Screen.height * 7f / 18);
        rightArrowButton.rectTransform.sizeDelta = new Vector2(Screen.width / 4, (float)Screen.height / 4f);

        backButton.rectTransform.anchoredPosition = new Vector2((float)Screen.width / 2, (float)Screen.height * 2f / 18);
        backButton.rectTransform.sizeDelta = new Vector2((float)Screen.width / 2, (float)Screen.height / 18);
        backButton.GetComponentInChildren<Text>().fontSize = buttonFontSize;

        ball.transform.position = new Vector3(0, -17, 0);
        Material material = Resources.Load(skins[skinIndex], typeof(Material)) as Material;
        ball.GetComponent<Renderer>().material = material;
    }

    public void PressRightArrow()
    {
        if (skinIndex < skins.Length - 1)
        {
            skinIndex++;
        }
        else
        {
            skinIndex = 0;
        }

        renderBallSkin(); 
    }

    public void PressLeftArrow()
    {
        if (skinIndex > 0)
        {
            skinIndex--;
        }
        else
        {
            skinIndex = skins.Length - 1;
        }

        renderBallSkin(); 
    }

    public void BackToMainMenu()
    {
        leftArrowButton.rectTransform.anchoredPosition = new Vector2((float)Screen.width * 2, (float)Screen.height * 8f / 18);
        rightArrowButton.rectTransform.anchoredPosition = new Vector2((float)Screen.width * 2, (float)Screen.height * 8f / 18);
        backButton.rectTransform.anchoredPosition = new Vector2((float)Screen.width * 2, (float)Screen.height * 8f / 18);
        ball.transform.position = new Vector3(-1000, -17, 0);

        SetupMainMenu();
    }

    public void MuteButtonPressed()
    {
        if (PlayerPrefs.GetInt("muted") == 1)
        {
            PlayerPrefs.SetInt("muted", 0);
            muteButton.sprite = Resources.Load("speaker_on", typeof(Sprite)) as Sprite;
        }
        else
        {
            PlayerPrefs.SetInt("muted", 1);
            muteButton.sprite = Resources.Load("speaker_off", typeof(Sprite)) as Sprite;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void SetupMainMenu()
    {
        titleText.rectTransform.anchoredPosition = new Vector2((float)Screen.width / 2f, (float)Screen.height * 3f / 4f);
        titleText.rectTransform.sizeDelta = new Vector2(Screen.width, (float)Screen.height / 2f);
        titleText.fontSize = titleFontSize;

        startGameButton.rectTransform.anchoredPosition = new Vector2((float)Screen.width / 2, (float)Screen.height * 8f / 18);
        startGameButton.rectTransform.sizeDelta = new Vector2((float)Screen.width / 2, (float)Screen.height / 18);
        startGameButton.GetComponentInChildren<Text>().fontSize = buttonFontSize;

        leaderboardButton.rectTransform.anchoredPosition = new Vector2((float)Screen.width / 2, (float)Screen.height * 6f / 18);
        leaderboardButton.rectTransform.sizeDelta = new Vector2((float)Screen.width / 2, (float)Screen.height / 18f);
        leaderboardButton.GetComponentInChildren<Text>().fontSize = buttonFontSize;

        customiseBallButton.rectTransform.anchoredPosition = new Vector2((float)Screen.width / 2, (float)Screen.height * 4f / 18);
        customiseBallButton.rectTransform.sizeDelta = new Vector2((float)Screen.width / 2, (float)Screen.height / 18);
        customiseBallButton.GetComponentInChildren<Text>().fontSize = buttonFontSize;

        exitGameButton.rectTransform.anchoredPosition = new Vector2((float)Screen.width / 2, (float)Screen.height * 2f / 18);
        exitGameButton.rectTransform.sizeDelta = new Vector2((float)Screen.width / 2, (float)Screen.height / 18);
        exitGameButton.GetComponentInChildren<Text>().fontSize = buttonFontSize;

        muteButton.rectTransform.anchoredPosition = new Vector2((float)Screen.width * 11 / 12, (float)Screen.height * 2.5f / 18);
        muteButton.rectTransform.sizeDelta = new Vector2((float)Screen.height / 10, (float)Screen.height / 10);

        if (PlayerPrefs.GetInt("muted") == 1)
        {
            muteButton.sprite = Resources.Load("speaker_off", typeof(Sprite)) as Sprite;
        }
        else
        {
            muteButton.sprite = Resources.Load("speaker_on", typeof(Sprite)) as Sprite;
        }
    }

    private void PopulateMaterialList()
    {
        skins[0] = "Red";
        skins[1] = "Blue";
        skins[2] = "White";
        skins[3] = "Black";
        skins[4] = "Yellow";
        skins[5] = "Materials/BlueishWall";
        skins[6] = "Carpet 03/Carpet pattern 03";
        skins[7] = "Rough string 01/Rough string pattern 01";
    }

    private void setSavedSkin()
    {
        if (!PlayerPrefs.HasKey("skin"))
        {
            PlayerPrefs.SetString("skin", skins[5]);
            skinIndex = 5;
        }
        else
        {
            skinIndex = System.Array.IndexOf(skins, PlayerPrefs.GetString("skin"));
        }
    }

    private void renderBallSkin()
    {
        Material material = Resources.Load(skins[skinIndex], typeof(Material)) as Material;
        ball.GetComponent<Renderer>().material = material;
        PlayerPrefs.SetString("skin", skins[skinIndex]);
    }
}
