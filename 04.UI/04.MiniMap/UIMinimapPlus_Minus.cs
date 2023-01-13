using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIMinimapPlus_Minus : MonoBehaviour
{
	[SerializeField]
	private Camera FirstTown_MiniMapCamera;//FirstTown_MiniMapCamera        <-//�̸�����

	//�߰�    //�ּ� ��ǿ� �ø� ����
	[SerializeField]
	private Camera Forest_MiniMapCamera;//Forest_MiniMapCamera
    //

	[SerializeField]
	private float zoomMin = 15;      // ī�޶��� orthographicSize �ּ� ũ��
	[SerializeField]
	private float zoomMax = 30;     // ī�޶��� orthographicSize �ִ� ũ��
	[SerializeField]
	private float zoomOneStep = 3;  // 1ȸ �� �� �� ����/���ҵǴ� ��ġ
	[SerializeField]
	private TextMeshProUGUI textMapName;

	private void Awake()
	{
		// �� �̸��� ���� �� �̸����� ���� (���ϴ� �̸����� ����)
		//�̴ϸ� �Ʒ��� NewText�� ���� �� �̸����� �ٲ۴�.
		textMapName.text = SceneManager.GetActiveScene().name;
	}

	public void ZoomIn()
	{
		//���� ���� ���� FirstTown�̶��
		if (SceneManager.GetSceneByName("FirstTown").name == "FirstTown")
		{
			// ī�޶��� orthographicSize ���� ���ҽ��� ī�޶� ���̴� �繰 ũ�� Ȯ��
			FirstTown_MiniMapCamera.orthographicSize = Mathf.Max(FirstTown_MiniMapCamera.orthographicSize - zoomOneStep, zoomMin);
		}
		//���� ���� ���� Forest�̶��
		else if (SceneManager.GetSceneByName("Forest").name == "Forest")
		{
			// ī�޶��� orthographicSize ���� ���ҽ��� ī�޶� ���̴� �繰 ũ�� Ȯ��
			Forest_MiniMapCamera.orthographicSize = Mathf.Max(Forest_MiniMapCamera.orthographicSize - zoomOneStep, zoomMin);

		}
	}


	public void ZoomOut()
	{
		//���� ���� ���� FirstTown�̶��
		if (SceneManager.GetSceneByName("FirstTown").name == "FirstTown")
		{
			// ī�޶��� orthographicSize ���� �������� ī�޶� ���̴� �繰 ũ�� ���
			FirstTown_MiniMapCamera.orthographicSize = Mathf.Min(FirstTown_MiniMapCamera.orthographicSize + zoomOneStep, zoomMax);
		}
		//���� ���� ���� Forest�̶��
		else if (SceneManager.GetSceneByName("Forest").name == "Forest")
        {
			// ī�޶��� orthographicSize ���� ���ҽ��� ī�޶� ���̴� �繰 ũ�� Ȯ��
			Forest_MiniMapCamera.orthographicSize = Mathf.Min(Forest_MiniMapCamera.orthographicSize + zoomOneStep, zoomMax);
		}
	}
}