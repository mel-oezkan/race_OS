using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShapeCreator))]
public class ShapeEditor : Editor
{

    ShapeCreator shapeCreator;
    SelectionInfo selectionInfo;

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
            HandleLeftMouseDown(mousePosition);
        }

        if (
            guiEvent.type == EventType.MouseUp
            && guiEvent.button == 0 
            && guiEvent.modifiers == EventModifiers.None    
        ) {
            HandleLeftMouseUp(mousePosition);
        }

        if (
            guiEvent.type == EventType.MouseDrag
            && guiEvent.button == 0 
            && guiEvent.modifiers == EventModifiers.None    
        ) {
            HandleLeftMouseDrag(mousePosition);
        }

        // checks wether mouse is over a point 
        // unless a point is already selected
        if (!selectionInfo.pointIsSelected)  
            UpdateMouseOverInfo(mousePosition);
    } 

    void HandleLeftMouseDown(Vector3 mousePosition)
    {
        // create a new point if mouse is not over existing one
        if (!selectionInfo.mouseIsOverPoint)
        {

            int newPointIndex = (selectionInfo.lineIndex != -1) ? selectionInfo.lineIndex + 1 : shapeCreator.points.Count;
            
            // handler such that points can be undone
            Undo.RecordObject(shapeCreator, "Add point");
            shapeCreator.points.Insert(newPointIndex, mousePosition);
            selectionInfo.pointIndex = newPointIndex;
        }

        selectionInfo.pointIsSelected = true;
        selectionInfo.positionAtStartOfDrag = mousePosition;
        needsRepaint = true;
    }

    void HandleLeftMouseUp(Vector3 mousePosition)
    {
        // remove selection when mouse is released
        if (selectionInfo.pointIsSelected) {
            shapeCreator.points[selectionInfo.pointIndex] = selectionInfo.positionAtStartOfDrag;
            Undo.RecordObject(shapeCreator, "Move point");
            shapeCreator.points[selectionInfo.pointIndex] = mousePosition;

            selectionInfo.pointIsSelected = false;
            selectionInfo.pointIndex = -1;
            needsRepaint = true;
        }
    }

    void HandleLeftMouseDrag(Vector3 mousePosition)
    {
        if (selectionInfo.pointIsSelected) 
        {
            shapeCreator.points[selectionInfo.pointIndex] = mousePosition;
        }
    }

    void UpdateMouseOverInfo(Vector3 mousePosition) 
    {
        int mouseOverPointIndex = -1;

        for (int i = 0; i < shapeCreator.points.Count; i++) {
            if (Vector3.Distance(mousePosition, shapeCreator.points[i]) < shapeCreator.handleRadius ) {
                mouseOverPointIndex = i;
                break;
            }
        }

        // index changed
        if (mouseOverPointIndex != selectionInfo.pointIndex) {
            selectionInfo.pointIndex = mouseOverPointIndex;
            selectionInfo.mouseIsOverPoint = mouseOverPointIndex != -1;
            
            // repaint the new selection
            needsRepaint = true;
        }


        if (selectionInfo.mouseIsOverPoint) {
            selectionInfo.mouseIsOverLine = false;
            selectionInfo.lineIndex = -1;    
        } 
         else 
        {
            int mouseOverLineIndex = -1;
            float closestLineDist = shapeCreator.handleRadius;

            for (int i = 0; i < shapeCreator.points.Count; i++) {
                Vector3 nextPointInShape = shapeCreator.points[(i+1) % shapeCreator.points.Count];
                float distFromMouseToLine = HandleUtility.DistancePointToLineSegment(
                    mousePosition.ToXZ(), 
                    shapeCreator.points[i].ToXZ(), 
                    nextPointInShape.ToXZ()
                ); 

                if (distFromMouseToLine < closestLineDist) {
                    closestLineDist = distFromMouseToLine;
                    mouseOverLineIndex = i;
                    break;
                }
            }

            if (selectionInfo.lineIndex != mouseOverLineIndex)
            {
                selectionInfo.lineIndex = mouseOverLineIndex;
                selectionInfo.mouseIsOverLine = mouseOverLineIndex != -1;
                needsRepaint = true;
            }
        }
    }

    void Draw() 
    {   
        for (int i = 0; i < shapeCreator.points.Count; i++) {
            Vector3 nextPoint = shapeCreator.points[(i+1) % shapeCreator.points.Count];
            
            if (i == selectionInfo.lineIndex) 
            {    
                Handles.color = Color.red;
                Handles.DrawLine(shapeCreator.points[i], nextPoint);

            } else
            {
                Handles.color = Color.black;
                Handles.DrawDottedLine(shapeCreator.points[i], nextPoint, 4);
            }
            
            if (i == selectionInfo.pointIndex) 
                Handles.color = selectionInfo.pointIsSelected ? Color.black : Color.red;
            else
                Handles.color = Color.white;
    
            Handles.DrawSolidDisc(shapeCreator.points[i], Vector3.up, shapeCreator.handleRadius);
        }
        needsRepaint = true;
    }

    void OnEnable() 
    {
        shapeCreator = target as ShapeCreator;
        selectionInfo = new SelectionInfo();
    }

    public class SelectionInfo 
    {
        public int pointIndex;
        public bool mouseIsOverPoint;
        public bool pointIsSelected;
        public Vector3 positionAtStartOfDrag;


        public int lineIndex;
        public bool mouseIsOverLine;
    }

}

