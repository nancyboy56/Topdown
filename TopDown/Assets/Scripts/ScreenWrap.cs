using UnityEngine;

// Attach this script to your player GameObject.
// When the player moves past any edge of the screen, they reappear on the opposite side —
// just like the classic arcade game Asteroids.
public class ScreenWrap : MonoBehaviour
{
    // A reference to the scene's main camera.
    // We need this to figure out how big the visible world area is.
    private Camera _cam;

    // Half the width and height of the visible world (in world units, not pixels).
    // We store "half" values because Unity's world origin (0,0) sits at the screen centre,
    // so the left edge is at -_halfW and the right edge is at +_halfW.
    private float _halfW, _halfH;

    // Start() is called once when the game begins, before the first frame.
    void Start()
    {
        // Grab the camera tagged "Main Camera" in the scene.
        _cam = Camera.main;

        // Calculate the world-space bounds of the screen straight away.
        UpdateBounds();
    }

    // Works out how far the screen edges are in world units.
    // This only needs to run once (at Start) because the camera size doesn't change.
    // If your camera could resize at runtime (e.g. split-screen), call this every frame instead.
    void UpdateBounds()
    {
        // ScreenToWorldPoint converts a pixel position on screen into a position in the game world.
        // (0, 0, 0) is the bottom-left corner of the screen in pixels.
        Vector3 bottomLeft = _cam.ScreenToWorldPoint(Vector3.zero);

        // (Screen.width, Screen.height, 0) is the top-right corner of the screen in pixels.
        Vector3 topRight = _cam.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, 0f));

        // The total world width = topRight.x - bottomLeft.x.
        // We halve it because the camera is centred at the world origin,
        // so each side extends exactly half the total width from centre.
        _halfW = (topRight.x - bottomLeft.x) / 2f;
        _halfH = (topRight.y - bottomLeft.y) / 2f;
    }

    // LateUpdate() runs every frame, but AFTER all other Update() calls have finished.
    // This is important — it means the player's movement script has already moved the player
    // this frame, and now we check whether they've gone off screen.
    void LateUpdate()
    {
        // Read the player's current position into a local variable.
        // Modifying a local copy first is slightly more efficient than
        // reading/writing transform.position multiple times.
        Vector3 pos = transform.position;

        // --- Horizontal wrap ---
        // If the player has moved past the right edge, teleport them to the left edge.
        if (pos.x > _halfW)  pos.x = -_halfW;

        // If the player has moved past the left edge, teleport them to the right edge.
        if (pos.x < -_halfW) pos.x =  _halfW;

        // --- Vertical wrap ---
        // If the player has moved past the top edge, teleport them to the bottom edge.
        if (pos.y > _halfH)  pos.y = -_halfH;

        // If the player has moved past the bottom edge, teleport them to the top edge.
        if (pos.y < -_halfH) pos.y =  _halfH;

        // Write the (potentially modified) position back to the player's transform.
        // Nothing visually changes if the player was still on screen —
        // the position values just get reassigned to what they already were.
        transform.position = pos;
    }
}