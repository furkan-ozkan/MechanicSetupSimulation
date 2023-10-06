using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    #region Vars
    // Backend
    private Vector3 mousePos;
    

    // Object Settings
    [SerializeField] private float objectDistance;
    [SerializeField] private float objectDistanceSpeed;
    [SerializeField] private float m_rotateScale = 50;

    // Referances
    private GameManager gameManager;
    #endregion

    #region Unity Funcs.
    private void Start()
    {
        Init();
    }
    void Update()
    {
        ObjectMouseFollow();
        ObjectDistance();
        StopFollowing();
    }

    #endregion

    #region Inits
    private void Init()
    {
        gameManager = GameManager.Instance;
        objectDistance = Camera.main.nearClipPlane + .1f;
    }
    #endregion

    #region Mouse Follow

    // Object following mouse
    private void ObjectMouseFollow()
    {
        if (gameManager.GetHoldingObject())
        {
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, objectDistance));
            gameManager.GetHoldingObject().transform.position = mousePos;
        }
    }

    // Object stop following mouse
    private void StopFollowing()
    {
        if (Input.GetMouseButtonDown(1))
        {
            gameManager.SetHoldingObject(null);
            gameManager.SetHoldingObjectInteractable(null);
            gameManager.EnableHolder();
        }
    }

    #endregion

    #region Object Distance

    // changes the objects distance from camera
    private void ObjectDistance()
    {
        if (gameManager.GetHoldingObject())
        {
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");

            if (objectDistance + scrollInput > Camera.main.nearClipPlane + .1f)
            {
                objectDistance += scrollInput / objectDistanceSpeed;
            }
        }
    }

    #endregion

    #region Rotate Object



    #endregion

}
