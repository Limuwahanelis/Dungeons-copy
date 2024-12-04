using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class CameraShake2D : MonoBehaviour
{
#if UNITY_EDITOR
    [Header("Debug")]
    [SerializeField] bool _debug;
    [SerializeField,Tooltip("Keep trauma at constant level when debugging")] bool _continous;
#endif
    [Header("Settings")]
    [SerializeField] Transform _camera;
    [SerializeField] float _decreaseSpeed = 0.1f;
    [SerializeField] float _timeSpeed;
    [SerializeField] float _power=2;
    [Header("Rotation")]
    [SerializeField] float _maxAngle;
    [SerializeField] float _angleSeed;
    [Header("Position")]
    [SerializeField] float _maxOffset;
    [SerializeField] float _xSeed;
    [SerializeField] float _ySeed;
    [SerializeField]  float _trauma=0;
    private float _shake;//0-1
    private float _angle;
    private float _offsetX;
    private float _offsetY;
    private float _time;
    private Coroutine _shakeCor;
    // Start is called before the first frame update
    void Start()
    {
        _shake = Mathf.Pow(_trauma, _power);
        if (!_continous) _trauma = 0;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (_debug)
        {
            _time += Time.deltaTime * _timeSpeed;
            _angle = _maxAngle * _shake * PerlinNoise(_angleSeed, _time);
            _offsetX = _maxOffset * _shake * PerlinNoise(_xSeed, _time);
            _offsetY = _maxOffset * _shake * PerlinNoise(_ySeed, _time);
            if (!_continous)
            {
                _trauma -= _decreaseSpeed * Time.deltaTime;
                _trauma = math.clamp(_trauma, 0, 1);
            }

            _shake = Mathf.Pow(_trauma, _power);
            _camera.eulerAngles = new Vector3(0, 0, _angle);
            _camera.transform.localPosition = new Vector3(_offsetX, _offsetY, -10);
        }
#endif
    }
    public void Shake(float traumaValue)
    {
        if (_trauma == 0)
        {
            _trauma += traumaValue;
            StartCoroutine(ShakeCor());
        }
        else
        {
            _trauma += traumaValue;
        }
       
    }
    IEnumerator ShakeCor()
    {
        while(_trauma>0)
        {
            _time += Time.deltaTime * _timeSpeed;
            _angle = _maxAngle * _shake * PerlinNoise(_angleSeed, _time);
            _offsetX = _maxOffset * _shake * PerlinNoise(_xSeed, _time);
            _offsetY = _maxOffset * _shake * PerlinNoise(_ySeed, _time);

            _camera.eulerAngles = new Vector3(0, 0, _angle);
            _camera.transform.localPosition = new Vector3(_offsetX, _offsetY, -10);

            _trauma -= _decreaseSpeed * Time.deltaTime;
            _trauma = math.clamp(_trauma, 0, 1);
            _shake = Mathf.Pow(_trauma, _power);

            yield return null;
        }
    }
    private float PerlinNoise(float seed,float time)
    {
        return (Mathf.PerlinNoise1D(seed + time) - 0.5f) * 2;
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(CameraShake2D))]
    public class CameraShake2DEditor:Editor
    {
        SerializedProperty _trauma;
        private void OnEnable()
        {
            _trauma = serializedObject.FindProperty("_trauma");
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            if(GUILayout.Button("Restore Trauma"))
            {
                _trauma.floatValue = 1;
            }
            if(GUILayout.Button("Shake"))
            {
                (target as CameraShake2D).Shake(1f);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
