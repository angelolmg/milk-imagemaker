using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotSession : MonoBehaviour
{
    public bool initiateSession = false;
    private bool sessionOnline = false;

    public float screenshotDelay = 1f;
    public int numberOfPhotosToTake = 10;
    private int photosTaken = 0;
    private int nitens = 0;

    private ObjectLocalizer objLocalizer;

    IEnumerator NewSession()
    {   
        yield return new WaitForSeconds(screenshotDelay);

        if (photosTaken < numberOfPhotosToTake){
            nitens += objLocalizer.Localize();
            Debug.Log("Screenshot nº " + photosTaken + " taken");
            photosTaken++;
            StartCoroutine(NewSession());
        } else {
            Debug.Log("Finished screenshot session");
            Debug.Log("Total of " + nitens + " itens detected in " + numberOfPhotosToTake + " photos taken");
            Debug.Log("Median item density of " + nitens/numberOfPhotosToTake);
            sessionOnline = false;
            photosTaken = 0;
            nitens = 0;
        }
        
    }

    void Start()
    {
        objLocalizer = FindObjectOfType<ObjectLocalizer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Make screenshot session
        if (initiateSession == true)
        {

            if (sessionOnline == false){
                Debug.Log("Creating new session");
                sessionOnline = true;
                StartCoroutine(NewSession());
            } else {
                Debug.Log("Session already online");
            }

            initiateSession = false;

        }
        
    }
}
