using UnityEngine;

namespace Main.UI.CharactersScreen {
	public class CharacterRotator : MonoBehaviour {
		[SerializeField] private float speed;

		private void Update() => transform.Rotate(Vector3.up * Time.deltaTime * speed);
	}
}