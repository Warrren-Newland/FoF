using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class ToggleSummary : MonoBehaviour, IPointerClickHandler
{
    public Canvas canvas;
    GameObject[] buttons;
    public bool isActive=true;
    SummaryController summaryController;

    void Start()
    {
        summaryController = GameObject.Find("GameManager").GetComponent<SummaryController>();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if(buttons == null)
        {
            buttons = GameObject.FindGameObjectsWithTag("SummaryTile");
        }

        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            if (isActive)
            {
                toggleButtons(false);
            }
            else
            {
                toggleButtons(true);
            }
        }
    }

    private void toggleButtons(bool active)
    {
        foreach (GameObject button in buttons)
        {
            CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
            if (active)
            {
                canvasGroup.alpha = 1f;
            }
            else
            {
                canvasGroup.alpha = 0f;
            }
            canvasGroup.interactable = active;
            canvasGroup.blocksRaycasts = active;
        }
        isActive = active;
        summaryController.isActive = active;
    }
}
