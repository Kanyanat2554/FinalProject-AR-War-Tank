using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARMapAutoPlacer_Static : MonoBehaviour
{
    public static ARMapAutoPlacer_Static Instance { get; private set; }
    [Header("Map Setup")]
    public List<GameObject> mapPrefabs;       // Map1, Map2, Map3
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;
    private GameObject currentMapInstance;

    [Header("Game End")]
    public GameObject winPrefab;
    public GameObject losePrefab;

    [Header("Settings")]
    public float minPlaneSize = 1.5f;

    private int currentMapIndex = 0;
    private GameObject currentMap;
    private bool mapPlaced = false;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update()
    {
        if (mapPlaced || currentMapIndex >= mapPrefabs.Count) return;

        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
        if (raycastManager.Raycast(center, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            ARPlane plane = FindPlaneById(hits[0].trackableId);
            if (plane == null) return;

            Vector2 size = plane.size;
            if (size.x < minPlaneSize || size.y < minPlaneSize) return;

            Quaternion flatRotation = Quaternion.Euler(0, hitPose.rotation.eulerAngles.y, 0);
            currentMap = Instantiate(mapPrefabs[currentMapIndex], hitPose.position, flatRotation);
            mapPlaced = true;

            // ลบ Plane Visual
            planeManager.enabled = false;
            foreach (var p in planeManager.trackables)
                p.gameObject.SetActive(false);

            Debug.Log($"✅ Map {currentMapIndex + 1} placed");
        }
    }

    ARPlane FindPlaneById(TrackableId id)
    {
        foreach (var plane in Object.FindObjectsByType<ARPlane>(FindObjectsSortMode.None))
            if (plane.trackableId == id) return plane;
        return null;
    }

    // เรียกจาก UIManager → เมื่อผ่านด่าน
    public void LoadLevelAtIndex(int index)
    {
        if (currentMap != null)
            Destroy(currentMap);

        currentMapIndex = index;
        mapPlaced = false;

        if (index >= mapPrefabs.Count)
        {
            Instantiate(winPrefab, Vector3.zero, Quaternion.identity);
            Debug.Log("🏁 All maps complete! You Win!");
        }
        else
        {
            planeManager.enabled = true;
            foreach (var p in planeManager.trackables)
                p.gameObject.SetActive(true);

            Debug.Log($"🔁 Ready for map {index + 1}");
        }
    }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowLose()
    {
        if (losePrefab != null)
        {
            Instantiate(losePrefab, Vector3.zero, Quaternion.identity);
            Debug.Log("🏆 You Win!");
        }
    }

    
    public void ShowWin()
    {
        if (winPrefab != null)
        {
            Instantiate(winPrefab, Vector3.zero, Quaternion.identity);
            Debug.Log("🏆 You Win!");
        }
    }
    public void ClearCurrentMap()
    {
        if (currentMapInstance != null)
        {
            Destroy(currentMapInstance);
            currentMapInstance = null;
        }

        // ถ้าคุณต้องการลบ AR Plane
        if (planeManager != null)
        {
            foreach (var plane in planeManager.trackables)
            {
                Destroy(plane.gameObject);
            }
        }
    }
    

}
