using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleVirusScript : MonoBehaviour
{
    private float SpeedFactor;
    private float RandomX;
    private float RandomY;
    private float RandomZ;

    public GameObject MyExplosion;

    // Start is called before the first frame update
    void Start()
    {
        //Start moving this virus in a randomdirection and speed
        SpeedFactor = ApplicationData.SpeedFactor;//Get speed from MyGameObject
        RandomX = UnityEngine.Random.Range(-SpeedFactor, SpeedFactor);
        RandomY = UnityEngine.Random.Range(-SpeedFactor, SpeedFactor);
        RandomZ = UnityEngine.Random.Range(-SpeedFactor, SpeedFactor);

        //make sure it is never smaller than 0.7+/-
        if (RandomX >-0.7 && RandomX < 0.7)
        {
            RandomX = 1.5f;
        }
        if (RandomY > -0.7 && RandomX < 0.7)
        {
            RandomY = 1.2f;
        }
        if (RandomZ > -0.7 && RandomX < 0.7)
        {
            RandomZ = 1.8f;
        }

        GetComponent<ConstantForce>().relativeForce = new Vector3(RandomX, RandomY, RandomZ);
        GetComponent<ConstantForce>().relativeTorque = new Vector3(RandomX / 20, RandomY / 40, RandomZ / 30);

    }

    //Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision WhatHit)
    {
        if (WhatHit.gameObject.CompareTag("injector")) //If Virus hits wall: make it bounce and change direction randomly
        {
            if (WhatHit.gameObject.GetComponent<ConstantForce>().enabled == true)
            {//Was the injector in flight? We want to ignore viruses flying into injectors while still in hand

                Destroy(this.gameObject); //Destory the virus

                Destroy(WhatHit.gameObject); //Destroy the injector that hit the wall
                Instantiate(MyExplosion, transform.position, transform.rotation); //Make new explosion
                ApplicationData.HowManyViruses--; //one less virus!
            }//End "if the injector was in flight and not still in hand"

        }//End if a Injector hit the virus

    }//End OnCollisionEnter

}//End class
