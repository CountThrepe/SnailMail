using UnityEngine;

public abstract class DragDrop : MonoBehaviour
{
    private bool dragable, dragging;
    private Vector2 originalPos;
    private Bounds cameraBounds;
    protected int index;

    void Awake() {
        dragable = true;
        dragging = false;
        originalPos = new Vector2();

        // Set camera bounds
        float camY = Camera.main.orthographicSize * 2;
        float camX = camY * Screen.width / Screen.height;
        Vector2 extents = new Vector2(camX, camY);
        Vector2 size = Vector2.Scale(GetComponent<BoxCollider2D>().size, transform.localScale);
        cameraBounds = new Bounds((Vector2) Camera.main.transform.position, extents - size);
    }

    public void SetDragEnabled(bool enabled) {
        dragable = enabled;
    }
    
    private Vector2 GetMouseWorldPosition() {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return cameraBounds.ClosestPoint(pos);
    }

    protected void Update() {
        if(dragging) {
            transform.position = GetMouseWorldPosition();
        }
    }

    public void OnMouseDown() {
        if(dragable) {
            dragging = true;
            originalPos = transform.position;
        }
    }

    public void OnMouseUp() {
        if(dragging) {
            dragging = false;
            if(!OnDrop()) transform.position = originalPos;
        }
    }

    public void SetIndex(int i) {
        index = i;
    }

    protected abstract bool OnDrop();
}
