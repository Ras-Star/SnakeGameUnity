using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_Play : MonoBehaviour
{
    static public Manager_Play Instance;

    static bool IsGameOver = false;

    [SerializeField]
    RectTransform Trans_Items;
    [SerializeField]
    GameObject Pannel_GameOver;

    #region Snake
    [SerializeField]
    Transform Point_Origin;
    [SerializeField]
    Object_Snake m_Snake;
    #endregion

    #region Food
    [SerializeField]
    Transform[] SpawnLimits = new Transform[2];
    [SerializeField]
    Object_Food Prefabe_Food;
    [SerializeField]
    Object_Food Prefabe_BonusFood;
    Object_Food currentFood = null;
    Object_Food currentBonusFood = null;
    float bonusFoodCooldown = 10f;
    [SerializeField]
    float bonusFoodCooldown_Base = 10f;
    Object_Food SpawnFood(Object_Food _prefabe)
    {
        Object_Food _food = Instantiate(_prefabe, Trans_Items) as Object_Food;
        float _x, _y;
        do
        {
            _x = Mathf.Lerp(SpawnLimits[0].position.x, SpawnLimits[1].position.x, Random.Range(0f, 1f));
            _y = Mathf.Lerp(SpawnLimits[0].position.y, SpawnLimits[1].position.y, Random.Range(0f, 1f));
            _food.transform.position = new Vector3(_x, _y, 0);
        } while (Vector2.Distance(_food.transform.position, m_Snake.transform.position) <= 1f);
        return _food;
    }
    public void SetBonusFoodCooldown() 
    {
        bonusFoodCooldown = bonusFoodCooldown_Base;
    }
    #endregion

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
        if (!currentFood) 
        {
            currentFood = SpawnFood(Prefabe_Food);
            if (!currentBonusFood & bonusFoodCooldown <= 0) 
            {
                currentBonusFood = SpawnFood(Prefabe_BonusFood);
            }
        }

        if (bonusFoodCooldown > 0)
        {
            bonusFoodCooldown -= Time.deltaTime;
        }

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
        m_Snake.Initialize();
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
