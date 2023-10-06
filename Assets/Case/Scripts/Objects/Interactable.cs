using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IPlaceable, IHoverable
{
    // General
    [Header("Control")]
    public bool isLocked;
    public bool isPlaced;
    [Space]
    [Header("General")]
    public string dataKey;
    public InteractableType type;
    public List<string> blocks = new List<string>();
    [Space]
    [Header("Visual supports")]
    public Vector3 animPos;

    // Referances
    private GameManager m_gameManager;
    private MeshRenderer m_meshRenderer;

    private void Start()
    {
        m_meshRenderer = gameObject.GetComponent<MeshRenderer>();
        m_gameManager = GameManager.Instance;
    }

    // update material after hover ending to normal
    public void EndHover(Material mtl)
    {
        m_meshRenderer.material = mtl;
    }

    // update material for hover effect
    public void Hover(Material mtl)
    {
        m_meshRenderer.material = mtl;
    }

    // placing object to correct place
    public void PlaceObject(Vector3 pos, Vector3 rot)
    {
        transform.DOMove(animPos + pos, .5f).OnComplete(() =>
        {
            if (type.Equals(InteractableType.Rod_bolt_Left) || type.Equals(InteractableType.Rod_bold_Right))
            {
                transform.DORotate(rot, .5f).OnComplete(() =>
                {
                    transform.DORotate(new Vector3(transform.eulerAngles.x,transform.eulerAngles.y, 175), .5f);
                    transform.DOMove(pos, .5f);
                });

            }
            else
            {
                transform.DORotate(rot, .5f);
                transform.DOMove(pos, .5f);
            }
        });
        isPlaced = true;
        EndHover(GameManager.Instance.holder.defaultMTL);
        m_gameManager.SetHoldingObject(null);
        m_gameManager.SetHoldingObjectInteractable(null);
        m_gameManager.EnableHolder();
        m_gameManager.data.steps[dataKey] = true;
        m_gameManager.CheckIsComplete();
    }

    // if object is not locked then remove object
    public virtual void UnplaceObject()
    {
        CheckLocked();
        if (!isLocked)
        {
            transform.DOMove(animPos + transform.position, .5f);
            isPlaced = false;
            m_gameManager.data.steps[dataKey] = false;
            transform.parent = null;
        }
        else
        {
            m_gameManager.uiManager.ShowRemovePopup();
        }
    }

    // check object for can placeable or can removable
    public virtual void CheckLocked()
    {
        foreach (var block in blocks)
        {
            if (m_gameManager.data.steps[block].Equals(true))
            {
                isLocked = true;
                return;
            }
        }
        isLocked = false;
    }
}
public enum InteractableType
{
    Wrist_pin,
    Rod_cap,
    Rod_bolt_Left,
    Rod_bold_Right,
    Rod_bearing_rod_side,
    Rod_bearing_cap_side,
    Rod,
    Pin_clip_left,
    Pin_clip_right
}