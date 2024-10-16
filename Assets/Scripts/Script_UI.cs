using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Script_UI : MonoBehaviour
{
    [Header("HighScore")]
    public int currentPlace = 11;
    public List<Highscore> highscoreList = new List<Highscore>();
    [SerializeField] int maxLen = 10;
    [SerializeField] string filename;
    [SerializeField] bool save = false;

    [Header("Get Components")]
    [SerializeField] private TextMeshProUGUI uiRoundNbr;
    [SerializeField] private TextMeshProUGUI uiHighScore;
    [SerializeField] private TextMeshProUGUI uiScore;
    [SerializeField] private TextMeshProUGUI uiPlayerLife;
    [SerializeField] private Slider uiPlayerPower;
    [SerializeField] private TextMeshProUGUI uiPowerFull;

    [Header("Variables")]
    [SerializeField] private int roundNbr;
    public float score;
    [SerializeField] private float multiplicator = 1.0f;
    [SerializeField] private int playerLife;
    [SerializeField] private int playerPower;
    private int maxPower = 50;
    [SerializeField] private List<UIScoreClass> highscores;

    [System.Serializable]
    public class UIScoreClass
    {
        public string[] element;
    }

    static public Script_UI instance;
    private void Awake()
    {
        if(instance == null) { instance = this; }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
        LoadHighScores();
        uiPlayerPower.maxValue = maxPower;
        uiHighScore.text = highscoreList[0].points.ToString();
    }

    private void Update()
    {
        if(multiplicator > 1)
        {
            multiplicator -= Time.deltaTime;
        }
        if(multiplicator < 1)
        {
            multiplicator = 1;
        }
    }

    public void UpdateLife(int nbr)
    {
        playerLife = nbr;
        uiPlayerLife.text = "";
        for(int i = 0; i < playerLife; i++)
        {
            uiPlayerLife.text += "♦";
        }
    }

    public void UpdatePower(int nbr)
    {
        playerPower = nbr;
        uiPlayerPower.value = playerPower;
        if (playerPower == maxPower)
        {
            uiPowerFull.text = "full";
        }
        else
        {
            uiPowerFull.text = "";
        }
        
    }

    public void UpdateScore(int nbr)
    {
        score += Mathf.CeilToInt(nbr * multiplicator);
        multiplicator += 1;
        uiScore.text = score.ToString();
        for (int i = 0;i < 10 - uiScore.text.Length; i++)
        {
            uiScore.text = "0" + uiScore.text;
        }
        UpdateHighScore();
    }

    private void UpdateHighScore()
    {
        if(highscoreList.Count < 10)
        {
            currentPlace = highscoreList.Count;
        }
        for (int i = 0; i < highscoreList.Count; i++)
        {
            if (score > highscoreList[i].points && currentPlace > i)
            {
                currentPlace = i;
            }
        }
    }

    public void UpdateRound(int nbr)
    {
        roundNbr = nbr;
        uiRoundNbr.text = roundNbr.ToString();
    }

    public void LoadHighScores()
    {
        highscoreList = JSON.ReadFromJSON<Highscore>(filename);

        while(highscoreList.Count > maxLen)
        {
            highscoreList.RemoveAt(maxLen);
        }
    }

    public void SaveHighScores()
    {
        JSON.SaveToJSON<Highscore>(highscoreList, filename);
    }
}
