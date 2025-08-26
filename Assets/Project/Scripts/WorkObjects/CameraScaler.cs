using UnityEngine;

namespace Project.Scripts.WorkObjects
{
    public class CameraScaler : MonoBehaviour
    {
        private const float TargetAspect = 3f / 4f;
        
        [SerializeField] private Camera _camera;

        private void Awake()
        {
            float currentAspect = (float)Screen.width / Screen.height;

            if (currentAspect >= TargetAspect) 
                return;
            
            float scale = TargetAspect / currentAspect;
            _camera.orthographicSize *= scale;
        }
    }
}