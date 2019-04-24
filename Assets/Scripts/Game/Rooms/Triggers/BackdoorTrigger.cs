using UnityEngine;
using System.Collections;

public class BackdoorTrigger : CSBehaviour
{
    Animation2D door;
    protected bool doorIsOpen = false;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.GetComponent<PlayerControl>()) {
            OnCollide(col);
        }
    }

    protected virtual void OnCollide(Collider2D col) {
        Destroy(this.GetComponent<BoxCollider2D>());
        OnDoorExit();
    }

    protected virtual void OnDoorExit() {
        DispatchMessage("OnPlayerExittedDoor", this);
        OpenDoor();
    }

    public Collider2D GetCollider() {
        return this.collider2D;
    }

    public void OpenDoor() {
        door = transform.GetComponentInChildren<Animation2D>();
        if(door)
            door.Play();
        doorIsOpen = true;
    }

    public void CloseDoor() {
        door = transform.GetComponentInChildren<Animation2D>();
        if(door)
            door.Play(true, true);
        doorIsOpen = false;
    }
}
