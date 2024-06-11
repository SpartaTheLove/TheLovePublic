using TMPro;
using UnityEngine;

public class Temple : MonoBehaviour
{
    [SerializeField] private GameObject _prayBtn;
    [SerializeField] private GameObject _prayText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            _prayText.SetActive(true);
            _prayBtn.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            _prayText.SetActive(false);
            _prayBtn.SetActive(false);
        }
    }

    public void OnclickPrayBtn()
    {
        EndingManager.Instance.CheckEnding(EndingType.Hidden);
    }
}
