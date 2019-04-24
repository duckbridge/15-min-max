using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseClickListener : CSBehaviour {

    public List<Camera> usedCameras = new List<Camera>();
    public float dragDistance = 1f;
    public float dragFriction = 20f;
    private Vector3 mouseClickPoint;
    
    protected bool isMouseDown = false;
    protected bool isDragging = false;
    public bool canBePaused = false;

    private static GameObject selectedObject;

    public void Start() {
    }

    public void Update() {
        if(!isMouseDown) {
            if (Input.GetMouseButtonDown(0)) {
                isMouseDown = true;
                selectedObject = null;
                RaycastHit hitSummary = RaycastObjectsInCameras(Input.mousePosition);
                this.mouseClickPoint = Input.mousePosition;

                if (hitSummary.collider != null) {
                    selectedObject = hitSummary.collider.gameObject;
                    selectedObject.SendMessage("OnClicked", hitSummary, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        
        if(Input.GetMouseButtonUp(0)) {
            this.isMouseDown = false;
            
            if(selectedObject != null) {
                selectedObject.SendMessage("OnDraggingStopped", null, SendMessageOptions.DontRequireReceiver);
                DispatchMessage("OnDraggingStopped", null);
            }

            selectedObject = null;
        }

        if(isMouseDown) {
           OnMouseDown();
        }
    }

    private void OnMouseDown() {
        foreach(Camera usedCamera in usedCameras) {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 drag = (mouseClickPoint - currentMousePosition);

            mouseClickPoint -= (drag / dragFriction);

            DragSummary dragSummary = new DragSummary();
            dragSummary.position = usedCamera.ScreenToWorldPoint(currentMousePosition);
            dragSummary.cameraSource = usedCamera;
            dragSummary.amount = drag;

            if(selectedObject != null)
                selectedObject.SendMessage("OnDrag", dragSummary, SendMessageOptions.DontRequireReceiver);

            if(Mathf.Abs(drag.x) > dragDistance) {
                if(selectedObject != null)
                    selectedObject.SendMessage("OnHorizontalDrag", dragSummary, SendMessageOptions.DontRequireReceiver);
                DispatchMessage("OnHorizontalDrag", dragSummary);
            }

            if(Mathf.Abs(drag.y) > dragDistance) {
                if(selectedObject != null)
                    selectedObject.SendMessage("OnVerticalDrag", dragSummary, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private RaycastHit RaycastObjectsInCameras(Vector2 screenPos) {
        RaycastHit hitInfo = new RaycastHit();
        foreach(Camera usedCamera in usedCameras) {
            hitInfo = RaycastObject(usedCamera, screenPos);
            if(hitInfo.collider != null) {
                return hitInfo;
            }
        }
        return hitInfo;
    }

    private RaycastHit RaycastObject(Camera usedCamera, Vector2 screenPos) {
        RaycastHit info;
        Physics.Raycast(usedCamera.ScreenPointToRay(screenPos), out info, 200);

        return info;
    }

    public static GameObject GetSelectedObject() {
        return selectedObject;
    }

    public override void OnPauseGame() {
        if(canBePaused) base.OnPauseGame();
    }

    public override void OnResumeGame() {
        if(canBePaused) base.OnResumeGame();
    }
}
