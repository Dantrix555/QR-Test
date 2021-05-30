using System;
using System.Threading.Tasks;

public abstract class UIContainerBase : UIPanelBase
{
    protected void SetInteractionContainerFlagValue(bool newValue)
    {
        p_localIsReadyForInteraction = newValue;
    }

    protected async void ActivateBehaviourInteractionFlagWithDelay(float interactionFlagDelay = 1.0f)
    {
        await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(interactionFlagDelay));
        SetInteractionContainerFlagValue(true);
    }

    private bool p_localIsReadyForInteraction = false;

    public bool PanelIsReadyToInteract()
    {
        if (IsQueuedForLerp) { return false; }

        return p_localIsReadyForInteraction;
    }


    protected override void OnSetup()
    {
        SetInteractionContainerFlagValue(false);
        OnContainerSetup();
    }

    protected abstract void OnContainerSetup();

    protected abstract void ResetAllContainedElements();

    protected override void OnClosedPanel()
    {
        OnContainerClosed();
    }

    protected abstract void OnContainerClosed();
}
