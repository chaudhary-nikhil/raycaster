using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class MockMousePositionProvider : IMousePositionProvider
    {
        ulong tick = 0;
        static int PERIOD = 60 * 10;
        public Vector3 GetMousePosition()
        {
            var x = 1500f * Mathf.Sin((2f * Mathf.PI) / PERIOD * tick++);
            var y = 1500f * Mathf.Cos((2f * Mathf.PI) / PERIOD * tick++);
            return new Vector3(x, y , 0);
        }
    }
}
