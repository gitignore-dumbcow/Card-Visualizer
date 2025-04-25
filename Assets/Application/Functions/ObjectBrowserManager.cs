using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectBrowserManager : MonoBehaviour
{
    public GameObject view;
    public Transform itemList, propertyList;
    public GameObject objectListItemPF;

    public TextMeshProUGUI title;
    public DataCollection data;

    private DataItem currentDataItem;
    private bool open;

    private Coroutine populateCoroutine;
    private Coroutine expandCoroutine;

    public void UpdateList()
    {
        ClearList();
        populateCoroutine = StartCoroutine(PopulatePagesCoroutine());
    }

    public void ClearList()
    {
        if (populateCoroutine != null)
        {
            StopCoroutine(populateCoroutine);
            populateCoroutine = null;
        }
        if (expandCoroutine != null)
        {
            StopCoroutine(expandCoroutine);
            expandCoroutine = null;
        }

        foreach (Transform child in itemList)
        {
            Destroy(child.gameObject);
        }
    }

    public void ToggleList()
    {
        open = !open;

        if (open)
        {
            view.SetActive(true);
            UpdateList();
        }
        else
        {
            ClearList();
            view.SetActive(false);
        }
    }

    private IEnumerator PopulatePagesCoroutine()
    {
        foreach (DataPage page in data.DataCollectionList)
        {
            GameObject item = Instantiate(objectListItemPF, itemList);
            item.GetComponentInChildren<TextMeshProUGUI>().text = page.PageName;

            Button pageButton = item.GetComponent<Button>();
            if (pageButton != null)
            {
                pageButton.onClick.AddListener(() => OnPageButtonClick(page, item.transform.GetSiblingIndex()));
            }

            yield return null;

            if (page.extended)
            {
                int count = 0;
                foreach (DataItem dataItem in page.List)
                {
                    GameObject childItem = Instantiate(objectListItemPF, itemList);
                    childItem.GetComponentInChildren<TextMeshProUGUI>().text = dataItem.DataItemName;
                    page.children.Add(childItem);

                    Button itemButton = childItem.GetComponent<Button>();
                    if (itemButton != null)
                    {
                        itemButton.onClick.AddListener(() => OnItemButtonClick(dataItem));
                    }

                    count++;
                    if (count >= 10)
                    {
                        count = 0;
                        yield return null; // Wait for next frame after 10 items
                    }
                }
            }
        }
    }

    private void OnPageButtonClick(DataPage page, int buttonChildIndex)
    {
        if (expandCoroutine != null)
        {
            StopCoroutine(expandCoroutine);
        }
        expandCoroutine = StartCoroutine(HandlePageExpansionCoroutine(page, buttonChildIndex));
    }

    private IEnumerator HandlePageExpansionCoroutine(DataPage page, int buttonChildIndex)
    {
        page.extended = !page.extended;

        if (page.extended)
        {
            int index = buttonChildIndex + 1;
            int count = 0;

            foreach (DataItem dataItem in page.List)
            {
                GameObject item = Instantiate(objectListItemPF, itemList);
                item.transform.SetSiblingIndex(index++);
                item.GetComponentInChildren<TextMeshProUGUI>().text = dataItem.DataItemName;
                page.children.Add(item);

                Button itemButton = item.GetComponent<Button>();
                if (itemButton != null)
                {
                    itemButton.onClick.AddListener(() => OnItemButtonClick(dataItem));
                }

                count++;
                if (count >= 5)
                {
                    count = 0;
                    yield return null; // Batch limit
                }
            }

            itemList.GetComponent<VerticalLayoutGroup>().SetLayoutVertical();
        }
        else
        {
            foreach (GameObject child in page.children)
            {
                Destroy(child);
            }

            page.children.Clear();
        }

        expandCoroutine = null;
    }

    private void OnItemButtonClick(DataItem dataItem)
    {
        if (currentDataItem == dataItem) return;

        currentDataItem = dataItem;

        for (int i = propertyList.childCount - 1; i >= 0; i--)
        {
            Destroy(propertyList.GetChild(i).gameObject);
        }

        title.text = dataItem.DataItemName;

        foreach (DataProperty dataProperty in dataItem.Properties)
        {
            GameObject property = Instantiate(objectListItemPF, propertyList);
            var textComponent = property.GetComponentInChildren<TextMeshProUGUI>();

            if (textComponent != null)
            {
                textComponent.text = $"{dataProperty.Key}: {dataProperty.Value}";
            }
        }

        propertyList.GetComponent<VerticalLayoutGroup>().SetLayoutVertical();
    }
}
