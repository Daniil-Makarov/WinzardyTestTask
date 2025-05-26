using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Main.Services.Input {
	public class InputHandler : MonoBehaviour {
		public event Action BackPressed;
		
		private InputSystem inputSystem;

		private void Awake() {
			inputSystem = new InputSystem();
			inputSystem.Enable();
		}
		private void OnEnable() => inputSystem.Player.Back.performed += OnBackPerformed;
		private void OnDisable() => inputSystem.Player.Back.performed -= OnBackPerformed;
		private void OnDestroy() => inputSystem.Disable();
		private void OnBackPerformed(InputAction.CallbackContext obj) => BackPressed?.Invoke();
	}
}