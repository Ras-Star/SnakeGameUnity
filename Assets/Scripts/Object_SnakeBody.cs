using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Object_SnakeBody : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 1f;
    [SerializeField]
    float lerpSpeed = 1f;
    
    public float currentDegree = 0f;

    [SerializeField]
    Transform targetTran;
    [SerializeField]
    float controlDistance = 30f;
    public void SetTarget(Transform _target) 
    {
        targetTran = _target;
        transform.position = targetTran.position;
    }

    [SerializeField]
    bool isBodyDamage = false;
    [SerializeField]
    float protectTime = 1f;
    public void SetBodyDamage(bool _isDamage)
    {
        isBodyDamage = _isDamage;
    }

    [SerializeField]
    RectTransform m_AnimAxis;
    [SerializeField]
    Image m_Image;
    [SerializeField]
    Sprite Sprite_Body;
    [SerializeField]
    Sprite Sprite_Tail;
    public void SetSpriteBody() 
    {
        m_Image.sprite = Sprite_Body;
    }
    public void SetSpriteTail()
    {
        m_Image.sprite = Sprite_Tail;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTran != null)  
        {
            Vector2 _dir = (Vector2)targetTran.position - (Vector2)transform.position;

            float _targetDegree = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
            currentDegree = _targetDegree;//Mathf.LerpAngle(currentDegree, _targetDegree, lerpSpeed * Time.deltaTime);
            m_AnimAxis.localEulerAngles = Vector3.forward * currentDegree;
            if (_dir.magnitude >= controlDistance)
            {
                float _rad = currentDegree * Mathf.Deg2Rad;
                transform.position += new Vector3(Mathf.Cos(_rad), Mathf.Sin(_rad), 0) * moveSpeed * Time.deltaTime;
            }
        }
        if (protectTime > 0) 
        {
            protectTime -= Time.deltaTime; ;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isBodyDamage & protectTime <= 0) 
        {
            if (collision.GetComponent<Object_Snake>())
            {
                Manager_Play.GameOver();
            }
        }
    }
}
