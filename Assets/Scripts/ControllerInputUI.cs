using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class ControllerInputUI : MonoBehaviour
{
    [SerializeField]
    private bool horizontalButtons = false;
    private List<Button> buttons;
    private int currentIndex;
    private bool selectActive = false, buttonClicked = false;
    // Start is called before the first frame update
    void Start()
    {
        buttons = new List<Button>();
        buttons.AddRange(FindObjectsOfType<Button>());
        if (buttons.Count > 0)
        {
            if (horizontalButtons)
            {
                buttons = buttons.OrderByDescending(x => x.transform.localPosition.x).ToList();
            }
            else
            {
                buttons = buttons.OrderByDescending(x => x.transform.localPosition.y).ToList();
            }
            
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
                buttons[currentIndex].transform.localScale = Vector3.one;
                currentIndex++;
            }
            else
            {
                buttons[currentIndex].transform.localScale = Vector3.one;
                currentIndex = 0;
            }
            SelectOption();
        }
        else if (Input.GetAxis("Vertical") > 0 && !selectActive)
        {
            selectActive = true;
            if (currentIndex > 0)
            {
                buttons[currentIndex].transform.localScale = Vector3.one;
                currentIndex--;
            }
            else
            {
                buttons[currentIndex].transform.localScale = Vector3.one;
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
            buttons[currentIndex].transform.localScale = Vector3.one;
            buttons[currentIndex].onClick.Invoke();
            buttons.Clear();
            buttons.AddRange(FindObjectsOfType<Button>());
            if (buttons.Count > 0)
            {
                if (horizontalButtons)
                {
                    buttons = buttons.OrderByDescending(x => x.transform.localPosition.x).ToList();
                }
                else
                {
                    buttons = buttons.OrderByDescending(x => x.transform.localPosition.y).ToList();
                }
                currentIndex = 0;
                
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
        buttons[currentIndex].transform.localScale *= 1.4f;
    }
}
