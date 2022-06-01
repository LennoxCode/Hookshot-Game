using UnityEngine;
/// <summary>
/// this class was taken from the slides
/// this class overloads the stateHandler to represent a concrete menu state
/// if the state is entered from anywhere the canvas group attached this state is shown by setting the alpha
/// to 1 and upon leaving the alpha is set to 0 this ensures that only on menu is visible at any time
/// </summary>
public class ScreenHandler : StateHandler
{
    [SerializeField] private string name;
    public override string Name => name;
    [SerializeField] private CanvasGroup canvasGroup;
    public override void OnEnter<T>(T transition)
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public override void OnExit<T>(T transition)
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
