using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Script_UI : MonoBehaviour
{
    [Header("Get Components")]
    [SerializeField] private TextMeshProUGUI uiHighScore;
    [SerializeField] private TextMeshProUGUI uiScore;
    [SerializeField] private TextMeshProUGUI uiPlayerLife;
    [SerializeField] private Slider uiPlayerPower;
    [SerializeField] private GameObject uiPowerFull;

    [Header("Variables")]
    [SerializeField] private float score;
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
        uiPlayerPower.maxValue = maxPower;
        uiHighScore.text = highscores[0].element[1];
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
            uiPowerFull.GetComponent<TextMeshProUGUI>().text = "full";
        }
        else
        {
            uiPowerFull.GetComponent<TextMeshProUGUI>().text = "";
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
        if(score >  int.Parse(uiHighScore.text))
        {
            uiHighScore.text = uiScore.text;
            highscores[0].element[1] = uiScore.text;
        }
    }

    private void UpdateHighScore()
    {
        for(int i = highscores.Count - 1 ; i > 0 ; i--)
        {
            highscores[i].element[0] = highscores[i-1].element[0];
            highscores[i].element[1] = highscores[i - 1].element[1];
        }
    }
}
