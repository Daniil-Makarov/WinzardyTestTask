using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Main.UI.Screens.CharacterSelection {
	public class CharacterSelectionScreen : Screen {
		[SerializeField] private CharactersConfig characters;
		[SerializeField] private CharacterElement characterElementPrefab;
		[SerializeField] private Transform selectedCharacterContainer;
		[SerializeField] private Transform characterElementsContainer;
		[SerializeField] private Button backButton;
		[SerializeField] private Animator backButtonAnimator;
		private (CharacterModel model, CharacterElement element) selected;
		private Tween switchModelTween;

		private void Start() {
			const float AnimationDelay = 0.1f;
			Sequence sequence = DOTween.Sequence().Pause();
			
			for (int i = 0; i < characters.Characters.Length; i++) {
				CharacterElement element = CreateElement(characters.Characters[i]);
				AppendElementShowAnimation(element, sequence, AnimationDelay, selectOnComplete: i == 0);
			}
			
			AppendBackButtonShowAnimation(sequence, AnimationDelay);
		}
		private void OnEnable() => backButton.onClick.AddListener(Back);
		private void OnDisable() => backButton.onClick.RemoveListener(Back);
		private void Back() => ScreenNavigator.CloseCurrentScreen();
		public void Select(CharacterElement characterElement) {
			if (selected.element == characterElement || switchModelTween != null && switchModelTween.IsActive()) return;

			if (selected.model && selected.element) {
				selected.element.PlayDeselectAnimation();
				switchModelTween = selected.model.PlayHideAnimation(onComplete: () => SetNewSelectedCharacter(characterElement));
			}
			else {
				SetNewSelectedCharacter(characterElement);
			}
		}
		private CharacterElement CreateElement(Character character) {
			CharacterElement element = Instantiate(characterElementPrefab, characterElementsContainer);
			element.Initialize(character, this);
			return element;
		}
		private void AppendElementShowAnimation(CharacterElement element, Sequence sequence, float animationDelay, bool selectOnComplete) {
			Tween showTween = element.GetShowTween();
			if (selectOnComplete) showTween.OnComplete(() => Select(element));
			sequence
				.AppendInterval(animationDelay)
				.AppendCallback(() => showTween.Play());
		}
		private void AppendBackButtonShowAnimation(Sequence sequence, float animationDelay) {
			backButtonAnimator.enabled = false;
			
			Tween backButtonShowAnimation = backButton.transform
				.DOScale(1, AnimationDuration)
				.From(0)
				.SetEase(Ease.OutBack)
				.OnComplete(() => backButtonAnimator.enabled = true)
				.Pause();

			sequence
				.AppendInterval(animationDelay)
				.AppendCallback(() => backButtonShowAnimation.Play())
				.Play();
		}
		private void SetNewSelectedCharacter(CharacterElement characterElement) {
			selected.model = Instantiate(characterElement.Config.Prefab, selectedCharacterContainer);
			selected.element = characterElement;

			characterElement.PlaySelectAnimation();
		}
	}
}