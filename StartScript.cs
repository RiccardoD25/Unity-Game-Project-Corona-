using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ApplicationData
{

    public static float SpeedFactor = 1f;//speed factor of the viruses
    public static int HowManyViruses;///number of viruses created.set on at startScript
    public static bool LInjectorLoaded = false;
    public static bool LInjectorFired = false;
    public static bool RInjectorLoaded = false;
    public static bool RInjectorFired = false;


}

public class StartScript : MonoBehaviour
{

    private bool IsRayTouching = false;
    public GameObject StartLight;
    private float Raydist;
    public GameObject RayHandAnchor;//the hand the ray is attached to
    public GameObject RayObject;//the ray itself, a child of the controller, a child of the hand

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (IsRayTouching == true)
        {

            Raydist = Vector3.Distance(RayHandAnchor.transform.position, transform.position);//how fat is the hand?
            RayObject.transform.localScale = new Vector3(0.005f, 0.005f, Raydist * 1.1f);//make the ray 10% longer than the required, so it doesnt twitch. if it does- make 1.1 longer.
            RayObject.transform.localPosition = new Vector3(0, 0, Raydist * 0.55f);//move ray a little more than half its length back, so it stays on hand. make sure it corrsponds to half the length

        }
        else
        {
            RayObject.transform.localScale = new Vector3(0.005f, 0.005f, 20.0f);
            RayObject.transform.localPosition = new Vector3(0, 0, 10.0f);
        }



        if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger) && IsRayTouching == true ||
            Input.GetKeyDown(KeyCode.B))//the b key is so we can test in unity before building
        {

            ApplicationData.HowManyViruses = 10;///here is where we decide how many viruses to spawn
            SceneManager.LoadScene("CoronaLoading");//go to loading scene
            
        }



    }//end of update

    void OnTriggerEnter(Collider MyRay)
    {

        //Check for a match with the specified tag on any GameObject that collides with your GameObject
        if (MyRay.gameObject.CompareTag("ray"))//is it you ray????
        {

            if (IsRayTouching == false)
            {
                // MyRay.gameObject.GetComponent<Renderer>().enabled = true;

                var RayRenderer = MyRay.gameObject.GetComponent<Renderer>();

                //Call SetColor using the shader property name "_Color" and setting the color to red
                RayRenderer.material.SetColor("_Color", Color.red);


                StartLight.SetActive(true);
                IsRayTouching = true;
                // print(IsRayTouching);
                OVRInput.SetControllerVibration(0.1f, 0.1f, OVRInput.Controller.LTouch);//if ray attached to left
                Invoke("StopVibration", 0.1f);
            }

        }

    }

    void OnTriggerExit(Collider MyRay)
    {
        //Check for a match with the specified tag on any GameObject that collides with your GameObject
        if (MyRay.gameObject.CompareTag("ray"))
        {

            var RayRenderer = MyRay.gameObject.GetComponent<Renderer>();

            //Call SetColor using the shader property name "_Color" and setting the color to red
            RayRenderer.material.SetColor("_Color", Color.green);
            //MyRay.gameObject.GetComponent<Renderer>().enabled = false;
            StartLight.SetActive(false);
            IsRayTouching = false;
            StopVibration();
        }


    }



    void StopVibration()
    {
        OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.LTouch);
        OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.RTouch);
    }

}
