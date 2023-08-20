
using UnityEngine;

namespace Assets.Scripts
{
    class NetworkMousePositionProvider : IMousePositionProvider
    {
        private WS_Client client_script = new();
        
        public Vector3 GetMousePosition()
        {
                      
            return new  Vector3((int)client_script.xCoord + 700, (int)client_script.yCoord + 587, 0);
        }
    }
}
