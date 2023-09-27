

using Flocking;
using Flocking.Behaviours;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CompositeBehaviour))]
public class CompositeBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //setup
        // CompositeBehaviour cb = (CompositeBehaviour)target;
        //
        // Rect r = EditorGUILayout.BeginHorizontal();
        // r.height = EditorGUIUtility.singleLineHeight;
        //
        // //check for behaviors
        // if (cb.behaviors == null || cb.behaviors.Length == 0)
        // {
        //     EditorGUILayout.HelpBox("No behaviors in array.", MessageType.Warning);
        //     EditorGUILayout.EndHorizontal();
        //     r = EditorGUILayout.BeginHorizontal();
        //     r.height = EditorGUIUtility.singleLineHeight;
        // }
        // else
        // {
        //     r.x = 30f;
        //     r.width = EditorGUIUtility.currentViewWidth - 95f;
        //     EditorGUI.LabelField(r, "Behaviors");
        //     r.x = EditorGUIUtility.currentViewWidth - 65f;
        //     r.width = 60f;
        //     EditorGUI.LabelField(r, "Weights");
        //     r.y += EditorGUIUtility.singleLineHeight * 1.2f;
        //
        //     EditorGUI.BeginChangeCheck();
        //     for (int i = 0; i < cb.behaviors.Length; i++)
        //     {
        //         r.x = 5f;
        //         r.width = 20f;
        //         EditorGUI.LabelField(r, i.ToString());
        //         r.x = 30f;
        //         r.width = EditorGUIUtility.currentViewWidth - 95f;
        //         cb.behaviors[i] = (FlockBehaviour)EditorGUI.ObjectField(r, cb.behaviors[i], typeof(FlockBehaviour), false);
        //         r.x = EditorGUIUtility.currentViewWidth - 65f;
        //         r.width = 60f;
        //         cb.weights[i] = EditorGUI.FloatField(r, cb.weights[i]);
        //         r.y += EditorGUIUtility.singleLineHeight * 1.1f;
        //     }
        //     if (EditorGUI.EndChangeCheck())
        //     {
        //         EditorUtility.SetDirty(cb);
        //     }
        // }
        //
        // EditorGUILayout.EndHorizontal();
        // r.x = 5f;
        // r.width = EditorGUIUtility.currentViewWidth - 10f;
        // r.y += EditorGUIUtility.singleLineHeight * 0.5f;
        // if (GUI.Button(r, "Add Behavior"))
        // {
        //     AddBehavior(cb);
        //     EditorUtility.SetDirty(cb);
        // }
        //
        // r.y += EditorGUIUtility.singleLineHeight * 1.5f;
        // if (cb.behaviors != null && cb.behaviors.Length > 0)
        // {
        //     if (GUI.Button(r, "Remove Behavior"))
        //     {
        //         RemoveBehavior(cb);
        //         EditorUtility.SetDirty(cb);
        //     }
        // }


    }

    // void AddBehavior(CompositeBehaviour cb)
    // {
    //     int oldCount = (cb.behaviors != null) ? cb.behaviors.Length : 0;
    //     FlockBehaviour[] newBehaviors = new FlockBehaviour[oldCount + 1];
    //     float[] newWeights = new float[oldCount + 1];
    //     for (int i = 0; i < oldCount; i++)
    //     {
    //         newBehaviors[i] = cb.behaviors[i];
    //         newWeights[i] = cb.weights[i];
    //     }
    //     newWeights[oldCount] = 1f;
    //     cb.behaviors = newBehaviors;
    //     cb.weights = newWeights;
    // }
    //
    // void RemoveBehavior(CompositeBehaviour cb)
    // {
    //     int oldCount = cb.behaviors.Length;
    //     if (oldCount == 1)
    //     {
    //         cb.behaviors = null;
    //         cb.weights = null;
    //         return;
    //     }
    //     FlockBehaviour[] newBehaviors = new FlockBehaviour[oldCount - 1];
    //     float[] newWeights = new float[oldCount - 1];
    //     for (int i = 0; i < oldCount - 1; i++)
    //     {
    //         newBehaviors[i] = cb.behaviors[i];
    //         newWeights[i] = cb.weights[i];
    //     }
    //     cb.behaviors = newBehaviors;
    //     cb.weights = newWeights;
    // }
}
