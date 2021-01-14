using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class tableScript : MonoBehaviour
{
    public Transform target;
    public GameObject key;
    public GameObject keyIcon;
    public Animator BigCube;
    public GameObject Cube;
    public GameObject Teleport;
    public GameObject Chest;
    public GameObject KeyChest;
    public GameObject Crystall;
    public GameObject Retry;
    GameObject ChestTop;
    public static tableScript instance;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        ChestTop = Chest.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.isInGround)
        {
            if (Vector3.Distance(target.transform.position, transform.position) <= 4)
            {
                PlayerController.instance.text.color = Color.magenta;
                if (key.activeSelf)
                {
                    PlayerController.instance.Text.GetComponent<RectTransform>().sizeDelta = new Vector2(332, 30);
                    PlayerController.instance.text.text = "Press E to pick up";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        key.SetActive(false);
                        keyIcon.SetActive(true);
                        BigCube.SetBool("Big", true);

                    }
                }
            }
            
            else
            {
                PlayerController.instance.text.text = "";
            }
            if (Vector3.Distance(Cube.transform.position, target.transform.position) <= 13)
            {
                PlayerController.instance.offsetPosition = new Vector3(0, 2, -2.8f);
            }
            else
            {
                PlayerController.instance.offsetPosition = new Vector3(0, 7, -10);
            }
            if (keyIcon.activeSelf)
            {
               PlayerController.instance.text.color = Color.red;
                PlayerController.instance.Text.GetComponent<RectTransform>().sizeDelta = new Vector2(680, 30);
                PlayerController.instance.text.text = "Stand on the teleport maschine";
            }
            if(!keyIcon.activeSelf && Vector3.Distance(target.transform.position, transform.position) > 4)
            {
                PlayerController.instance.text.color = Color.red;
                PlayerController.instance.Text.GetComponent<RectTransform>().sizeDelta = new Vector2(680, 30);
                PlayerController.instance.text.text = "There is a key on the table, take it";
            }
        }
        /*if(Vector3.Distance(Cube.transform.position, target.transform.position) <= 13)
        {
            PlayerController.instance.offsetPosition = new Vector3(0, 2, -2.8f);
        }
        else if(Vector3.Distance(Teleport.transform.position, target.transform.position) <= 31)
        {
            *//*Debug.Log("Teleporti");*//*
            PlayerController.instance.offsetPosition = new Vector3(0, 7f, -10);
            *//*Debug.Log(Vector3.Distance(Teleport.transform.position, target.transform.position));*//*
        }*/
        if (Vector3.Distance(target.transform.position, Chest.transform.position) <= 4)
        {
            PlayerController.instance.Text.GetComponent<RectTransform>().sizeDelta = new Vector2(332, 30);
            PlayerController.instance.text.text = "Press E to open";
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerController.instance.planeAnim.SetBool("Chest Open", true);
                KeyChest.SetActive(true);
                keyIcon.SetActive(false);

            }
            if (PlayerController.instance.planeAnim.GetCurrentAnimatorStateInfo(2).IsName("final"))
            {
                PlayerController.instance.Text.GetComponent<RectTransform>().sizeDelta = new Vector2(630, 30);
                PlayerController.instance.text.text = "Press Q to pick up crystal";
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Crystall.SetActive(false);
                    PlayerController.instance.scr++;
                    PlayerController.instance.Score.GetComponent<Text>().text = "Score: " + PlayerController.instance.scr.ToString() + "/3";
                    if (PlayerController.instance.scr != 3)
                    {
                        Retry.SetActive(true);
                        PlayerController.instance.blackBack.SetActive(true);
                        Debug.Log("mtela");
                    }
                    else
                    {
                        Retry.SetActive(false);
                    }
                        
                }
            }
        }
    } 
        /*else
        {
            *//*PlayerController.instance.text.text = "";*//*
        }*/
        /*else
        {
            PlayerController.instance.offsetPosition = new Vector3(0, 3, -5);
            Debug.Log(Vector3.Distance(Teleport.transform.position, target.transform.position));
        }*/
    //}
}
