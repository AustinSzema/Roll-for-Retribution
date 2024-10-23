using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BulletPattern : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _patternTransforms = new();
    [SerializeField]
    private Color _textColor = new(1f, 1f, 1f, 0.5f);
    [SerializeField]
    private Color _pointColor = Color.red;
    [SerializeField]
    private float _pointRadius = 1;

    public List<Vector3> PatternPoints { get; private set; } = new();

    private void Awake()
    {
        foreach (Transform t in _patternTransforms)
        {
            PatternPoints.Add(t.localPosition);
            Destroy(t.gameObject);
        }
    }

    private bool PointsInitialized => PatternPoints.Count == _patternTransforms.Count;

#if UNITY_EDITOR
    private void DrawPatternPoint(Vector3 origin, Vector3 point, int index)
    {
        Camera camera = SceneView.currentDrawingSceneView.camera;
        float zoom = camera.orthographicSize;

        // Draw point
        Gizmos.color = _pointColor;
        Gizmos.DrawWireSphere(point, _pointRadius);

        // Draw line
        Gizmos.DrawRay(origin, point - origin);

        // Draw number
        var textStyle = new GUIStyle
        {
            fontStyle = FontStyle.Bold,
            fontSize = Mathf.FloorToInt(96 / zoom),
            alignment = TextAnchor.MiddleCenter,
            normal = { textColor = _textColor }
        };
        Handles.Label(point, index.ToString(), textStyle);
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < _patternTransforms.Count; i++)
        {
            DrawPatternPoint(
                transform.position,
                PointsInitialized
                    ? PatternPoints[i] + transform.position
                    : _patternTransforms[i].position,
                i
            );
        }
    }
#endif
}