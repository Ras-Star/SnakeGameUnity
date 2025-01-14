using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_Play : MonoBehaviour
{
    static public Manager_Play Instance;

    static bool IsGameOver = false;

    [SerializeField]
    Object_Snake m_Snake;
    [SerializeField]
    Transform Point_Origin;
    [SerializeField]
    GameObject Pannel_GameOver;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError($"{this} should be singleton.");
            Destroy(gameObject);
        }
    }

    void OnDestroy() 
    {
        if (Instance == this) 
        {
            Instance = null;
        }
    }

    [SerializeField]
    Text Text_Score;

    static float score = 0;

    // Start is called before the first frame update
    void Start()
    {
        Button_Restart();
    }

    // Update is called once per frame
    void Update()
    {
        Text_Score.text = $"Score: {score}";
    }
    public void Button_Back()
    {
        Time.timeScale = 1;
        Manager_Main.GoScene(1);
    }
    public void Button_Pause() 
    {
        if (IsGameOver)
        {
            Time.timeScale = 0;
            return;
        }
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public void Button_Restart() 
    {
        IsGameOver = false;
        Pannel_GameOver.SetActive(false);
        score = 0;
        Time.timeScale = 1;
        m_Snake.transform.position = Point_Origin.transform.position;
    }
    public static void GameOver()
    {
        IsGameOver = true;
        Instance.Pannel_GameOver.SetActive(true);
        Time.timeScale = 0;
    }

    public static void AddScore(int _addValue) 
    {
        score += _addValue;
    }
}
