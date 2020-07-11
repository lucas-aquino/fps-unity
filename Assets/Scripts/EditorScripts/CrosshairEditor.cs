using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Crosshair))]
public class CrosshairEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Crosshair crosshair = (Crosshair)target;

        crosshair.GetComponent<RectTransform>().sizeDelta = new Vector2(crosshair.gap, crosshair.gap);

        crosshair.topLine.sizeDelta = new Vector2(crosshair.thickness, crosshair.size);
        crosshair.rightLine.sizeDelta = new Vector2(crosshair.size, crosshair.thickness);
        crosshair.bottomLine.sizeDelta = new Vector2(crosshair.thickness, crosshair.size);
        crosshair.leftLine.sizeDelta = new Vector2(crosshair.size, crosshair.thickness);

    }
}
