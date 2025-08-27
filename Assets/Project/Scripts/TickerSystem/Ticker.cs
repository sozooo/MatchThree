using Project.Scripts.TickerSystem.BallSpawningSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Project.Scripts.TickerSystem
{
    public class Ticker : MonoBehaviour
    {
        [SerializeField] private TickerRotator _rotator;
        [SerializeField] private BallDropper _ballDropper;
        [Inject] private PlayerInput _playerInput;

        private void Awake()
        {
            _ballDropper.Initialize();
        }

        private void OnEnable()
        {
            _playerInput.Enable();
            _rotator.Start();

            _playerInput.Pointer.Click.performed += HandleClick;
        }

        private void OnDisable()
        {
            _playerInput.Disable();
            _rotator.Stop();

            _playerInput.Pointer.Click.performed -= HandleClick;
        }

        private void HandleClick(InputAction.CallbackContext context)
        {
            _ballDropper.Drop();
        }
    }
}