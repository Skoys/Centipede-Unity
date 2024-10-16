using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Script_Pause : MonoBehaviour
{
    [SerializeField] private bool pausePressed = false;
    [SerializeField] private bool pauseAlreadyPressed = false;
    [SerializeField] private bool isInPause;

    [SerializeField] private bool directionalPressed = false;
    [SerializeField] private float directionalInput;

    [SerializeField] private bool enterPressed = false;
    [SerializeField] private bool enterAlreadyPressed = false;

    [SerializeField] private int currentSelection = 0;

    [SerializeField] private GameObject pauseObject;
    [SerializeField] private TextMeshProUGUI[] pauseOptions;

    private void Start()
    {
        pauseObject.SetActive(false);
    }

    void Update()
    {
        if(pausePressed && !pauseAlreadyPressed)
        {
            pauseAlreadyPressed = true;
            if (!isInPause)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
        else if (!pausePressed)
        {
            pauseAlreadyPressed = false;
        }

        if (isInPause && (directionalInput == 1 || directionalInput == -1) && !directionalPressed)
        {
            currentSelection += (int)directionalInput;
            directionalPressed = true;
            if(currentSelection == 0) { currentSelection = 1; }
            if(currentSelection > pauseOptions.Length) {currentSelection = pauseOptions.Length; }
            RefreshPause();
        }
        else if(directionalInput == 0)
        {
            directionalPressed = false;
        }

        if(isInPause && enterPressed && !enterAlreadyPressed)
        {
            enterAlreadyPressed = true;
            if (currentSelection == 1)
            {
                Unpause();
            }
            else if(currentSelection == 2)
            {
                SceneManager.LoadScene(0);
            }
        }
        else if (!enterPressed)
        {
            enterAlreadyPressed = false;
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        isInPause = true;
        currentSelection = 0;
        pauseObject.SetActive(true);
    }

    private void Unpause()
    {
        Time.timeScale = 1;
        isInPause = false;
        pauseObject.SetActive(false);
    }

    private void RefreshPause()
    {
        for (int i = 0; i < pauseOptions.Length; i++)
        {
            if (pauseOptions[i].text.Contains("- "))
            {
                pauseOptions[i].text = pauseOptions[i].text.Remove(0, 2);
            }
            if (currentSelection == i+1)
            {
                pauseOptions[i].text = "- " + pauseOptions[i].text;
            }
        }
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
