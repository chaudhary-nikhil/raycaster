using UnityEngine;
using Assets.Scripts;


/**
 * This is to help use the mouse to position 3D objects in the world in real time.
 * It poses the 3D object such that it points out of the camera and into the scene based on the mouse position.
 * This can be helpful to drive the motion controllers as if they're positioned with the mouse when testing things
 * outside of VR on a device with a mouse.
 */
public class MousePoseDriver : MonoBehaviour
{
    // The offset relative to what would be normally returned as the ScreenPointToRay position
    // Can help put an object behind the camera near plane so it's not obstructing your view
    public Vector3 offset;

    // The rotation offset relative to the ray direction
    public Quaternion rotationOffset;

    // The camera we're projecting from, leave as unset to use Camera.main
    public Camera sourceCamera;

    public IMousePositionProvider mousePositionProvider = null;

    private void Start()
    {
        // = new DeviceMousePositionProvider();
        //mousePositionProvider = new MockMousePositionProvider();
        mousePositionProvider = new NetworkMousePositionProvider();
    }

    
    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("Contacted \n");
    }

    void Update()
    {
        
        if(mousePositionProvider == null)
        {
            return;
        }

        if (sourceCamera == null)
        {
            sourceCamera = Camera.main;
        }

        //float distanceFromCamera = offset.z + sourceCamera.nearClipPlane;
        float distanceFromCamera = -1.0f;

        // TODO: replace actual mouse input with mock data to test on device
        //Vector2 mousePos = Input.mousePosition;
        Vector2 mousePos = (Vector2)(mousePositionProvider?.GetMousePosition());
        //Debug.Log(mousePos.x + "," + mousePos.y);
        //Vector2 mousePos = MockMousePositionProvider.GetMousePosition();

        // Convert world space offset to screen space so the mouse coords are offset correctly
        if (offset.x != .0f || offset.y != .0f)
        {
            Vector3 worldLeftBottom = sourceCamera.ViewportToWorldPoint(new Vector3(.0f, .0f, distanceFromCamera));
            Vector2 viewportRightTop = sourceCamera.ViewportToScreenPoint(new Vector2(1.0f, 1.0f));

            if (offset.x != .0f)
            {
                Vector3 worldRightBottom = sourceCamera.ViewportToWorldPoint(new Vector3(1.0f, 0.0f, distanceFromCamera));
                Vector3 worldHorizontal = worldRightBottom - worldLeftBottom;
                float worldHorizontalDistance = worldHorizontal.magnitude;
                mousePos.x += viewportRightTop.x * (offset.x / worldHorizontalDistance);
            }

            if (offset.y != .0f)
            {
                Vector3 worldLeftTop = sourceCamera.ViewportToWorldPoint(new Vector3(0.0f, 1.0f, distanceFromCamera));
                Vector3 worldVertical = worldLeftTop - worldLeftBottom;
                float worldVerticalDistance = worldVertical.magnitude;
                mousePos.y += viewportRightTop.y * (offset.y / worldVerticalDistance);
            }
        }

        transform.position = sourceCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, distanceFromCamera));
        Vector3 farPoint = sourceCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, distanceFromCamera + 2.0f));

        // Safe to assume this is normalized because farPoint is exactly 1 away
        Vector3 direction = farPoint - transform.position;
        direction = Vector3.Normalize(direction);

       //transform.rotation = Quaternion.LookRotation(direction, sourceCamera.transform.TransformDirection(Vector3.up)) * rotationOffset;
        transform.rotation = Quaternion.LookRotation(direction, sourceCamera.transform.TransformDirection(Vector3.up));

        /*Ray rayHighlight = sourceCamera.ViewportPointToRay(direction);
        RaycastHit hit;
        if(Physics.Raycast(rayHighlight, out hit))
        {
            print("I'm looking at " + hit.transform.name);
        }
*/
        //HighlightObjectInCenterOfCam(direction);
    }
}