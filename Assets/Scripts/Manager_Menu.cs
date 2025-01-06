using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Menu : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject helpPanel; // Reference to the HelpPanel

    // Toggles the Help Panel visibility
    public void Button_HelpPanel(bool _isActive)
    {
        mainPanel.SetActive(!_isActive);
        helpPanel.SetActive(_isActive);
    }

    public void Button_Start()
    {
        Manager_Main.GoScene(2);
    }
}
