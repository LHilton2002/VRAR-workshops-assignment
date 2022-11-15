using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class moveblocker3 : MonoBehaviour

{
    public float stateTimer;
    public int state;

    // Start is called before the first frame update
    void Start()
    {
        stateTimer = 10.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stateTimer >= 0)
        {
            stateTimer = stateTimer - 0.05f;
        }

        if (stateTimer <= 0)
        {

            if (state == 1)
            {
                transform.position = new Vector3(3, 15, 7);
                state = 0;
            }

            else if (state == 0)
            {
                transform.position = new Vector3(8.32f, 1.6f, -2.07f);

                state = 1;
            }

            stateTimer = 10;
        }
    }
}
