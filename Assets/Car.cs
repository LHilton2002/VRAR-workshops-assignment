using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Car : MonoBehaviour
{

    public List<Transform> wps;
    public List<Transform> route;
    public int routeNumber = 0;
    public int targetWP = 0;
    public bool go = false;
    public float initialDelay;


    Rigidbody crigidbody;

    // Start is called before the first frame update
    void Start()
    {
        wps = new List<Transform>();
        GameObject wp;

        wp = GameObject.Find("CP1");
        wps.Add(wp.transform);

        wp = GameObject.Find("CP2");
        wps.Add(wp.transform);

        wp = GameObject.Find("CP3");
        wps.Add(wp.transform);

        wp = GameObject.Find("CP4");
        wps.Add(wp.transform);

        wp = GameObject.Find("CP5");
        wps.Add(wp.transform);

        wp = GameObject.Find("CP6");
        wps.Add(wp.transform);

        wp = GameObject.Find("CP7");
        wps.Add(wp.transform);

        wp = GameObject.Find("CP8");
        wps.Add(wp.transform);


        initialDelay = Random.Range(2.0f, 4.0f);
        transform.position = new Vector3(0.0f, -1.5f, 0.0f);

        crigidbody = GetComponent<Rigidbody>();

        SetRoute();
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (!go)
        {
            initialDelay -= Time.deltaTime;
            if (initialDelay <= 0.0f)
            {
                go = true;
                SetRoute();
            }
            else return;
        }

        Vector3 displacement = route[targetWP].position - transform.position;
        displacement.y = 0;
        float dist = displacement.magnitude;

        if (dist < 0.1f)
        {
            targetWP++;
            if (targetWP >= route.Count)
            {
                SetRoute();
                return;
            }
        }

        //calculate velocity for this frame
        Vector3 velocity = displacement;
        velocity.Normalize();
        velocity *= 15f;
        //apply velocity
        Vector3 newPosition = transform.position;
        newPosition += velocity * Time.deltaTime;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.MovePosition(newPosition);

        //align to velocity
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, velocity,
        10.0f * Time.deltaTime, 0f);
        Quaternion rotation = Quaternion.LookRotation(desiredForward);
        rb.MoveRotation(rotation);

    }



    void SetRoute()
    {
        //randomise the next route


        //routeNumber = Random.Range(0, 2);
        routeNumber = 2;


        //set the route waypoints
        if (routeNumber == 0) route = new List<Transform> { wps[0], wps[1], wps[2], wps[3] };
        else if (routeNumber == 1) route = new List<Transform> { wps[4], wps[5] };
        else if (routeNumber == 2) route = new List<Transform> { wps[0], wps[6], wps[7], wps[4], wps[5], wps[1], wps[2], wps[3], wps[0] };

        //initialise position and waypoint counter
        transform.position = new Vector3(route[0].position.x, 0.5f, route[0].position.z);
        targetWP = 1;
    }


    private void OnTriggerEnter(Collider other)
    {
        crigidbody.constraints = RigidbodyConstraints.FreezePosition;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Hard crash dodged");

        crigidbody.constraints = RigidbodyConstraints.None;
    }
}


