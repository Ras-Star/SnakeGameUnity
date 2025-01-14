using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Object_Snake : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 1f;
    [SerializeField]
    float lerpSpeed = 1f;
    
    float currentDegree = 0f;

    bool isTarget = false;
    Vector2 targetPosition;

    [SerializeField]
    RectTransform m_AnimAxis;

    
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

}
