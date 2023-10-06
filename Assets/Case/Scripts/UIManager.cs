using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject removePopup;
    [SerializeField] private GameObject placePopup;
    [SerializeField] private GameObject completePopup;

    public void ShowRemovePopup()
    {
        removePopup.SetActive(true);
        removePopup.transform.DOScale(1, .5f).OnComplete(() =>
        {
            StartCoroutine(HideTimer());
        });
        IEnumerator HideTimer()
        {
            yield return new WaitForSeconds(1f);
            removePopup.transform.DOScale(0, .5f);
            yield return new WaitForSeconds(.5f);
            removePopup.SetActive(false);
        }
    }
    public void ShowPlacePopup()
    {
        placePopup.SetActive(true);
        placePopup.transform.DOScale(1, .5f).OnComplete(() =>
        {
            StartCoroutine(HideTimer());
        });
        IEnumerator HideTimer()
        {
            yield return new WaitForSeconds(1f);
            placePopup.transform.DOScale(0, .5f);
            yield return new WaitForSeconds(.5f);
            placePopup.SetActive(false);
        }
    }
    public void ShowCompletePopup()
    {
        completePopup.SetActive(true);
        completePopup.transform.DOScale(1,.5f);
    }
}
