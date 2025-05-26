using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Main.UI.Screens {
	[CreateAssetMenu(fileName = nameof(Screens), menuName = "Configs/Screens")]
	public class Screens : ScriptableObject {
		[SerializeField] private Screen[] screens;
		private Dictionary<ScreenType, Screen> screensByType;
	
		public Dictionary<ScreenType, Screen> ScreensByType => screensByType ??= screens.ToDictionary(x => x.ScreenType, x => x);
	}
}