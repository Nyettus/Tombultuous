using UnityEditor;
using UnityEngine;


// In unity > 2020 support for serializable generic classes is added that lets us declare this
[CustomPropertyDrawer(typeof(GrowingPool<>))]
//https://catlikecoding.com/unity/tutorials/editor/custom-data/
public class GrowingPoolDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
                                            GUIContent label)
    {
        //return EditorGUI.GetPropertyHeight(property, label, true);
        return EditorGUIUtility.singleLineHeight * 3;
    }

    public override void OnGUI(Rect position,
                               SerializedProperty property,
                               GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.PrefixLabel(position, label);

        position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.indentLevel++;
        position = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

        if (Application.isPlaying)
        {
            // This is a masterclass in how to do Property Drawers for values that need to be evaluated as functions
            // The interface would not have to be used if the type of the class was concrete but Covariance makes it so we cant cast down to GrowingPool<PooledItem>
            // This allows us to expose functions and get them using this function which uses strong Reflection to get a reference from the targetObject
            var pool = EditorHelpers.SerializedPropertyToObject<IPoolCountStorage>(property);
            if (pool != null)
            {
                EditorGUI.LabelField(position, "Pool amount", $"{pool.GetPoolAmount()}");
                //field1Rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                //EditorGUI.LabelField(field1Rect, "Pool amount", $"{pool.PoolAmount()}");
            }
            else
                EditorGUI.LabelField(position, "Pool amount", $"Pool not initialised");
        }
        else
            EditorGUI.LabelField(position, "Pool amount", $"Pool not initialised");

        EditorGUI.indentLevel--;
        EditorGUI.EndProperty();
    }
}