using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ObjectBrowserManager : MonoBehaviour
{
    public Transform listParent;
    public GameObject objectListItemPF;
    public GameObject scrollView;
    public DataCollection data;
    public TextMeshProUGUI hoverTextBox;



    private bool open;

    //public CardGenerator cardGenerator;

    public void PopulatePages()
    {
        foreach (DataPage page in data.DataCollectionList)
        {
            GameObject item = Instantiate(objectListItemPF, listParent);
            item.GetComponentInChildren<TextMeshProUGUI>().text = page.PageName;

            // Set the button text (e.g., name of the page)
            item.GetComponentInChildren<TextMeshProUGUI>().text = page.PageName;

            // Get the button component and add a listener
            Button button = item.GetComponent<Button>();
            if (button != null)
            {
                // Add listener to button to call the method with the specific page as a parameter
                button.onClick.AddListener(() => OnPageButtonClick(page, item.transform.GetSiblingIndex()));
            }
        }
    }



    public void UpdateList()
    {
        PopulatePages();

    }

    public void ClearList()
    {
        foreach (Transform child in listParent)
            Destroy(child.gameObject);
    }

    public void ToggleList()
    {
        open = !open;

        if(open)
        {
            UpdateList();
            scrollView.SetActive(true);
        }
        else
        {
            ClearList();
            scrollView.SetActive(false);
        }

    }

    private void OnPageButtonClick(DataPage page, int buttonChildIndex)
    {
        page.extended = !page.extended;

        if (page.extended)
        {
            int index = buttonChildIndex + 1;
            foreach (DataItem dataItem in page.List)
            {
                GameObject item = Instantiate(objectListItemPF, listParent);
                item.transform.SetSiblingIndex(index);
                item.GetComponentInChildren<TextMeshProUGUI>().text = dataItem.DataItemName;
                page.children.Add(item);

                // Set the button text (e.g., name of the page)
                item.GetComponentInChildren<TextMeshProUGUI>().text = dataItem.DataItemName;
                index++;
            }
        }
        else
        {
            foreach (GameObject child in page.children)
            {
                Destroy(child.gameObject);
            }
        }
        
    }

    private void OnItemButtonClick(DataItem item, int buttonChildIndex)
    {

    }

}
