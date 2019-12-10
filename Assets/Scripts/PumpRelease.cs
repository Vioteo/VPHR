﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Controllables;

public class PumpRelease : MonoBehaviour
{
    VRTK_BaseControllable controllable;
    public GameObject Pump;
    public GameObject ColActivate;
    public GameObject[] Triggers;
    //[SerializeField] GameObject AdditionTrigger;
    // Start is called before the first frame update
    void Start()
    {
        //AdditionTrigger.SetActive(false);
        foreach (Collider col in Pump.GetComponents<Collider>())
        {
            col.enabled = false;
        }
        foreach (Collider col in ColActivate.GetComponents<Collider>())
        {
            col.enabled = false;
        }
        foreach (GameObject obj in Triggers)
        {
            obj.SetActive(false);
        }
        controllable = (controllable == null ? GetComponent<VRTK_BaseControllable>() : controllable);
        controllable.MaxLimitReached += Controllable_MaxLimitReached;
        controllable.MinLimitExited += Controllable_MinLimitExited;
    }

    private void Controllable_MinLimitExited(object sender, ControllableEventArgs e)
    {

    }

    private void Controllable_MaxLimitReached(object sender, ControllableEventArgs e)
    {
        Destroy(Pump.GetComponent<FixedJoint>());
        
        Pump.GetComponent<Rigidbody>().AddForce(Vector3.forward * 120f);
        foreach(Collider col in Pump.GetComponents<Collider>())
        {
            col.enabled = true;
        }
        foreach (Collider col in ColActivate.GetComponents<Collider>())
        {
            col.enabled = true;
        }
        foreach (GameObject obj in Triggers)
        {
            obj.SetActive(true);
        }
        GameObject.FindGameObjectWithTag("Controller").GetComponent<EducationControll>().PumpOut = true;
        //AdditionTrigger.SetActive(true);
        Destroy(this);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
