using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
	[SerializeField] private AudioSource _audioSource;

	private static MusicPlayer instance = null;
	public static MusicPlayer Instance
	{
		get { return instance; }
	}
	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
	// any other methods you need

	public void Play()
	{
		_audioSource.Play();
	}

	public void Stop()
	{
		_audioSource.Stop();
	}
}