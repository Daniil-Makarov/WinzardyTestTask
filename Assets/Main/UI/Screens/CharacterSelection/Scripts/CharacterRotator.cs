using UnityEngine;

namespace Main.UI.Screens.CharacterSelection {
	public class CharacterRotator : MonoBehaviour {
		[SerializeField] private float speed;

		private void Update() => transform.Rotate(Vector3.up * Time.deltaTime * speed);
	}
}