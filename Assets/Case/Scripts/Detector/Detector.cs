using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : SerializedMonoBehaviour
{
    [Header("Needed Items")]
    public Dictionary<InteractableType,GameObject> placeHolders = new Dictionary<InteractableType,GameObject>();

    [Header("Detected Item")]
    private InteractableType currentKey;
    private GameObject currentGO;
    private Vector3 pos, rot;

    // Check for "is any object triggered me?"
    // if any objects triggered than show placeholder and save it
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Interactable>())
        {
            foreach (var i in placeHolders)
            {
                if (other.GetComponent<Interactable>().type.Equals(i.Key) &&
                    other.gameObject.Equals(GameManager.Instance.GetHoldingObject()))
                {
                    currentKey = i.Key;
                    currentGO = other.gameObject;
                    pos = i.Value.transform.position;
                    rot = i.Value.transform.eulerAngles;
                    i.Value.SetActive(true);
                }
            }
        }
    }

    // If object leave tigger area reseting the values
    private void OnTriggerExit(Collider other)
    {
        HidePlaceHolder(other);
    }
    private void HidePlaceHolder(Collider other)
    {
        if (other.GetComponent<Interactable>())
        {
            foreach (var i in placeHolders)
            {
                if (other.GetComponent<Interactable>().type.Equals(i.Key))
                {
                    currentGO = null;
                    pos = Vector3.zero;
                    rot = Vector3.zero;
                    i.Value.SetActive(false);
                }
            }
        }
    }

    // if i hold an object and click on left click it checks is this object near to me
    // if it is then place the object
    private void Update()
    {
        if (currentGO != null)
        {
            if (currentGO.Equals(GameManager.Instance.GetHoldingObject()))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    currentGO.GetComponent<Interactable>().CheckLocked();
                    if (!currentGO.GetComponent<Interactable>().isLocked)
                    {
                        placeHolders[currentKey].SetActive(false);
                        currentGO.transform.parent = transform.parent;
                        currentGO.GetComponent<Interactable>().PlaceObject(pos, rot);
                        currentGO = null;
                    }
                    else
                    {
                        GameManager.Instance.uiManager.ShowPlacePopup();
                    }
                }
            }
            else
            {
                HidePlaceHolder(currentGO.GetComponent<Collider>());
            }
        }
    }
}
