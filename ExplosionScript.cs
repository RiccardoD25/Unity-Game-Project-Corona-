using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeleteExplosion", 3.0f);
    }

    void DeleteExplosion() //this will define what DeleteExplosion will do
    {
        Destroy(this.gameObject);
    }

}
//end of public class
