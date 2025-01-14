using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Object_Snake : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 1f;
    [SerializeField]
    float lerpSpeed = 1f;
    
    public float currentDegree = 0f;

    bool isTarget = false;
    Vector2 targetPosition;

    [SerializeField]
    RectTransform m_AnimAxis;
    [SerializeField]
    Object_SnakeBody[] m_Bodys = new Object_SnakeBody[0];
    [SerializeField]
    Object_SnakeBody Prefabe_Body;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (InputArea.IsTouch)
        {
            isTarget = true;
            targetPosition = InputArea.InputUiPosition;
        }
        else
        {
            isTarget = false;
        }

        if (isTarget) 
        {
            Vector2 _dir = targetPosition - (Vector2)transform.position;
            float _targetDegree = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
            currentDegree = Mathf.LerpAngle(currentDegree, _targetDegree, lerpSpeed * Time.deltaTime);
            m_AnimAxis.localEulerAngles = Vector3.forward * currentDegree;
        }

        float _rad = currentDegree * Mathf.Deg2Rad;
        transform.position += new Vector3(Mathf.Cos(_rad), Mathf.Sin(_rad), 0) * moveSpeed * Time.deltaTime;
    }

    public void Initialize()
    {
        currentDegree = 0;
        foreach (Object_SnakeBody body in m_Bodys) 
        {
            if (body)
            {
                Destroy(body.gameObject);
            }
        }
        m_Bodys = new Object_SnakeBody[0];

        for (int i = 0; i < 9; i++)
        {
            Growth();
        }
    }

    public void Growth() 
    {
        Object_SnakeBody[] _newBodys = new Object_SnakeBody[m_Bodys.Length + 1];
        for (int i = 0; i < _newBodys.Length; i++) 
        {
            if (i < m_Bodys.Length)
            {
                _newBodys[i] = m_Bodys[i];
                if (i < m_Bodys.Length - 1) _newBodys[i].SetSpriteBody();
            }
            else
            {
                _newBodys[i] = Instantiate(Prefabe_Body, transform.parent);
                _newBodys[i].SetTarget((i == 0) ? transform : _newBodys[i - 1].transform);
                _newBodys[i].SetBodyDamage(i >= 2);
                _newBodys[i].SetSpriteTail();
            }
        }
        m_Bodys = _newBodys;
    }
}
