using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggablePanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 pointerOffset;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 localPointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPointerPosition);
        pointerOffset = rectTransform.localPosition - (Vector3)localPointerPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPointerPosition))
        {
            rectTransform.localPosition = new Vector3(localPointerPosition.x, localPointerPosition.y, rectTransform.localPosition.z) + new Vector3(pointerOffset.x, pointerOffset.y, 0f);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Optionally add logic for when dragging ends
    }
}
