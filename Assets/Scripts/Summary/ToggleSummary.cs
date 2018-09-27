using UnityEngine.EventSystems;
using UnityEngine;

public class ToggleSummary : MonoBehaviour, IPointerClickHandler
{
    public Canvas canvas;
    GameObject[] buttons;
    public bool isActive=true;

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
            button.SetActive(active);
        }
        isActive = active;
    }
}
