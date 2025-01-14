using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Food : MonoBehaviour
{
    [SerializeField]
    int score = 1;
    [SerializeField]
    float lifeTime = -1;
    [SerializeField]
    bool isBonusFood = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeTime > 0) 
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                Manager_Play.Instance.SetBonusFoodCooldown();
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Object_Snake _snake = collision.GetComponent<Object_Snake>();
        if (_snake)
        {
            Manager_Play.AddScore(score);
            if (isBonusFood) 
            {
                Manager_Play.Instance.SetBonusFoodCooldown();
            }
            _snake.Growth();
            Destroy(gameObject);
        }
    }
}
