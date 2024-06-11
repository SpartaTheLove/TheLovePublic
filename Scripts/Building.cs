using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public ItemData HouseData { get; set; }
    private GameObject buildingPreview; // 건설 미리보기 오브젝트

    private void Update()
    {
        UpdateBuildingPreviewPosition();
        CheckForBuildingPlacement();
        CheckForBuildingCancellation();
    }

    private void UpdateBuildingPreviewPosition()
    {
        if (buildingPreview == null)
        {
            return;
        }

        // 마우스 위치로 건물 미리보기 위치 설정
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int groundLayerMask = LayerMask.GetMask("Ground");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayerMask))
        {
            buildingPreview.transform.position = hit.point;
        }
    }

    private void CheckForBuildingPlacement()
    {
        // 왼쪽 클릭으로 건물 건설
        if (Input.GetMouseButtonDown(0))
        {
            // 현재 건물 미리보기가 있는지 확인
            if (buildingPreview != null)
            {
                // 건물 건설
                BuildHouse(buildingPreview.transform.position);
            }
        }
    }

    private void CheckForBuildingCancellation()
    {
        // 우클릭으로 건물 건설 취소
        if (Input.GetMouseButtonDown(1))
        {
            // 현재 건물 미리보기가 있는지 확인
            if (buildingPreview != null)
            {
                // 건물 건설 취소
                CancelBuildingPlacement();
            }
        }
    }

    private void BuildHouse(Vector3 position)
    {
        // 실제 건물을 건설하기 전에 건물 미리보기 오브젝트를 제거
        Destroy(buildingPreview);

        // 건물을 생성하고 위치 설정
        GameObject house = Instantiate(HouseData.dropPrefab, position, Quaternion.identity);
    }

    public void StartBuildingPlacement()
    {
        GameObject UIInventory = CharacterManager.Instance.Player.Inventory.UIInventory.gameObject;
        ButtonManager.Instance.OnClickCancel(UIInventory);

        // 건물 미리보기 오브젝트를 생성하고 활성화
        if (buildingPreview == null && HouseData != null && HouseData.dropPrefab != null)
        {
            buildingPreview = Instantiate(HouseData.dropPrefab);
            buildingPreview.SetActive(true);
        }
    }

    public void CancelBuildingPlacement()
    {
        // 건물 건설 취소
        if (buildingPreview != null)
        {
            Destroy(buildingPreview);
        }
    }
}
