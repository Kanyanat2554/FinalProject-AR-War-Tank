using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARGameManager : MonoBehaviour
{
    [SerializeField] GameObject gameAreaPrefab;

    private XROrigin arOrigin;

    private void Start()
    {
        arOrigin = Object.FindFirstObjectByType<XROrigin>();
        SetupGameArea();
        DisableARPlaneVisualization();
    }

    private void SetupGameArea()
    {
        Instantiate(gameAreaPrefab, arOrigin.transform);
    }

    private void DisableARPlaneVisualization()
    {
        ARPlaneManager planeManager = arOrigin.GetComponent<ARPlaneManager>();
        if (planeManager != null)
        {
            planeManager.enabled = false;
            foreach (var plane in planeManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }
        }
    }
}
