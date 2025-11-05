using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondMenuController : MonoBehaviour
{
    public int currentTab = 0;
    public List<GameObject> contentTabs;
    public List<Button> buttons;
    public Sprite buttonActive;
    public Sprite buttonNormal;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < buttons.Count; i ++)
        {
            int tab = i;
            buttons[i].onClick.AddListener(() => { OpenTab(tab); });
        }
    }

    public void OpenTab(int tab)
    {
        currentTab = tab;
        ActiveCurrentButton();
        OpenCurrentTab();
    }

    public void ActiveCurrentButton()
    {
        //inactive all button
        for (int i = 0; i < contentTabs.Count; i++)
        {
            buttons[i].GetComponent<Image>().sprite = buttonNormal;
        }
        //active selected button
        buttons[currentTab].GetComponent<Image>().sprite = buttonActive;
    }

    public void OpenCurrentTab()
    {
        //disable all tab
        for(int i = 0; i < contentTabs.Count; i ++)
        {
            contentTabs[i].SetActive(false);
        }
        //enable selected tab
        contentTabs[currentTab].SetActive(true);
    }

}
