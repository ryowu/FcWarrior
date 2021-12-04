using UnityEngine;
using UnityEngine.UI;

public class InfoLabelController : MonoBehaviour
{
	[SerializeField] private GameObject info;
	[SerializeField] private Text lblInfo;
	[SerializeField] private Image imgInfo;
	[SerializeField] private string InfoText;

	private bool startFadeOut;
	private float alpha;

	// Start is called before the first frame update
	void Start()
	{
		startFadeOut = false;
		alpha = 1;
	}

	// Update is called once per frame
	void Update()
	{
		if (startFadeOut)
		{
			alpha -= 0.5f * Time.deltaTime;
			if (alpha < 0)
			{
				info.SetActive(false);
				ResetUI();
			}
			else
			{
				imgInfo.color = new Color(imgInfo.color.r, imgInfo.color.g, imgInfo.color.b, alpha);
				lblInfo.color = new Color(lblInfo.color.r, lblInfo.color.g, lblInfo.color.b, alpha);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.gameObject.CompareTag("Player")) return;

		ResetUI();
		
		lblInfo.text = InfoText;
		info.SetActive(true);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (!collision.gameObject.CompareTag("Player")) return;
		startFadeOut = true;
	}

	private void ResetUI()
	{
		startFadeOut = false;
		alpha = 1f;
		imgInfo.color = new Color(255f, 255f, 255f, 1f);
		lblInfo.color = new Color(255f, 255f, 255f, 1f);
	}
}
