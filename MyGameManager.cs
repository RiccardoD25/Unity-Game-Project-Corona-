using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MyGameManager : MonoBehaviour
{
    public GameObject TheVirus;
    public GameObject TheInjector;
    public GameObject TheLeftController;
    private GameObject CurrentLeftInjector;

    public GameObject TheRightController;
    private GameObject CurrentRightInjector;

    private float initialX;
    private float initialY;
    private float initialZ;

    public Text VirusesNumberDisplay;

    //Start is called before the first frame update
    void Start()
    {
        //Start by generating VIRUSES!!!
        for (int i=0; i<ApplicationData.HowManyViruses; i++)//Loop to instatiate viruses
        {
            initialX = Random.Range(-5f, 5f);
            initialY = Random.Range(-5f, 5f);
            initialZ = Random.Range(-5f, 5f);//give random XYZ for this instance
            Instantiate(TheVirus, new Vector3(initialX, initialY, initialZ), Quaternion.identity);
            //Make new virus

        }//End for END GENERATE VIRUSES//

        //Instantiate first injectors//
        
        MakeNewInjectorL();//Call the function to mke first new injeftor on left hand
        MakeNewInjectorR();//Call the function to mke first new injeftor on right hand
        //Definitions for these functions are right after "update"
    }

    //Update is called once per frame
    void Update()
    {
        //Display the number of viruses left
        VirusesNumberDisplay.text = "VIRUSES REMAINING: " + ApplicationData.HowManyViruses.ToString();

        // code for left hand//
        if (ApplicationData.LInjectorLoaded && !ApplicationData.LInjectorFired)
        {
            CurrentLeftInjector.transform.position = TheLeftController.transform.position;
            CurrentLeftInjector.transform.rotation = TheLeftController.transform.rotation;
        }

        if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger) && ApplicationData.LInjectorLoaded)//Trigger released
        {
            ApplicationData.LInjectorLoaded = true;//New loaded
            Invoke("MakeNewInjectorL", 0.3f);//Call MakeNewInjector in a little delay, give time to current injector to leave
        }

        //Code for right hand
        if(OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && ApplicationData.RInjectorLoaded)//Trigger released
        {
            ApplicationData.RInjectorLoaded = false;//Not loaded anymore
            ApplicationData.RInjectorFired = true;//Mark as fired
            CurrentRightInjector.GetComponent<ConstantForce>().enabled = true;//Enable forward force
            CurrentRightInjector.GetComponent<AudioSource>().pitch = Random.Range(0.1f, 2f);
            CurrentRightInjector.GetComponent<AudioSource>().Play();
        }

        if (!OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && !ApplicationData.RInjectorLoaded)//Trigger released
        {
            ApplicationData.RInjectorLoaded = true;//New loaded
            Invoke("<akeNewInjectorR", 0.3f);//Call MakeNewInjector in a little delay, give time to current injector to leave
        }

        ///Check if all viruses were killed, if yes - take to "WIN" scene///

        if (ApplicationData.HowManyViruses == 0)//No viruses left
        {
            SceneManager.LoadScene("CoronaWin");//Go to loading scene
        }
    }//End Update

    //Function to make new injector left hand
    void MakeNewInjectorL()
    {
        ApplicationData.LInjectorLoaded = true;//New loaded
        ApplicationData.LInjectorFired = false;//Not mark as fired
        CurrentLeftInjector = Instantiate(TheInjector, TheLeftController.transform.position, TheLeftController.transform.rotation);//Make new injector

    }//End function to make new injector left hand
    //Function to make new injector left hand
    void MakeNewInjectorR()
    {
        ApplicationData.RInjectorLoaded = true;//New loaded
        ApplicationData.RInjectorFired = false;//Not mark as fired
        CurrentRightInjector = Instantiate(TheInjector, TheRightController.transform.position, TheRightController.transform.rotation);//Make new injector

    }//End function to make new injector right hand
}//End class
