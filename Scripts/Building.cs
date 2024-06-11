using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public ItemData HouseData { get; set; }
    private GameObject buildingPreview; // �Ǽ� �̸����� ������Ʈ

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

        // ���콺 ��ġ�� �ǹ� �̸����� ��ġ ����
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
        // ���� Ŭ������ �ǹ� �Ǽ�
        if (Input.GetMouseButtonDown(0))
        {
            // ���� �ǹ� �̸����Ⱑ �ִ��� Ȯ��
            if (buildingPreview != null)
            {
                // �ǹ� �Ǽ�
                BuildHouse(buildingPreview.transform.position);
            }
        }
    }

    private void CheckForBuildingCancellation()
    {
        // ��Ŭ������ �ǹ� �Ǽ� ���
        if (Input.GetMouseButtonDown(1))
        {
            // ���� �ǹ� �̸����Ⱑ �ִ��� Ȯ��
            if (buildingPreview != null)
            {
                // �ǹ� �Ǽ� ���
                CancelBuildingPlacement();
            }
        }
    }

    private void BuildHouse(Vector3 position)
    {
        // ���� �ǹ��� �Ǽ��ϱ� ���� �ǹ� �̸����� ������Ʈ�� ����
        Destroy(buildingPreview);

        // �ǹ��� �����ϰ� ��ġ ����
        GameObject house = Instantiate(HouseData.dropPrefab, position, Quaternion.identity);
    }

    public void StartBuildingPlacement()
    {
        GameObject UIInventory = CharacterManager.Instance.Player.Inventory.UIInventory.gameObject;
        ButtonManager.Instance.OnClickCancel(UIInventory);

        // �ǹ� �̸����� ������Ʈ�� �����ϰ� Ȱ��ȭ
        if (buildingPreview == null && HouseData != null && HouseData.dropPrefab != null)
        {
            buildingPreview = Instantiate(HouseData.dropPrefab);
            buildingPreview.SetActive(true);
        }
    }

    public void CancelBuildingPlacement()
    {
        // �ǹ� �Ǽ� ���
        if (buildingPreview != null)
        {
            Destroy(buildingPreview);
        }
    }
}
