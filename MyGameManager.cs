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
    private GameObject CurrentLeftInjector;//in hand not fired yet

    public GameObject TheRightController;
    private GameObject CurrentRightInjector;//in hand not fired yet

    private float initialX;
    private float initialY;
    private float initialZ;

    public Text VirusesNumberDisplay;

    // Start is called before the first frame update
    void Start()
    {
        ApplicationData.HowManyViruses = 10;///here is where we decide how many viruses to 
        //start by GENERATIing VIRUSES//
        for (int i = 0; i < ApplicationData.HowManyViruses; i++)//loop to instanciate viruses
        {
            initialX = Random.Range(-5f, 5f);
            initialY = Random.Range(-5f, 5f);
            initialZ = Random.Range(-5f, 5f);//give random xyz for this instance
            Instantiate(TheVirus, new Vector3(initialX, initialY, initialZ), Quaternion.identity);//make a new virus

        }//end for
        //END GENERATE VIRUSES//

        //instaciate first injectors//

        MakeNewInjectorL();///call function to make first new injector on left hand
        MakeNewInjectorR();///call function to make first new injector on right hand
        //definitions for these functions are right after "update"
    }//end of start

    // Update is called once per frame
    void Update()
    {
        ///display the number of viruses left

        VirusesNumberDisplay.text = "VIRUSES REMAINING: " + ApplicationData.HowManyViruses.ToString();


        ///code for left hand/////
        if (ApplicationData.LInjectorLoaded && !ApplicationData.LInjectorFired)
        {//if injector loaded but not fired
            CurrentLeftInjector.transform.position = TheLeftController.transform.position;
            CurrentLeftInjector.transform.rotation = TheLeftController.transform.rotation;
        }//attach it to the hand

        if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger) && ApplicationData.LInjectorLoaded ||
            Input.GetKeyDown(KeyCode.L))//trigger pressed, injector loaded but not fired yet...
        {
            ApplicationData.LInjectorLoaded = false;//not loaded anymore
            ApplicationData.LInjectorFired = true;//mark as fired
            CurrentLeftInjector.GetComponent<ConstantForce>().enabled = true;//enable forward force, FIRE!
            CurrentLeftInjector.GetComponent<AudioSource>().pitch = Random.Range(0.1f, 2f);
            CurrentLeftInjector.GetComponent<AudioSource>().Play();
            //  

        }

        if (!OVRInput.Get(OVRInput.RawButton.LIndexTrigger) && !ApplicationData.LInjectorLoaded)//trigger released
        {
            ApplicationData.LInjectorLoaded = true;//new loaded 
            Invoke("MakeNewInjectorL", 0.3f);//call MakeNewInjector in a little delay, give time to current injector to leave
        }

        ///code for right hand/////
        if (ApplicationData.RInjectorLoaded && !ApplicationData.RInjectorFired)
        {
            CurrentRightInjector.transform.position = TheRightController.transform.position;
            CurrentRightInjector.transform.rotation = TheRightController.transform.rotation;
        }

        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && ApplicationData.RInjectorLoaded ||
            Input.GetKeyDown(KeyCode.R))//trigger pressed, injector loaded but not fired yet...
        {
            ApplicationData.RInjectorLoaded = false;//not loaded anymore
            ApplicationData.RInjectorFired = true;//mark as fired
            CurrentRightInjector.GetComponent<ConstantForce>().enabled = true;//enable forward force
            CurrentRightInjector.GetComponent<AudioSource>().pitch = Random.Range(0.1f, 2f);
            CurrentRightInjector.GetComponent<AudioSource>().Play();

        }

        if (!OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && !ApplicationData.RInjectorLoaded)//trigger released
        {
            ApplicationData.RInjectorLoaded = true;//new loaded
            Invoke("MakeNewInjectorR", 0.3f);//call MakeNewInjector in a little delay, give time to current injector to leave
        }

        /////check if all viruses were killed, if yes- take to "win" scene//////

        if (ApplicationData.HowManyViruses == 0)//no viruses left
        {
            SceneManager.LoadScene("CoronaWin");//go to loading scene
        }

    }//end update

    //function  to make new injector left hand
    void MakeNewInjectorL()
    {
        ApplicationData.LInjectorLoaded = true;//new loaded 
        ApplicationData.LInjectorFired = false;//not mark as fired
        CurrentLeftInjector = Instantiate(TheInjector, TheLeftController.transform.position, TheLeftController.transform.rotation);//make a new injector

    }///end function  to make new injector left hand

    //function  to make new injector right hand
    void MakeNewInjectorR()
    {
        ApplicationData.RInjectorLoaded = true;//new loaded 
        ApplicationData.RInjectorFired = false;//not mark as fired
        CurrentRightInjector = Instantiate(TheInjector, TheRightController.transform.position, TheRightController.transform.rotation);//make a new injector

    }///end function  to make new injector right hand

}//end class
