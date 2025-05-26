using DG.Tweening;
using Main.Services;
using UnityEngine;

namespace Main.UI.Screens {
	public abstract class Screen : MonoBehaviour {
		protected ScreenNavigator ScreenNavigator;

		[SerializeField] private float animationDuration = 0.2f;
		[SerializeField] private CanvasGroup canvasGroup;
		[SerializeField] private RectTransform containerTransform;
		
		[field:SerializeField] public ScreenType ScreenType { get; private set; }

		public void Construct(ScreenNavigator screenNavigator) => ScreenNavigator = screenNavigator;
		public void Open() => DOTween
			.Sequence()
			.Join(DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1, animationDuration).From(0))
			.Join(containerTransform.DOScale(1, animationDuration).From(0.8f).SetEase(Ease.OutBack))
			.Join(containerTransform.DOAnchorPos(Vector2.zero, animationDuration).From(new Vector2(0, -UnityEngine.Screen.height / 4f)).SetEase(Ease.OutBack));
		public void Close() {
			DOTween.KillAll();
			DOTween
				.Sequence()
				.Join(DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, animationDuration))
				.Join(containerTransform.DOScale(0.8f, animationDuration).SetEase(Ease.InBack))
				.Join(containerTransform.DOAnchorPos(new Vector2(0, -UnityEngine.Screen.height / 4f), animationDuration).SetEase(Ease.InBack))
				.OnComplete(() => Destroy(gameObject));
		}
	}
}