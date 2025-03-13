using System.Collections;
using UnityEngine;

public class Manager_Menu : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject helpPanel; // Reference to the HelpPanel

    [SerializeField]
    private AudioSource m_SeAudio;
    [SerializeField]
    private AudioClip AudioClip_Button;

    [SerializeField]
    private float in_degree;
    [SerializeField]
    private float normal_degree;
    [SerializeField]
    private float out_degree;

    private void Update()
    {
        out_degree = normal_degree + (Mathf.DeltaAngle(in_degree + 180f, normal_degree));
    }

    // Toggles the Help Panel visibility
    public void Button_HelpPanel(bool _isActive)
    {
        mainPanel.SetActive(!_isActive);
        helpPanel.SetActive(_isActive);

        PlayButtonSound();
    }

    public void Button_Start()
    {
        if (Manager_Main.Instance != null)
        {
            PlayButtonSound();
            Manager_Main.GoScene(1); // Directly call GoScene if the instance exists
        }
        else
        {
            Debug.LogError("Manager_Main instance is not found. Make sure it exists in the scene.");
        }
    }

    private void PlayButtonSound()
    {
        if (m_SeAudio != null && AudioClip_Button != null)
        {
            m_SeAudio.clip = AudioClip_Button;
            m_SeAudio.Play();
        }
    }
}
