using System;
using DG.Tweening;
using UnityEngine;

namespace Main.UI.Screens.CharacterSelection {
	public class CharacterModel : MonoBehaviour {
		[SerializeField] private float animationDuration = 0.2f;

		private void Start() => PlayShowAnimation();
		public Tween PlayHideAnimation(Action onComplete = null) => DOTween
			.Sequence()
			.Join(transform.DOScale(0, animationDuration).SetEase(Ease.InBack))
			.Join(transform.DOLocalMoveY(-1, animationDuration).SetEase(Ease.InBack))
			.OnComplete(() => {
				onComplete?.Invoke();
				Destroy(gameObject);
			});
		private void PlayShowAnimation() => DOTween
			.Sequence()
			.Join(transform.DOScale(1, animationDuration).From(0).SetEase(Ease.OutBack))
			.Join(transform.DOLocalMoveY(0, animationDuration).From(-1).SetEase(Ease.OutBack));
	}
}