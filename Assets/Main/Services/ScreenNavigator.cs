using System.Collections.Generic;
using Main.Services.Input;
using Main.UI.Screens;
using UnityEngine;
using Screen = Main.UI.Screens.Screen;

namespace Main.Services {
	public class ScreenNavigator : MonoBehaviour {
		[SerializeField] private Transform screenParent;
		[SerializeField] private Screens screens;
		[SerializeField] private InputHandler inputHandler;
		[SerializeField] private ScreenType startScreen;

		private readonly Stack<Screen> history = new ();
		
		private void Start() => Open(startScreen);
		private void OnEnable() => inputHandler.BackPressed += CloseCurrentScreen;
		private void OnDisable() => inputHandler.BackPressed -= CloseCurrentScreen;
		public void Open(ScreenType screenType) {
			if (screenType == ScreenType.Unknown) return;

			Screen newScreen = Instantiate(screens.ScreensByType[screenType], screenParent);
			newScreen.Construct(this);
			newScreen.Open();

			history.Push(newScreen);
		}
		public void CloseCurrentScreen() {
			if (history.Count > 1) history.Pop().Close();
		}
	}
}