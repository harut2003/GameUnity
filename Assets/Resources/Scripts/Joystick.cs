using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public Vector2 direction;
    bool touchstart;
    public GameObject ThresholdRight;
    public GameObject ThresholdLeft;
    public GameObject TouchRight;
    public GameObject TouchLeft;
    enum ScreenPart { left, right };
    ScreenPart screenPart;
    bool firstTouch;
    public static bool OtherTouch;
    Touch touch;


    public static Joystick instance;
    private void Awake()
    {
        instance = this;
        
        ThresholdRight = GameObject.Find("ThresholdRight");
        ThresholdLeft = GameObject.Find("ThresholdLeft");
        TouchLeft = GameObject.Find("TouchLeft");
        TouchRight = GameObject.Find("TouchRight");
    }
    void JoysticMovement(GameObject Threshold, GameObject TouchUI, Touch touch)
    {

        if (touch.phase == TouchPhase.Began)
        {
            pointA = touch.position;
            Threshold.transform.position = pointA;
            Threshold.SetActive(true);
            TouchUI.SetActive(true);
            
        }


        if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary)
        {
            pointB = touch.position;
            touchstart = true;

        }
        else if (touch.phase == TouchPhase.Ended)
        {
            touchstart = false;
            Threshold.SetActive(false);
            TouchUI.SetActive(false);
            direction = Vector2.zero;
        }
        if (touchstart)
        {
            Vector2 offset = pointB - pointA;
            direction = Vector2.ClampMagnitude(offset, 50);
            TouchUI.transform.position = new Vector2(direction.x + pointA.x, direction.y + pointA.y);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void PlaybtnClicked()
    {
        Debug.Log("mtav");
    }
        // Update is called once per frame
    void Update()
    {
        
    }
}
