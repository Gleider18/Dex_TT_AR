using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceController : MonoBehaviour
{
    [SerializeField] private PortalController _portalControllerPrefab;
    [SerializeField] private GameObject _housePrefab;
    [SerializeField] private Button _changePrefabTypeButton;
    [SerializeField] private TextMeshProUGUI _currentPrefabText;
    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private Transform _player;

    private GameObject _currentPrefab;
    private ECurrentPrefabType _currentPrefabType;

    void Start()
    {
        _currentPrefabType = ECurrentPrefabType.House;
        _changePrefabTypeButton.onClick.AddListener(OnChangePrefab);
        _currentPrefabText.text = _currentPrefabType.ToString();
    }

    private void OnChangePrefab()
    {
        _currentPrefabType = _currentPrefabType == ECurrentPrefabType.House
            ? ECurrentPrefabType.Portal
            : ECurrentPrefabType.House;
        _currentPrefabText.text = _currentPrefabType.ToString();
        if (_currentPrefab != null)
        {
            Destroy(_currentPrefab.gameObject);
            _currentPrefab = null;
        }
    }

    public void Click()
    {
        var mousePos = Input.mousePosition;
        ARRayCasting(new Vector2(mousePos.x, mousePos.y));
    }

    private void ARRayCasting(Vector2 pos)
    {
        List<ARRaycastHit> hits = new();
        if (_raycastManager.Raycast(pos, hits, TrackableType.PlaneEstimated))
        {
            Pose pose = hits[0].pose;

            switch (_currentPrefabType)
            {
                case ECurrentPrefabType.House:
                    if (_currentPrefab == null) _currentPrefab = Instantiate(_housePrefab, pose.position, pose.rotation);
                    else _currentPrefab.transform.SetPositionAndRotation(pose.position, pose.rotation);
                    break;
                case ECurrentPrefabType.Portal:
                    if (_currentPrefab == null)
                    {
                        var tempPrefab = Instantiate(_portalControllerPrefab, pose.position, pose.rotation);
                        tempPrefab.Initialize(_player);
                        _currentPrefab = tempPrefab.gameObject;
                    }
                    else _currentPrefab.transform.SetPositionAndRotation(pose.position, pose.rotation);
                    break;
            }
        }
    }

    private enum ECurrentPrefabType
    {
        House,
        Portal
    }
}