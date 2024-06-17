using UnityEngine;
using UnityEngine.EventSystems;

public class UIRaycastController : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private PlaceController _placeController;

    private float _pointerDownTime;
    private bool _isDragging;

    public void OnPointerClick(PointerEventData eventData)
    {
        float clickDuration = Time.time - _pointerDownTime;
        if (clickDuration < 0.1f && !_isDragging) _placeController.Click();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _pointerDownTime = Time.time;
        _isDragging = false;
    }

    public void OnPointerUp(PointerEventData eventData) => _isDragging = false;

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.delta.magnitude > 0) _isDragging = true;
    }
}
