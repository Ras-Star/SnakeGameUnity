using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Food : MonoBehaviour
{
    [SerializeField]
    int score = 1;
    [SerializeField]
    float lifeTime = -1;


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
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Object_Snake>())
        {
            Manager_Play.AddScore(score);
            Destroy(gameObject);
        }
    }
}
