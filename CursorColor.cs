using UnityEngine;

namespace ReactVR.CAMP
{
    public class CursorColor : MonoBehaviour
    {
        public Color baseColor = Color.white;
        public Color activeBaseColor = Color.blue;
        public float activeColorFadeTime = 0.25f;
        public Color tipColor = Color.clear;

        private Mesh _mesh;
        private Color[] _colors;
        private float _lastActiveTime;

        void Start()
        {
            _mesh = gameObject.GetComponentInChildren<MeshFilter>().mesh;
            _colors = new Color[_mesh.uv.Length];
            SetColor(1.0f);
        }

        void Update()
        {
           // Fade the cursor if there is no mouse activity

            /*
            _lastActiveTime = Time.time;
           
            SetColor(Mathf.Min(1.0f, (Time.time - _lastActiveTime) / activeColorFadeTime));
            */
        }

        private void SetColor(float fadePct)
        {
            var currentBaseColor = Color.Lerp(activeBaseColor, baseColor, fadePct);
            for (var i = 0; i < _colors.Length; ++i)
            {
                _colors[i] = _mesh.vertices[i].z > 0.0f ? tipColor : currentBaseColor;
            }

            _mesh.colors = _colors;
        }
    }
}
