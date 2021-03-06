using UnityEngine;

public class GameHandler : StateHandler
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