using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Script_MainMenu : MonoBehaviour
{
    [SerializeField] private bool directionalPressed = false;
    [SerializeField] private float directionalInput;

    [SerializeField] private bool enterPressed = false;
    [SerializeField] private bool enterAlreadyPressed = false;

    [SerializeField] private int currentSelection = 0;

    [SerializeField] private TextMeshProUGUI[] menuOptions;

    void Update()
    {
        if ((directionalInput == 1 || directionalInput == -1) && !directionalPressed)
        {
            currentSelection += (int)directionalInput;
            directionalPressed = true;
            if (currentSelection == 0) { currentSelection = 1; }
            if (currentSelection > menuOptions.Length) { currentSelection = menuOptions.Length; }
            RefreshMenu();
        }
        else if (directionalInput == 0)
        {
            directionalPressed = false;
        }

        if (enterPressed && !enterAlreadyPressed)
        {
            enterAlreadyPressed = true;
            if (currentSelection == 1)
            {
                SceneManager.LoadScene(1);
            }
            else if(currentSelection == 2)
            {
                Application.Quit();
            }
        }
        else if (!enterPressed)
        {
            enterAlreadyPressed = false;
        }
    }

    private void RefreshMenu()
    {
        for (int i = 0; i < menuOptions.Length; i++)
        {
            if (menuOptions[i].text.Contains("- "))
            {
                menuOptions[i].text = menuOptions[i].text.Remove(0, 2);
            }
            if (currentSelection == i + 1)
            {
                menuOptions[i].text = "- " + menuOptions[i].text;
            }
        }
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
