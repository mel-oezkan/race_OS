using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShapeCreator))]
public class ShapeEditor : Editor
{

    ShapeCreator shapeCreator;
    bool needsRepaint;

    void OnSceneGUI() 
    {
        Event guiEvent = Event.current;

        if (guiEvent.type == EventType.Repaint) {
            Draw();
        } else if (guiEvent.type == EventType.Layout) {
            // prevents the layout event from being consumed by the editor
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        } else {
            HandleInput(guiEvent);
            if (needsRepaint) {
                HandleUtility.Repaint();
            }
        }
    }

    void HandleInput(Event guiEvent) 
    {
        Ray mouseRay = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition);
        float drawPlaneHeight = 0;
        float distToDrawPlane = (drawPlaneHeight - mouseRay.origin.y) / mouseRay.direction.y;

        Vector3 mousePosition = mouseRay.GetPoint(distToDrawPlane);
        if (
            guiEvent.type == EventType.MouseDown // mouse zclick 
            && guiEvent.button == 0 // left mouse button
            && guiEvent.modifiers == EventModifiers.None    
            // prevent perspective camera from adding points when moving camera
        ) {
            
            // handler such that points can be undone
            Undo.RecordObject(shapeCreator, "Add point");
         
            shapeCreator.points.Add(mousePosition);
            needsRepaint = true;
        }
    } 

    void Draw() 
    {   
        for (int i = 0; i < shapeCreator.points.Count; i++) {
            Vector3 nextPoint = shapeCreator.points[(i+1) % shapeCreator.points.Count];
            Handles.color = Color.black;
            Handles.DrawDottedLine(shapeCreator.points[i], nextPoint, 4);
            Handles.color = Color.white;
            Handles.DrawSolidDisc(shapeCreator.points[i], Vector3.up, 0.5f);
        }
        needsRepaint = true;
    }

    void OnEnable() 
    {
        shapeCreator = target as ShapeCreator;
    }

}
