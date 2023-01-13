using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIMinimapPlus_Minus : MonoBehaviour
{
	[SerializeField]
	private Camera FirstTown_MiniMapCamera;//FirstTown_MiniMapCamera        <-//이름변경

	//추가    //주석 노션에 올릴 예정
	[SerializeField]
	private Camera Forest_MiniMapCamera;//Forest_MiniMapCamera
    //

	[SerializeField]
	private float zoomMin = 15;      // 카메라의 orthographicSize 최소 크기
	[SerializeField]
	private float zoomMax = 30;     // 카메라의 orthographicSize 최대 크기
	[SerializeField]
	private float zoomOneStep = 3;  // 1회 줌 할 때 증가/감소되는 수치
	[SerializeField]
	private TextMeshProUGUI textMapName;

	private void Awake()
	{
		// 맵 이름을 현재 씬 이름으로 설정 (원하는 이름으로 설정)
		//미니맵 아래에 NewText를 현재 씬 이름으로 바꾼다.
		textMapName.text = SceneManager.GetActiveScene().name;
	}

	public void ZoomIn()
	{
		//만약 지금 씬이 FirstTown이라면
		if (SceneManager.GetSceneByName("FirstTown").name == "FirstTown")
		{
			// 카메라의 orthographicSize 값을 감소시켜 카메라에 보이는 사물 크기 확대
			FirstTown_MiniMapCamera.orthographicSize = Mathf.Max(FirstTown_MiniMapCamera.orthographicSize - zoomOneStep, zoomMin);
		}
		//만약 지금 씬이 Forest이라면
		else if (SceneManager.GetSceneByName("Forest").name == "Forest")
		{
			// 카메라의 orthographicSize 값을 감소시켜 카메라에 보이는 사물 크기 확대
			Forest_MiniMapCamera.orthographicSize = Mathf.Max(Forest_MiniMapCamera.orthographicSize - zoomOneStep, zoomMin);

		}
	}


	public void ZoomOut()
	{
		//만약 지금 씬이 FirstTown이라면
		if (SceneManager.GetSceneByName("FirstTown").name == "FirstTown")
		{
			// 카메라의 orthographicSize 값을 증가시켜 카메라에 보이는 사물 크기 축소
			FirstTown_MiniMapCamera.orthographicSize = Mathf.Min(FirstTown_MiniMapCamera.orthographicSize + zoomOneStep, zoomMax);
		}
		//만약 지금 씬이 Forest이라면
		else if (SceneManager.GetSceneByName("Forest").name == "Forest")
        {
			// 카메라의 orthographicSize 값을 감소시켜 카메라에 보이는 사물 크기 확대
			Forest_MiniMapCamera.orthographicSize = Mathf.Min(Forest_MiniMapCamera.orthographicSize + zoomOneStep, zoomMax);
		}
	}
}