using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButtonManager : MonoBehaviour
{
    public GameObject helpPanel; // Reference to the HelpPanel

    // Toggles the Help Panel visibility
    public void ToggleHelpPanel()
    {
        if (helpPanel != null)
        {
            bool isActive = helpPanel.activeSelf;
            helpPanel.SetActive(!isActive); // Show if hidden, hide if shown
        }
    }
}
