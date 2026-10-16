using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFitter : MonoBehaviour
{
    [Header("Design Rect (world units)")]
    public float designWidth = 16f;
    public float designHeight = 9f;

    [Header("Camera Focus Point")]
    public Vector2 focusPoint = new Vector2(0f, 4.5f);

    private Camera cam;
    private int lastW, lastH;

    void Awake()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = true;
        Apply();
    }

    void Update()
    {
        // Re-apply if window is resized (Editor) or rotated (device)
        if (Screen.width != lastW || Screen.height != lastH)
            Apply();
    }

    void Apply()
    {
        lastW = Screen.width;
        lastH = Screen.height;

        float screenAspect = (float)Screen.width / Screen.height;

        // Ortho size = half the visible vertical world units
        float sizeFromHeight = designHeight / 2f;
        float sizeFromWidth  = designWidth / (2f * screenAspect);

        // Take the larger so BOTH dimensions are guaranteed visible
        cam.orthographicSize = Mathf.Max(sizeFromHeight, sizeFromWidth);

        transform.position = new Vector3(focusPoint.x, focusPoint.y, -10f);
    }
}