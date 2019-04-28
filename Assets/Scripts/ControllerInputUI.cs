using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControllerInputUI : MonoBehaviour
{
    private List<Button> buttons;
    private int currentIndex;
    private bool selectActive, buttonClicked;
    // Start is called before the first frame update
    void Start()
    {
        buttons = new List<Button>();
        buttons.AddRange(FindObjectsOfType<Button>());
        if (buttons.Count > 0)
        {
            currentIndex = 0;
            SelectOption();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") < 0 && !selectActive)
        {
            selectActive = true;
            if (currentIndex < buttons.Count - 1)
            {
                currentIndex++;
            }
            else
            {
                currentIndex = 0;
            }
            SelectOption();
        }
        else if (Input.GetAxis("Vertical") > 0 && !selectActive)
        {
            selectActive = true;
            if (currentIndex > 0)
            {
                currentIndex--;
            }
            else
            {
                currentIndex = buttons.Count - 1;
            }
            SelectOption();
        }
        else if (Input.GetAxis("Vertical") == 0)
        {
            selectActive = false;
        }
        if (Input.GetAxis("Fire1") > 0 && !buttonClicked)
        {
            buttonClicked = true;
            buttons[currentIndex].onClick.Invoke();
            buttons.Clear();
            buttons.AddRange(FindObjectsOfType<Button>());
            if (buttons.Count > 0)
            {
                if (currentIndex > buttons.Count - 1)
                {
                    currentIndex = 0;
                }
                SelectOption();
            }
        }
        else if (Input.GetAxis("Fire1") == 0)
        {
            buttonClicked = false;
        }
    }
    private void SelectOption()
    {
        Debug.Log($"{buttons[currentIndex].name} is selected");
        buttons[currentIndex].Select();
    }
}
