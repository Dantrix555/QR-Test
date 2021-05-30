using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class UIBase : MonoBehaviour
{
    private Coroutine p_lerpCoroutine;
    protected virtual float DefaultLerpDuration => 5.0f;

    private CanvasGroup canvasGroup;
    protected CanvasGroup CanvasGroupComponent => canvasGroup != null ? canvasGroup : transform.GetOrAddComponentExtension<CanvasGroup>(out canvasGroup);

    public void SetUIBaseCanvasGroupInterctableStatus(bool newStatus)
    {
        CanvasGroupComponent.interactable = newStatus;
        CanvasGroupComponent.blocksRaycasts = newStatus;
    }

    public void ForceOpenedBase(bool canvasIsActive = true)
    {
        SetUIBaseVisibleStatus(true, canvasIsActive, false);
    }

    public void ForceCloseBase()
    {
        bool unusedBool = false;
        SetUIBaseVisibleStatus(false, unusedBool, false);
    }

    protected bool UIBaseIsTotallyActive => p_isInactiveOrLerpHandling ? false : CanvasGroupComponent.alpha > 0.5f;

    public void SetLerpToStatus(bool newStatus)
    {
        SetUIBaseVisibleStatus(newStatus, newStatus, true);
    }

    private bool p_isInactiveOrLerpHandling = false;
    public bool IsQueuedForLerp => p_isInactiveOrLerpHandling;

    private void SetUIBaseVisibleStatus(bool objStatus, bool canvasStatus, bool useLerpTransition)
    {
        p_isInactiveOrLerpHandling = true;

        if (p_lerpCoroutine != null) { StopCoroutine(p_lerpCoroutine); }

        if (useLerpTransition && !transform.HaveParentedActiveStatus())
        {
            useLerpTransition = false;
        }

        if(useLerpTransition)
        {
            p_lerpCoroutine = StartCoroutine(CanvasGroupLerpEnumerator(objStatus));
        }
        else
        {
            SetElementsVisibleStatus(objStatus, canvasStatus, objStatus, !objStatus);
        }
    }

    protected IEnumerator CanvasGroupLerpEnumerator(bool newVisibleStatus)
    {
        SetElementsVisibleStatus(true, !newVisibleStatus, false, false);

        float hideJourney = 0f;

        while(hideJourney <= DefaultLerpDuration)
        {
            hideJourney += Time.deltaTime;
            float percent = Mathf.Clamp01(hideJourney / DefaultLerpDuration);
            float inversePercent = Mathf.Clamp01(1 - percent);
            CanvasGroupComponent.alpha = newVisibleStatus ? percent : inversePercent;
            yield return null;
        }

        SetElementsVisibleStatus(newVisibleStatus, newVisibleStatus, newVisibleStatus, !newVisibleStatus);
    }

    private void SetElementsVisibleStatus(bool activePar, bool visiblePar, bool isInteractable, bool lerpVarStatus)
    {
        SetUIBaseCanvasGroupInterctableStatus(isInteractable);

        if (!activePar) { OnUIClosed?.Invoke(); }

        CanvasGroupComponent.alpha = visiblePar ? 1 : 0;
        gameObject.SetActive(activePar);

        p_isInactiveOrLerpHandling = lerpVarStatus;
    }

    protected abstract Action OnUIClosed { get; }
}
