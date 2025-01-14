using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputArea : MonoBehaviour
{
    public static bool IsTouch = false;
    public static Vector2 InputScreenPosition = Vector2.zero;


    public static Vector2 InputUiPosition 
    {
        get {
            return Camera.main.ScreenToWorldPoint(InputScreenPosition);
        }   

    }


    private void Update()
    {


        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetMouseButton(0))
            {
                IsTouch = true;
                InputScreenPosition = Input.mousePosition;
            }
            else
            {
                IsTouch = false;
            }
        }
        else if(Application.platform == RuntimePlatform.Android | Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0)
            {
                IsTouch = true;
                InputScreenPosition = Input.touches[0].position;
            }
            else
            {
                IsTouch = false;
            }
        }
    }
}
