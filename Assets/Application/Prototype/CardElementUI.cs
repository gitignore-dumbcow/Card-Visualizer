using UnityEngine;
using UnityEngine.EventSystems;

public class CardElementUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private RectTransform parentRect;
    private Canvas canvas;

    private Vector2 cursorDelta;
    private bool dragging;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        parentRect = rectTransform.parent as RectTransform;
        canvas = GetComponentInParent<Canvas>();
    }

    void Update()
    {
        if (dragging)
        {
            Vector2 screenMousePos = Input.mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRect, screenMousePos, canvas.worldCamera, out Vector2 localMousePos
            );

            Vector2 targetPos = localMousePos - cursorDelta;

            // Clamp to parent bounds
            Vector2 clampedPos = ClampToParentBounds(targetPos);
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, clampedPos, 0.1f);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect, eventData.position, eventData.pressEventCamera, out Vector2 localMousePos
        );

        cursorDelta = localMousePos - rectTransform.anchoredPosition;
        dragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;
    }

    private Vector2 ClampToParentBounds(Vector2 targetPos)
    {
        Vector2 elementSize = rectTransform.rect.size;
        Vector2 parentSize = parentRect.rect.size;

        float minX = -parentSize.x / 2 + elementSize.x / 2;
        float maxX = parentSize.x / 2 - elementSize.x / 2;
        float minY = -parentSize.y / 2 + elementSize.y / 2;
        float maxY = parentSize.y / 2 - elementSize.y / 2;

        return new Vector2(
            Mathf.Clamp(targetPos.x, minX, maxX),
            Mathf.Clamp(targetPos.y, minY, maxY)
        );
    }
}
