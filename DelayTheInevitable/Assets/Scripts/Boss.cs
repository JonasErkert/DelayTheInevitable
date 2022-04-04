using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss : MonoBehaviour
{
	[SerializeField] private GameObject door;
	[SerializeField] private GameObject bossRotationObject;
	[SerializeField] private AudioClip walkAudioClip;
	[SerializeField] private AudioClip doorOpenAudioClip;
	[SerializeField] private AudioClip doorCloseAudioClip;
	
	[Header("Wait Durations")]
	[SerializeField] private float doorOpenTimeStart = 60f;
	[SerializeField] private float randomDeviationPercent = 10f;
	[SerializeField] private float bossVisibleDuration = 4f;
	[SerializeField] private float bossUntilDisappearDuration = 1f;
	[SerializeField] private float minBossPause = 30f;

    private Animator _doorAnimator;
	private Animator _bossAnimator;
	private AudioSource _doorAudioSource;
	private float _doorAudioLength;
	private float _doorCloseAudioLength;
	private float _walkAudioLength;
	private float _appearancePause;
	private float _timeMultiplier = 1f;
	
	public bool hasBossAppeared = false;

	// Start is called before the first frame update
	void Start()
	{
		_doorAnimator = door.GetComponent<Animator>();
		_bossAnimator = bossRotationObject.GetComponent<Animator>();
		_doorAudioSource = door.GetComponent<AudioSource>();
		_doorAudioLength = doorOpenAudioClip.length;
		_doorCloseAudioLength = doorCloseAudioClip.length;
		_walkAudioLength = walkAudioClip.length;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyUp(KeyCode.A))
		{
			StartBossDoor();
		}
	}

	public void StartBossDoor()
	{
		CalcBossAppearancePause();
		StartCoroutine(WaitBossAppearanceLoop());
	}
	
	public void ResetBossDoor()
	{
		StopAllCoroutines();
	}

	private void PlayBossSteps()
	{
		_doorAudioSource.clip = walkAudioClip;
		_doorAudioSource.Play();
	}

	private void ToggleBossRotation(bool isAppearing)
	{
		_bossAnimator.SetTrigger(isAppearing ? "Open" : "Close");
	}

	private void ToggleDoor(bool isOpen)
	{
		_doorAnimator.SetTrigger(isOpen ? "Open" : "Close");
		_doorAudioSource.clip = isOpen ? doorOpenAudioClip : doorCloseAudioClip;
		_doorAudioSource.Play();
	}

	private void CalcBossAppearancePause()
	{
		float randomRange = doorOpenTimeStart * randomDeviationPercent / 100;
		_timeMultiplier = 1 - GameManager.Instance.Difficulty;
		_appearancePause = doorOpenTimeStart * _timeMultiplier + Random.Range(-randomRange, randomRange);

		_appearancePause = Mathf.Clamp(_appearancePause, minBossPause, 9999f);
	}

	private IEnumerator WaitBossAppearanceLoop()
	{
		yield return new WaitForSeconds(_appearancePause);
		PlayBossSteps();
		yield return new WaitForSeconds(_walkAudioLength);
		ToggleDoor(true);
		yield return new WaitForSeconds(_doorAudioLength);
		ToggleBossRotation(true);
		hasBossAppeared = true;
		yield return new WaitForSeconds(bossVisibleDuration);
		ToggleBossRotation(false);
		hasBossAppeared = false;
		yield return new WaitForSeconds(bossUntilDisappearDuration);
		ToggleDoor(false);
		yield return new WaitForSeconds(_doorCloseAudioLength);
		PlayBossSteps();
		yield return new WaitForSeconds(_walkAudioLength);
		
		// Repeat
		CalcBossAppearancePause();
		StartCoroutine(WaitBossAppearanceLoop());
	}
}
