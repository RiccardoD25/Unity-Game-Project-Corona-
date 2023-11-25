using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBounce : MonoBehaviour
{

    private float RandomX;
    private float RandomY;
    private float RandomZ;

    private float SpeedFactor;

    // Start is called before the first frame update
    void Start()
    {
        SpeedFactor = ApplicationData.SpeedFactor;//get speed frm MyGameManager
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision WhatHit)
    {
        //Check for a match with the specified tag on any GameObject that collides with your SpeedFactor
        if (WhatHit.gameObject.CompareTag("corona"))//if a virus hit the wall: make it bounce and change direction randomly
        {
            //If the GameObject's tag matches the one you suggest
            RandomX = UnityEngine.Random.Range(-SpeedFactor, SpeedFactor);
            RandomY = UnityEngine.Random.Range(-SpeedFactor, SpeedFactor);
            RandomZ = UnityEngine.Random.Range(-SpeedFactor, SpeedFactor);

            if (RandomX > -0.7 && RandomX < 0.7)
            {
                RandomX = 1.8f;
            }

            if (RandomY > -0.7 && RandomY < 0.7)
            {
                RandomY = 2.0f;
            }

            if (RandomZ > -0.7 && RandomZ < 0.7)
            {
                RandomZ = -1.5f;
            }

            WhatHit.gameObject.GetComponent<ConstantForce>().relativeForce = new Vector3(RandomX, RandomY, RandomZ);
            WhatHit.gameObject.GetComponent<ConstantForce>().relativeTorque = new Vector3(RandomX / 20, RandomY / 40, RandomZ / 30);

        }//end if a virus hit the wall

        if (WhatHit.gameObject.CompareTag("injector"))//if an injector hit the wall- destroy it
        {

            Destroy(WhatHit.gameObject);//destroy the injector that hit the wall


        }//end if a injector hit the wall

    }


}
