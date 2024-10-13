using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        foreach(Transform t in _patternTransforms)
        {
            PatternPoints.Add(t.localPosition);
            Destroy(t.gameObject);
        }
    }

    public bool PointsInitialized => PatternPoints.Count == _patternTransforms.Count;

    private void DrawPatternPoint(Vector3 origin, Vector3 point, int index)
    {
        Camera camera = SceneView.currentDrawingSceneView.camera;
        float zoom = camera.orthographicSize;
        //draw point
        Gizmos.color = _pointColor;
        Gizmos.DrawWireSphere(point, _pointRadius);
        //draw line
        Gizmos.DrawRay(origin, point - origin);
        //draw number
        var textStyle = new GUIStyle();
        
        textStyle.fontStyle = FontStyle.Bold;
        textStyle.fontSize = Mathf.FloorToInt(fontSize / zoom);
        textStyle.alignment = TextAnchor.MiddleCenter;
        textStyle.normal.textColor =_textColor;
        Handles.Label(point, index.ToString(), textStyle);
    }
    private void OnDrawGizmos()
    {
        DrawPatternPoints();
    }

    public void DrawPatternPoints()
    {
        for(int i =0; i< _patternTransforms.Count; i++)
        {
            DrawPatternPoint(transform.position, 
                PointsInitialized 
                    ? PatternPoints[i]+ transform.position 
                    : _patternTransforms[i].position, 
                i);
        }
    }
}
