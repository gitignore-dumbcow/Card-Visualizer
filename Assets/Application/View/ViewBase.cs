using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ViewBase : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI Elements")]
    public RectTransform topBar;
    public RectTransform dragCorner;
    public Button closeButton;

    [Header("Color Controls")]
    public Image windowBackground;
    public Image topBarBackground;
    public Color windowColor = Color.white;
    public Color topBarColor = Color.gray;

    private RectTransform rectTransform;
    private bool isDragging = false;
    private bool isResizing = false;

    private Vector2 dragStartMousePos;
    private Vector2 dragStartAnchoredPos;
    private Vector2 resizeStartMousePos;
    private Vector2 resizeStartSize;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        if (closeButton != null)
            closeButton.onClick.AddListener(() => gameObject.SetActive(false));

        ApplyColors();
    }

    void OnValidate()
    {
        ApplyColors();
    }

    private void ApplyColors()
    {
        if (windowBackground != null)
            windowBackground.color = windowColor;

        if (topBarBackground != null)
            topBarBackground.color = topBarColor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(topBar, eventData.position, eventData.pressEventCamera))
        {
            isDragging = true;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out dragStartMousePos);
            dragStartAnchoredPos = rectTransform.anchoredPosition;
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(dragCorner, eventData.position, eventData.pressEventCamera))
        {
            isResizing = true;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out resizeStartMousePos);
            resizeStartSize = rectTransform.sizeDelta;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localPoint);

        if (isDragging)
        {
            Vector2 offset = localPoint - dragStartMousePos;
            rectTransform.anchoredPosition = dragStartAnchoredPos + offset;
        }
        else if (isResizing)
        {
            Vector2 offset = localPoint - resizeStartMousePos;
            Vector2 newSize = resizeStartSize + new Vector2(offset.x, -offset.y);
            newSize.x = Mathf.Max(100, newSize.x);
            newSize.y = Mathf.Max(100, newSize.y);
            rectTransform.sizeDelta = newSize;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        isResizing = false;
    }
}
