using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField] private List<PortalTeleporter> _portalTeleporters;
    [SerializeField] private List<PortalCamera> _portalCameras;

    public void Initialize(Transform player)
    {
        foreach (var item in _portalTeleporters) item.Initialize(player);
        foreach (var item in _portalCameras) item.Initialize(player);
    }
}
