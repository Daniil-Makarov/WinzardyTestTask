using UnityEngine;

namespace Main.UI.Screens.CharacterSelection {
	[CreateAssetMenu(fileName = nameof(CharactersConfig), menuName = "Configs/Characters/CharactersConfig")]
	public class CharactersConfig : ScriptableObject {
		public Character[] Characters;
	}
}