using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject menuContent;
    // Start is called before the first frame update
    void Start()
    {
        menuContent.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if(menuContent.activeInHierarchy)
            {
                menuContent.SetActive(false);
                Time.timeScale = 1.0f;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                menuContent.SetActive(true);
                Time.timeScale = 0.0f;
                Cursor.lockState = CursorLockMode.None;
            }
           
        }

        
    }
}
