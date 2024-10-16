using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Script_GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private Script_UI ui;
    [SerializeField] private int place;
    [SerializeField] private int alphPlace;
    private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ ";
    private bool inputName = false;

    [SerializeField] TextMeshProUGUI[] highScoreTMPList;
    [SerializeField] TextMeshProUGUI pressEnter;

    [SerializeField] private bool pausePressed = false;
    [SerializeField] private bool pauseAlreadyPressed = false;
    [SerializeField] private bool isInPause;

    [SerializeField] private bool directionalPressed = false;
    [SerializeField] private float directionalInput;

    [SerializeField] private bool enterPressed = false;
    [SerializeField] private bool enterAlreadyPressed = false;

    public static Script_GameOver instance;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
        ui = Script_UI.instance;
        gameOver.SetActive(false);
    }

    private void Update()
    {
        if (inputName)
        {
            if (pausePressed && !pauseAlreadyPressed)
            {
                if (place == 3)
                {
                    ui.SaveHighScores();
                    Time.timeScale = 1f;
                    SceneManager.LoadScene(0);
                }
                pauseAlreadyPressed = true;
            }
            else if (!pausePressed)
            {
                pauseAlreadyPressed = false;
            }

            if ((directionalInput == 1 || directionalInput == -1) && !directionalPressed && place != 3)
            {
                if(directionalInput == 1)
                {
                    alphPlace += 1;
                    if(alphPlace >= alphabet.Length)
                    {
                        alphPlace = 0;
                    }
                }
                if (directionalInput == -1)
                {
                    alphPlace -= 1;
                    if (alphPlace < 0)
                    {
                        alphPlace = alphabet.Length-1;
                    }
                }
                string pseudo = "";
                for(int i = 0; i < 3; i++)
                {
                    if(i == place)
                    {
                        pseudo += alphabet[alphPlace];
                    }
                    else
                    {
                        pseudo += ui.highscoreList[ui.currentPlace].pseudo[i];
                    }
                }
                ui.highscoreList[ui.currentPlace].pseudo = pseudo;
                directionalPressed = true;
            }
            else if (directionalInput == 0)
            {
                directionalPressed = false;
            }

            if (enterPressed && !enterAlreadyPressed && place != 3)
            {
                place++;
                alphPlace = 27;
                if(place == 3)
                {
                    alphPlace = 27;
                    pressEnter.text = "Press Start to continue..";
                }
                enterAlreadyPressed = true;
            }
            else if (!enterPressed)
            {
                enterAlreadyPressed = false;
            }

            if(ui.currentPlace < 11)
            {
                highScoreTMPList[ui.currentPlace].text = (ui.currentPlace + 1) + " - " + ui.highscoreList[ui.currentPlace].pseudo + " : " + ui.highscoreList[ui.currentPlace].points;
            }
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOver.SetActive(true);

        if(ui.currentPlace == ui.highscoreList.Count && ui.currentPlace < 11)
        {
            ui.highscoreList.Add(new Highscore("___", (int)ui.score));
        }
        else if(ui.currentPlace > 10)
        {
            ui.highscoreList[ui.currentPlace].pseudo = "___";
        }
        else
        {
            if(ui.highscoreList.Count < 10)
            {
                ui.highscoreList.Add(new Highscore("___", 0));
            }
            for (int i = ui.highscoreList.Count - 1; i > ui.currentPlace; i--)
            {
                ui.highscoreList[i] = ui.highscoreList[i - 1];
            }
            ui.highscoreList[ui.currentPlace].pseudo = "___";
            ui.highscoreList[ui.currentPlace].points = (int)ui.score;
        }

        for (int i = 0; i < ui.highscoreList.Count; i++)
        {
            string points = ui.highscoreList[i].points.ToString();
            if (points.Length < 10)
            {
                for (int j = 0; j < 10 - points.Length; j++)
                {
                    points = "0" + points;
                }
            }
            highScoreTMPList[i].text = (i+1) + " - " + ui.highscoreList[i].pseudo + " : " + points;
        }
        if(ui.currentPlace > 10)
        {
            place = 3;
            inputName = true;
            pressEnter.text = "Press Start to continue..";
            return;
        }
        inputName = true;
        place = 0;
        alphPlace = 27;
    }

    public void Pause(InputAction.CallbackContext context)
    {
        pausePressed = context.ReadValue<float>() > 0 ? true : false;
    }

    public void Directional(InputAction.CallbackContext context)
    {
        directionalInput = context.ReadValue<float>();
    }

    public void Enter(InputAction.CallbackContext context)
    {
        enterPressed = context.ReadValue<float>() > 0 ? true : false;
    }
}
