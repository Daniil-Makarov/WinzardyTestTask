using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Main.UI.Screens.MainMenu {
	public class MainMenuScreen : Screen {
		[SerializeField] private Button settingsButton;
		[SerializeField] private Animator settingsButtonAnimator;
		[SerializeField] private Button playButton;
		[SerializeField] private Button characterSelectionButton;
		[SerializeField] private Button exitButton;

		private void Start() {
			settingsButtonAnimator.enabled = false;
			
			DOTween
				.Sequence()
				.AppendInterval(AnimationDuration)
				.Append(exitButton.transform.DOScale(1, AnimationDuration).From(0).SetEase(Ease.OutBack))
				.Append(characterSelectionButton.transform.DOScale(1, AnimationDuration).From(0).SetEase(Ease.OutBack))
				.Append(playButton.transform.DOScale(1, AnimationDuration).From(0).SetEase(Ease.OutBack))
				.Append(settingsButton.transform.DOScale(1, AnimationDuration).From(0).SetEase(Ease.OutBack)
					.OnComplete(() => settingsButtonAnimator.enabled = true));
		}
		private void OnEnable() {
			characterSelectionButton.onClick.AddListener(OpenCharacterSelection);
			exitButton.onClick.AddListener(Application.Quit);
		}
		private void OnDisable() {
			characterSelectionButton.onClick.RemoveListener(OpenCharacterSelection);
			exitButton.onClick.RemoveListener(Application.Quit);
		}
		private void OpenCharacterSelection() => ScreenNavigator.Open(ScreenType.CharacterSelection);
	}
}