using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalController : MonoBehaviour
{
    [SerializeField] private GameObject _missionNote;
    [SerializeField] private GameObject _progressNote;
    [SerializeField] private bool _isMissionNew = false;
    [SerializeField] private bool _isProgressNew = false;

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_isMissionNew)
            _missionNote.SetActive(true);
        else
            _missionNote.SetActive(false);
        if (_isProgressNew)
            _progressNote.SetActive(true);
        else
            _progressNote.SetActive(false);
    }
    public void MissionClicked()
    {
        Debug.Log("Mission Clicked");
    }
    public void HandbookClicked()
    {
        Debug.Log("Handbook Clicked");
    }

    public void ProgressClicked()
    {
        Debug.Log("Progress Clicked");
    }

    public void CommunityClicked()
    {
        Debug.Log("Community Clicked");
    }
}
