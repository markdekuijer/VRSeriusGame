using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using Valve.VR;
//using Valve.VR.InteractionSystem;

public class CharacterInput : MonoBehaviour
{
    //public SteamVR_Action_Boolean testTriggerAction;
    //public Hand rightHand;

	void Update ()
    {
        //if (Test())
        //    print("UseSecond");
        //if (IsEligible(rightHand))
        

        //(gameObject)finger.SetActive(Test());
        CheckPointing();
	}

    public void CheckPointing()
    {
        Unit u = null;
        //if (Test())
        //{
            //Debug.DrawRay(rightHand.gameObject.transform.position, rightHand.gameObject.transform.forward * 100);
            //RaycastHit hit;
            //if (Physics.Raycast(rightHand.gameObject.transform.position, rightHand.gameObject.transform.forward, out hit, Mathf.Infinity))
            //{
            //    if (hit.transform.gameObject.CompareTag("Unit"))
            //    {
            //        u = hit.transform.GetComponent<Unit>();
            //        u.ChangeColor(true);
            //    }
            //}

        //}
        //else if (u != null)
        //{
        //    u.ChangeColor(false);
        //    u = null;
        //}
            
    }

    //public bool Test()
    //{
    //    return testTriggerAction.GetState(rightHand.handType);
    //}

    //public bool IsEligible(Hand hand)
    //{
    //    if (hand == null)
    //        return false;

    //    if (!hand.gameObject.activeInHierarchy)
    //        return false;

    //    if (hand.hoveringInteractable != null)
    //        return false;

    //    if (hand.noSteamVRFallbackCamera == null)
    //    {
    //        if (hand.isActive == false)
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}
}
