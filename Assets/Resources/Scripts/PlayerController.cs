using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float horizontal_Intesity=100;
    public float vertical_Intesity=100;
    float horizontal_move;
    float vertical_move;
    bool Jump;
    public GameObject mainCam;
    public GameObject Score;
    public GameObject Reset;
    public GameObject Win;
    
    public GameObject blackBack;
    public GameObject nextLev;
    public GameObject white;
    int liv;
    public int scr;
    public GameObject Text;
    float currentRot;
    public Animator Canvas;
    public Vector3 pointA;
    public Vector3 pointB;
    public GameObject pause;
    public Animator freMaleAnim;
    public GameObject[] lives;
    int h = 0;
    public Animator planeAnim;
    [SerializeField]
    private Transform target;
    [SerializeField]
    public Vector3 offsetPosition;
    public static PlayerController instance;
    /*public Animator Teleport;*/
    public GameObject LightForTele; 
    public GameObject LightForTeleDown;
    public GameObject Table;
    bool teleDwbl;
    [SerializeField]
    private Space offsetPositionSpace = Space.Self;
    public Transform startMarker;
    public Transform endMarker;
    [SerializeField]
    private bool lookAt = true;
    public bool isInGround;
    public Text text;


    // Movement speed in units per second.
    public float speed = 1.0f;

    // Time when the movement started.
    private float startTime;
    public void camFolow()
    {
        
        
        // compute position
        if (offsetPositionSpace == Space.Self)
        {
            mainCam.transform.position = target.TransformPoint(offsetPosition);
        }
        else
        {
            mainCam.transform.position = target.position + offsetPosition;
        }

        // compute rotation
        if (lookAt)
        {
            mainCam.transform.LookAt(target);
        }
        else
        {
            mainCam.transform.rotation = target.rotation;
        }
    }
    private void Awake()
    {
        instance = this;
        mainCam = GameObject.Find("Camera");
        Score = GameObject.Find("Score");
/*        GameObject.Find("Button").GetComponent<Button>().onClick.AddListener(JumpButton);*/
        Reset.SetActive(false);
        pause.SetActive(false);
    }
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Level 3")
        {
            text = Text.GetComponent<Text>();
        }
        startTime = Time.time;
        freMaleAnim = GetComponent<Animator>();
        // Calculate the journey length.
        
        blackBack.SetActive(false);
        nextLev.SetActive(false);
        scr = 0;
        Score.GetComponent<Text>().text = "Score: " + scr.ToString() + "/3";
    }
    public void pauseButton()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        blackBack.SetActive(true);
          pause.SetActive(true);
        if(sceneName == "Level 3")
        {
            planeAnim.speed = 0;
        }
    }
    public void Resume()
    {
            blackBack.SetActive(false);
            pause.SetActive(false);
    }
    public void nextLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if(sceneName == "Level 1")
        {
            SceneManager.LoadScene("Level 2");
        }
        if (sceneName == "Level 2")
        {
            SceneManager.LoadScene("Level 3");
        }
    }
   
    public void Change()
    {
        gameObject.SetActive(false);
    }
    public void Resett()
    {

        SceneManager.LoadScene("Level 1");
    }
    public void ResetNextLev()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        SceneManager.LoadScene(sceneName);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        
        if (pause.activeSelf)
        {
            blackBack.GetComponent<Button>().enabled = true;
            transform.Translate(0, 0, 0);
            transform.Rotate(0, 0, 0);
            if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Space))
            {
                Resume();
            }
        }
        /*if (planeAnim.GetCurrentAnimatorStateInfo(1).IsName("Teleport"))
        {
            transform.Translate(0, 0, 0);
            transform.Rotate(0, 0, 0);
            transform.position = new Vector3(0, 0, 0);
            offsetPosition = new Vector3(0, 7, -10);
        }*/
        else
        {

            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;
            // Distance moved equals elapsed time times speed..
            // Fraction of journey completed equals current distance divided by total distance.
            if(sceneName == "Level 3")
            {
                planeAnim.speed = 1;
                if (planeAnim.GetCurrentAnimatorStateInfo(1).IsName("Teleport"))
                {
                    transform.position = Vector3.Lerp(startMarker.position, endMarker.position, 0.25f * Time.deltaTime * 5);
                }
                else if (planeAnim.GetCurrentAnimatorStateInfo(1).IsName("Teleport Down"))
                {
                    transform.position = new Vector3(2.900395f, 9.63f, 4.992777f);
                    planeAnim.SetBool("teleport", false);
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    offsetPosition = new Vector3(0, 3, 7);
                    LightForTeleDown.SetActive(true);
                }
            }
            
            
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                pauseButton();
            }
            
            
            blackBack.GetComponent<Button>().enabled = false;
            camFolow();
            /*mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, transform.position + Offset, Time.deltaTime * 3 );*/
            vertical_move = Input.GetAxis("Vertical") * vertical_Intesity;
            horizontal_move = Input.GetAxis("Horizontal") * horizontal_Intesity;

            transform.Translate(0, 0, vertical_move * Time.deltaTime);
            transform.Rotate(0, horizontal_move * Time.deltaTime, 0);
            //transform.GetChild(3).Rotate(Input.GetAxis("Mouse Y") * Time.deltaTime * 25, 0, 0);


            //GetComponent<Rigidbody>().AddForce(horizontal_move, 0, vertical_move);


            currentRot += Input.GetAxis("Mouse Y") * Time.deltaTime * 25;

            currentRot = Mathf.Clamp(currentRot, -20, 50);

            //transform.GetChild(3).localRotation = Quaternion.Euler(currentRot,0,0);
            if (Jump)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GetComponent<Rigidbody>().AddForce(Vector3.up * 200);

                    freMaleAnim.SetBool("jump", true);
                    Jump = false;
                }


            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                freMaleAnim.SetBool("jump", false);
            }

            if (vertical_move > 0) //|| horizontal_move != 0)
            {
                freMaleAnim.SetBool("walk", true);
                freMaleAnim.SetBool("back_walk", false);
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    freMaleAnim.SetBool("run", true);
                    GetComponent<Rigidbody>().AddForce(0, 0, 180);
                    /*transform.Translate(new Vector3(0, 0, vertical_move * Time.deltaTime * 10));*/
                }
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    freMaleAnim.SetBool("run", false);
                    freMaleAnim.SetBool("walk", true);
                }
            }
            else if (vertical_move < 0)
            {
                freMaleAnim.SetBool("back_walk", true);
                freMaleAnim.SetBool("walk", false);
                freMaleAnim.SetBool("jump", false);

            }
            
            else
            {
                freMaleAnim.SetBool("walk", false);
                freMaleAnim.SetBool("back_walk", false);
                freMaleAnim.SetBool("run", false);
            }

            if (lives[2].activeSelf == false)
            {
                gameObject.SetActive(false);
                mainCam.transform.position = new Vector3(0, 9.82f, -14);
                mainCam.transform.rotation = Quaternion.Euler(6.794f, -1.063f, 0.242f);
                Score.SetActive(false);
                Reset.SetActive(true);
                blackBack.SetActive(true);
            }
            if (scr == 3)
            {
                gameObject.SetActive(false);
                mainCam.transform.position = new Vector3(0, 9.82f, -14);
                mainCam.transform.rotation = Quaternion.Euler(6.794f, -1.063f, 0.242f);
                
                nextLev.SetActive(true);
                if(sceneName == "Level 3")
                {
                    blackBack.SetActive(false);
                    Text.SetActive(false);
                    Canvas.GetComponent<Animator>().SetTrigger("Win");
                }
                else
                {
                    blackBack.SetActive(true);
                }
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cube"))
        {
            scr++;
            Score.GetComponent<Text>().text = "Score: " + scr.ToString() + "/3";
            Destroy(other.gameObject);
            
            
        }
        if (other.gameObject.CompareTag("grey_cube"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Empty"))
        {
            resetLev();
            
        }
        
    }
    public void resetLev()
    {
        lives[h++].SetActive(false);
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Level 1")
        {
            transform.position = new Vector3(0, 1.5f, 0);
        }
        else if (sceneName == "Level 2")
        {
            transform.position = new Vector3(-3.85f, 1, -8f);
        }
        else if (sceneName == "Level 3")
        {
            planeAnim.Play("idle");
            planeAnim.SetBool("first", false);
            planeAnim.SetBool("second", false);
            transform.position = new Vector3(-8.6f, 1.176f, 9.3f);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            text.color = Color.magenta;
            Text.GetComponent<RectTransform>().sizeDelta = new Vector2(680, 30);
            text.text = "Go and stand on the purple cube";
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Teleport ground")
        {
            Jump = true;
            
        }
        if (col.gameObject.tag == "anim_cube")
        {
            mainCam.transform.position = new Vector3(0, 9.82f, -14);
            mainCam.transform.rotation = Quaternion.Euler(6.794f, -1.063f, 0.242f);

            resetLev();
            
        }
        if (col.gameObject.tag == "Pink")
        {
            planeAnim.SetBool("first", true);
            planeAnim.SetBool("second", false);
            text.color = Color.grey;
            text.text = "Wait for the white cube, stand on it";
        }
        if(col.gameObject.tag == "White")
        {
            
            planeAnim.SetBool("second", true);
            planeAnim.SetBool("first", false);
            text.color = Color.red;
            text.text = "Now you have to keep your balance";
        }
        
        if(col.gameObject.tag == "Teleport ground")
        {
            isInGround = true;
            text.color = Color.red;
            text.text = "There is a key on the table, take it";
            
        }
        else
        {
            isInGround = false;
        }
        if (col.gameObject.tag == "Teleport")
        {

            /*transform.position = new Vector3(0, 0, 0);
            transform.Translate(0, 0, 0);
            transform.Rotate(0, 0, 0);;*/
            planeAnim.SetBool("teleport", true);
            LightForTele.SetActive(true);
            text.text = "";
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            offsetPosition = new Vector3(0, 2, -10);
        }
        
        else if (col.gameObject.tag == "Teleport down")
        {
            offsetPosition = new Vector3(0, 0.83f, -1.89f);
            LightForTeleDown.SetActive(true);
            text.color = Color.yellow;
            text.text = "Go and open the chest with the key";
            teleDwbl = true;
        }
        else if(teleDwbl)
        {
            offsetPosition = new Vector3(0, 1.45f, -3.3f);
            teleDwbl = false;
        }
        
        else
        {
            offsetPosition = new Vector3(0, 3, -5);
            if(sceneName == "Level 3")
            {
                LightForTeleDown.SetActive(false);
                LightForTele.SetActive(false);
            }
        }
        
    }
    
}



