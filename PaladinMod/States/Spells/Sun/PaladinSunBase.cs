﻿using EntityStates;
using RoR2;
using UnityEngine;

namespace PaladinMod.States.Sun
{
	public abstract class PaladinSunBase : BaseState
	{
		[SerializeField]
		public GameObject enterEffectPrefab;

		protected PaladinSunController sunController { get; private set; }

		protected Transform vfxRoot { get; private set; }

		protected virtual bool shouldEnableSunController => true;

		protected abstract float desiredVfxScale { get; }

		public override void OnEnter()
		{
			base.OnEnter();
			sunController = GetComponent<PaladinSunController>();
			sunController.enabled = shouldEnableSunController;
			vfxRoot = base.transform.Find("VfxRoot");
			if ((bool)enterEffectPrefab)
			{
				EffectManager.SimpleImpactEffect(enterEffectPrefab, vfxRoot.position, Vector3.up, transmit: false);
			}
			SetVfxScale(desiredVfxScale);
		}

		public override void Update()
		{
			base.Update();
			SetVfxScale(desiredVfxScale);
		}

		private void SetVfxScale(float newScale)
		{
			newScale = Mathf.Max(newScale, 0.01f);
			if ((bool)vfxRoot && vfxRoot.transform.localScale.x != newScale)
			{
				vfxRoot.transform.localScale = new Vector3(newScale, newScale, newScale);
			}
		}
	}
}