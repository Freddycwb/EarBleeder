using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InvokeAfterCounter))]
public class InvokeAfterCounterEditor : Editor
{
    InvokeAfterCounter _counter;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        _counter = ((InvokeAfterCounter)target);
        MaxValueLine();
        CurrentValueLine();
        MinValueLine();
        serializedObject.ApplyModifiedProperties();
    }

    public void MaxValueLine()
    {
        GUILayout.BeginHorizontal();
        switch (_counter.maxValueType)
        {
            case InvokeAfterCounter.valueType.Int:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("maxValueInt"));
                break;
            case InvokeAfterCounter.valueType.Float:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("maxValueFloat"));
                break;
            case InvokeAfterCounter.valueType.IntVariable:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("maxValueIntVariable"));
                break;
            case InvokeAfterCounter.valueType.FloatVariable:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("maxValueFloatVariable"));
                break;
            default:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("maxValueInt"));
                break;
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maxValueType"));
        GUILayout.EndHorizontal();
    }

    public void CurrentValueLine()
    {
        GUILayout.BeginHorizontal();
        switch (_counter.currentValueType)
        {
            case InvokeAfterCounter.valueType.Int:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("currentValueInt"));
                break;
            case InvokeAfterCounter.valueType.Float:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("currentValueFloat"));
                break;
            case InvokeAfterCounter.valueType.IntVariable:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("currentValueIntVariable"));
                break;
            case InvokeAfterCounter.valueType.FloatVariable:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("currentValueFloatVariable"));
                break;
            default:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("currentValueInt"));
                break;
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("currentValueType"));
        GUILayout.EndHorizontal();
    }

    public void MinValueLine()
    {
        GUILayout.BeginHorizontal();
        switch (_counter.minValueType)
        {
            case InvokeAfterCounter.valueType.Int:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("minValueInt"));
                break;
            case InvokeAfterCounter.valueType.Float:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("minValueFloat"));
                break;
            case InvokeAfterCounter.valueType.IntVariable:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("minValueIntVariable"));
                break;
            case InvokeAfterCounter.valueType.FloatVariable:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("minValueFloatVariable"));
                break;
            default:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("minValueInt"));
                break;
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("minValueType"));
        GUILayout.EndHorizontal();
    }
}
