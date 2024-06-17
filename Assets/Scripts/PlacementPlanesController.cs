using UnityEngine;
using UnityEngine.UI;

public class PlacementPlanesController : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;

    private GameObject _planeContainer;
    
    private void Start()
    {
        _toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool value)
    {
        if (_planeContainer == null) _planeContainer = GameObject.Find("Trackables"); //TODO find a way to acces this directly from XROrigin
        _planeContainer.SetActive(value);
    }
}
