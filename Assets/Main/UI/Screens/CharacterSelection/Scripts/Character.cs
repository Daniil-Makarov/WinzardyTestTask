using UnityEngine;

namespace Main.UI.Screens.CharacterSelection {
	[CreateAssetMenu(fileName = nameof(Character), menuName = "Configs/Characters/Character")]
	public class Character : ScriptableObject {
		public Sprite Icon;
		public CharacterModel Prefab;
		[Range(0, 100)] public int Experience;
	}
}