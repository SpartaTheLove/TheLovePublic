using UnityEngine;

public class Bow : MonoBehaviour
{
    [System.Serializable]
    public class BowSettings
    {
        [Header("Arrow Settings")]
        public float arrowCount;
        public GameObject arrowPrefab;
        public Transform arrowPos;
        public Transform arrEquipParent;
        public float arrowForce = 20;

        [Header("Bow Stirng Transform")]
        public Transform bowString;
        public Transform stringInitialPos;
        public Transform stringHandPullPos;
        public Transform stringInitialParent;

    }

    [SerializeField]
    public BowSettings bowSettings;

    [Header("Crosshair Settings")]
    public GameObject crossHairPrefab;
    GameObject currentCrossHair;

    public bool getArrow;

    private void Awake()
    {
        bowSettings.stringHandPullPos = transform.parent;
        bowSettings.arrowPos = CharacterManager.Instance.Player.PlayerEquipTransform.ArrowPos;
        bowSettings.arrEquipParent = CharacterManager.Instance.Player.PlayerEquipTransform.ArrowEquipParent;
    }

    public void PickArrow()
    {
        bowSettings.arrowPos.gameObject.SetActive(true);
    }

    public void DisableArrow()
    {
        bowSettings.arrowPos.gameObject.SetActive(false);
    }

    public void PullString()
    {
        bowSettings.bowString.transform.position = bowSettings.stringHandPullPos.gameObject.transform.position;
        //bowSettings.bowString.transform.parent = bowSettings.stringHandPullPos;
    }

    public void ReleaseString()
    {
        bowSettings.bowString.transform.position = bowSettings.stringInitialPos.position;
        //bowSettings.bowString.transform.parent = bowSettings.stringInitialPos;
    }

    public void ShowCrosshair(Vector3 crosshairPos)
    {
        if (!currentCrossHair)
            currentCrossHair = Instantiate(crossHairPrefab) as GameObject;
        currentCrossHair.transform.position = crosshairPos;
        currentCrossHair.transform.LookAt(Camera.main.transform);
    }
    public void RemoveCrosshair()
    {
        if (currentCrossHair)
        {
            Destroy(currentCrossHair);
        }
    }

    public void Fire(Vector3 hitPoint)
    {
        Vector3 dir = (hitPoint - bowSettings.arrowPos.position).normalized;
        GameObject arrowInstance = Instantiate(bowSettings.arrowPrefab, bowSettings.arrowPos.position, bowSettings.arrowPos.rotation).gameObject;

        Arrow arrowScript = arrowInstance.GetComponent<Arrow>();
        if (arrowScript != null)
        {
            arrowScript.Fire(dir, bowSettings.arrowForce);
            SoundManager.Instance.Play(1, 0.2f);
        }
    }
}