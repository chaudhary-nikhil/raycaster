
using UnityEngine;

namespace Assets.Scripts
{
    class DeviceMousePositionProvider : IMousePositionProvider
    {
        public Vector3 GetMousePosition()
        {
            return Input.mousePosition;
        }
    }
}
