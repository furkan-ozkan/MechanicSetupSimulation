using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHolder : MonoBehaviour
{
    private GameObject hoverObject = null;
    private GameManager gameManager;
    public Material hoverMTL;
    public Material defaultMTL;

    public LayerMask mask;

    private void Update()
    {
        Init();
        ClickAction();
        HoverAction();
    }
    private void OnDisable()
    {
        if (hoverObject)
        {
            hoverObject.GetComponent<Interactable>().EndHover(defaultMTL);
            hoverObject = null;
        }
    }

    #region Inits
    private void Init()
    {
        gameManager = GameManager.Instance;
    }
    #endregion

    #region Object Selection

    // if i click on any "Interactable" object it works
    private void ClickAction()
    {
        bool leftClick = Input.GetMouseButtonDown(0);

        if (!gameManager.globalCoolDown)
        {
            if (!gameManager.GetHoldingObject() && leftClick)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100, mask))
                {
                    if (hit.transform.GetComponent<Interactable>())
                    {
                        if (!hit.transform.GetComponent<Interactable>().isPlaced)
                        {
                            hit.transform.GetComponent<Collider>().enabled = false;
                            GameManager.Instance.SetHoldingObject(hit.transform.gameObject);
                            GameManager.Instance.SetHoldingObjectInteractable(GameManager.Instance.GetHoldingObject().GetComponent<Interactable>());
                            GameManager.Instance.EnableController();
                            hit.transform.GetComponent<Collider>().enabled = true;
                        }
                        else
                        {
                            hit.transform.GetComponent<Interactable>().UnplaceObject();
                        }
                    }
                }
            }
        }
    }

    #endregion

    #region Hover Object

    // If mouse hover any object it works and execute hover funcs.
    private void HoverAction()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100,mask))
        {
            if (hit.transform.GetComponent<Interactable>() && !hit.transform.gameObject.Equals(hoverObject))
            {
                if (hoverObject)
                    hoverObject.GetComponent<Interactable>().EndHover(defaultMTL);
                hoverObject = hit.transform.gameObject;
                hoverObject.GetComponent<Interactable>().Hover(hoverMTL);
            }
        }
        else
        {
            if (hoverObject)
            {
                hoverObject.GetComponent<Interactable>().EndHover(defaultMTL);
                hoverObject = null;
            }
        }
    }

    #endregion
}
