using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(bonusCurveSegment))]
public class BonusCurveSegmentDrawer : PropertyDrawer
{
    const float line = 18f;
    const float space = 4f;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return line * 4 + space * 4;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var target = property.FindPropertyRelative("TargetParameter");
        var coefficient = property.FindPropertyRelative("Coefficient");

        var baseBonus = property.FindPropertyRelative("baseBonus");
        var midBonus = property.FindPropertyRelative("midBonus");
        var topBonus = property.FindPropertyRelative("topBonus");

        //---------------------------------------
        // Prima riga
        //---------------------------------------

        //Rect left = new Rect(position.x, position.y, position.width * .65f, line);
        //Rect right = new Rect(position.x + position.width * .68f, position.y, position.width * .32f, line);
        //EditorGUI.PropertyField(left, target, new GUIContent("Target Parameter"));
        //EditorGUI.PropertyField(right, coefficient, new GUIContent("Coeff."));

        //Label Target Parameter Position
        Rect labelRect = new Rect(position.x, position.y, 90, line);
        Rect fieldRect = new Rect(position.x + 70, position.y, 120, line);
        EditorGUI.LabelField(labelRect, "Target");
        EditorGUI.PropertyField(fieldRect, target, GUIContent.none);

        //Laber coefficient Parameter position
        Rect labelRect2 = new Rect(position.x + 220, position.y, 50, line);
        Rect fieldRect2 = new Rect(position.x + 275, position.y, 60, line);
        EditorGUI.LabelField(labelRect2, "Coeff.");
        EditorGUI.PropertyField(fieldRect2, coefficient, GUIContent.none);

        //---------------------------------------
        // Titoli colonne
        //---------------------------------------

        float y = position.y + line + space;

        float firstColumn = 70;
        float cellWidth = (position.width - firstColumn) / 3f;

        EditorGUI.LabelField(new Rect(position.x + firstColumn, y, cellWidth, line), "Base", EditorStyles.boldLabel);
        EditorGUI.LabelField(new Rect(position.x + firstColumn + cellWidth, y, cellWidth, line), "Mid", EditorStyles.boldLabel);
        EditorGUI.LabelField(new Rect(position.x + firstColumn + cellWidth * 2, y, cellWidth, line), "Top", EditorStyles.boldLabel);

        //---------------------------------------
        // Riga Count
        //---------------------------------------

        y += line;

        EditorGUI.LabelField(new Rect(position.x, y, firstColumn, line), "Count");

        DrawInt(baseBonus, "count", position.x + firstColumn, y, cellWidth);
        DrawInt(midBonus, "count", position.x + firstColumn + cellWidth, y, cellWidth);
        DrawInt(topBonus, "count", position.x + firstColumn + cellWidth * 2, y, cellWidth);

        //---------------------------------------
        // Riga Multiplier
        //---------------------------------------

        y += line;

        EditorGUI.LabelField(new Rect(position.x, y, firstColumn, line), "Multiplier");

        DrawFloat(baseBonus, "multiplayer", position.x + firstColumn, y, cellWidth);
        DrawFloat(midBonus, "multiplayer", position.x + firstColumn + cellWidth, y, cellWidth);
        DrawFloat(topBonus, "multiplayer", position.x + firstColumn + cellWidth * 2, y, cellWidth);

        EditorGUI.EndProperty();
    }

    void DrawInt(SerializedProperty property, string child, float x, float y, float width)
    {
        var p = property.FindPropertyRelative(child);
        EditorGUI.PropertyField(new Rect(x, y, width - 5, 18), p, GUIContent.none);
    }

    void DrawFloat(SerializedProperty property, string child, float x, float y, float width)
    {
        var p = property.FindPropertyRelative(child);
        EditorGUI.PropertyField(new Rect(x, y, width - 5, 18), p, GUIContent.none);
    }
}