using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Basic Singleton
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    #endregion

    #region Vars
    [SerializeField] private GameObject holdingObject;
    [SerializeField] private Interactable holdingObjectInteractable;

    public UIManager uiManager;
    public ObjectController controller;
    public ObjectHolder holder;

    public DataSO data;

    #endregion

    #region Getters & Setters 
    public GameObject GetHoldingObject()
    {
        return holdingObject;
    }
    public void SetHoldingObject(GameObject newObjcet)
    {
        holdingObject = newObjcet;
    }

    public Interactable GetHoldingObjectInteractable()
    {
        return holdingObjectInteractable;
    }
    public void SetHoldingObjectInteractable(Interactable newInteractable)
    {
        holdingObjectInteractable = newInteractable;
    }

    #endregion

    public void EnableHolder()
    {
        holder.enabled = true;
        controller.enabled = false;
    }
    public void EnableController()
    {
        holder.enabled = false;
        controller.enabled = true;
    }

    public void CheckIsComplete()
    {
        foreach (var i in data.steps.Values)
        {
            if (i.Equals(false))
            {
                return;
            }
        }
        uiManager.ShowCompletePopup();
    }
}
