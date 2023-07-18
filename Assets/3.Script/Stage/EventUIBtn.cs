using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventUIBtn : MonoBehaviour
{
    [Header("[OnOff Btn UI]")]
    [SerializeField] Button onoffBtn;
    [SerializeField] GameObject onoffBtnImg;

    [Header("[Event UI]")]
    [SerializeField] GameObject eventUI;
    [SerializeField] GameObject voteUI;
    [SerializeField] GameObject rolePlayingUI;

    private void Start()
    {
        LoadingBtn();
    }
    public void OffBtn()
    {
        onoffBtnImg.transform.rotation = Quaternion.Euler(0, 0, 180);
        
        eventUI.SetActive(false);

        onoffBtn.onClick.RemoveAllListeners();
        onoffBtn.onClick.AddListener(OnBtn);
    }
    public void OnBtn()
    {
        onoffBtnImg.transform.rotation = Quaternion.Euler(0, 0, 0);

        eventUI.SetActive(true);

        onoffBtn.onClick.RemoveAllListeners();
        onoffBtn.onClick.AddListener(OffBtn);
    }
    public void LoadingBtn()
    {
        OffBtn();
        onoffBtn.interactable = false;
    }
    public void VoteBtn()
    {
        voteUI.SetActive(true);
        rolePlayingUI.SetActive(false);

        OnBtn();
        onoffBtn.interactable = true;
    }
    public void NightBtn()
    {
        voteUI.SetActive(false);
        rolePlayingUI.SetActive(true);

        OnBtn();
        onoffBtn.interactable = true;
    }
    public void RolePlayingBtn()
    {
        OnBtn();
        onoffBtn.interactable = false;
    }
    public void Die()
    {
        onoffBtn.interactable = false;
    }
}
