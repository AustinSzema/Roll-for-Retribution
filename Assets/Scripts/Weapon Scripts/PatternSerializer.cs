#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using System.Collections.Generic;
using UnityEngine;

public class PatternSerializer : MonoBehaviour
{
    [SerializeField] private List<Transform> _patternTransforms = new();
    [SerializeField] private Color _textColor = new(1f, 1f, 1f, 0.5f);
    [SerializeField] private Color _pointColor = Color.red;
    [SerializeField] private float _pointRadius = 1;
    [SerializeField] private float fontSize = 36;
    public List<Vector3> PatternPoints { get; private set; } = new();

    private void Awake()
    {
        foreach (Transform t in _patternTransforms)
        {
            PatternPoints.Add(t.localPosition);
            Destroy(t.gameObject);
        }
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        DrawPatternPoints();
    }
    #endif

    public bool PointsInitialized => PatternPoints.Count == _patternTransforms.Count;

    private void DrawPatternPoint(Vector3 origin, Vector3 point, int index)
    {
        #if UNITY_EDITOR
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
            fontSize = Mathf.FloorToInt(fontSize / zoom),
            alignment = TextAnchor.MiddleCenter,
            normal = { textColor = _textColor }
        };
        Handles.Label(point, index.ToString(), textStyle);
        #endif
    }

    public void DrawPatternPoints()
    {
        for (int i = 0; i < _patternTransforms.Count; i++)
        {
            DrawPatternPoint(transform.position,
                PointsInitialized
                    ? PatternPoints[i] + transform.position
                    : _patternTransforms[i].position,
                i);
        }
    }

    public void UpdatePoints()
    {
        _patternTransforms.Clear();
        foreach (var t in gameObject.GetComponentsInChildren<Transform>())
        {
            if (t.gameObject.CompareTag("Point"))
            {
                _patternTransforms.Add(t);
            }
        }

        #if UNITY_EDITOR
        var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
        if (prefabStage != null)
        {
            EditorSceneManager.MarkSceneDirty(prefabStage.scene);
        }
        #endif
    }
}
