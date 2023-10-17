using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class LobbyController : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private TMP_Dropdown rowsDropdown;
    [SerializeField] private TMP_Dropdown columnDropdown;
    
    void Start()
    {
        rowsDropdown.value = GetDropdownIndex(rowsDropdown, GlobalData.rowCountDefault);
        columnDropdown.value = GetDropdownIndex(columnDropdown, GlobalData.columnCountDefault);
        
        rowsDropdown.onValueChanged.AddListener(delegate { OnRowsDropdownValueChanged(); });
        columnDropdown.onValueChanged.AddListener(delegate { OnColumnsDropdownValueChanged(); });
        
        playButton.onClick.AddListener(OnPlayButtonClicked);
    }

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(GlobalData.GAME_SCENE_INDEX);
    }

    public void OnRowsDropdownValueChanged()
    {
        GlobalData.rowCount = int.Parse(rowsDropdown.options[rowsDropdown.value].text);
        Debug.Log("GlobalData.rowCount : "+ GlobalData.rowCount);
    }
    
    public void OnColumnsDropdownValueChanged()
    {
        GlobalData.columnCount = int.Parse(columnDropdown.options[columnDropdown.value].text);
        Debug.Log("GlobalData.columnCount : "+ GlobalData.columnCount);
    }

    public int GetDropdownIndex(TMP_Dropdown dropdown, int item)
    {
        for (int i=0; i<dropdown.options.Count;i++)
        {
            if (int.Parse(dropdown.options[i].text) == item)
            {
                return i;
            }
        }
        return 0;
    }
}
