using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class animationScript : MonoBehaviour
{
    public Animator planeAnim;
    public GameObject free_male;
    public GameObject free_male_white;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "free male" && planeAnim.GetCurrentAnimatorStateInfo(0).IsName("white cube"))
        {
            /*free_male.SetActive(false);
            free_male_white.SetActive(true);*/
            planeAnim.SetBool("second", true);
            planeAnim.SetBool("first", false);
        }
        
    }
}
