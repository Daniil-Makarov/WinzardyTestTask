using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Main.UI.Screens.CharacterSelection {
	public class CharacterElement : MonoBehaviour {
		[SerializeField] private float animationDuration = 0.2f;
		[SerializeField] private RectTransform container;
		[SerializeField] private Image frame;
		[SerializeField] private Image icon;
		[SerializeField] private Slider experienceSlider;
		[SerializeField] private Button button;
		private CharacterSelectionScreen characterSelectionScreen;
		private Sequence showTween;
		private Sequence selectTween;
		private Tween deselectTween;
		private readonly Color selectedFrameColor = new (0.733f, 1, 0.6f);
		private readonly Color defaultFrameColor = Color.white;

		public Character Config { get; private set; }

		private void Start() {
			selectTween = DOTween
				.Sequence()
				.Join(container
					.DOScale(1.1f, animationDuration)
					.SetEase(Ease.OutBack)
					.OnComplete(() => container
						.DOScale(1f, animationDuration)
						.SetEase(Ease.InOutQuad)))
				.Join(frame.DOColor(selectedFrameColor, animationDuration))
				.SetAutoKill(false)
				.Pause();
			
			deselectTween = frame
				.DOColor(defaultFrameColor, animationDuration)
				.SetAutoKill(false)
				.Pause();
		}
		private void OnEnable() => button.onClick.AddListener(SelectCharacter);
		private void OnDisable() => button.onClick.RemoveListener(SelectCharacter);
		public void Initialize(Character config, CharacterSelectionScreen characterSelectionScreen) {
			Config = config;
			this.characterSelectionScreen = characterSelectionScreen;
			icon.sprite = config.Icon;
		}
		public Tween GetShowTween() {
			showTween = DOTween
				.Sequence()
				.Join(container.DOScale(1, animationDuration).From(0).SetEase(Ease.OutBack))
				.Join(container.DORotate(Vector3.zero, animationDuration).From(new Vector3(0, 0, -180)).SetEase(Ease.OutBack))
				.Append(DOTween.To(() => experienceSlider.value, x => experienceSlider.value = x, Config.Experience, animationDuration).From(0)
					.SetEase(Ease.OutQuad))
				.Pause();
			
			return showTween;
		}
		public void PlaySelectAnimation() {
			FinishActiveAnimations();
			selectTween.Restart();
		}
		public void PlayDeselectAnimation() {
			FinishActiveAnimations();
			deselectTween.Restart();
		}
		private void SelectCharacter() => characterSelectionScreen.Select(this);
		private void FinishActiveAnimations() {
			if (showTween != null && showTween.IsActive()) showTween.Complete();
			if (selectTween != null && selectTween.IsActive()) selectTween.Complete();
			if (deselectTween != null && deselectTween.IsActive()) deselectTween.Complete();
		}
	}
}