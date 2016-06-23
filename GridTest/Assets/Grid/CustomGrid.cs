using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// @author : YangDan
/// @date : 2014-6-20
/// 
/// CustomGrid,这个类主要做了一件事,就是优化了,NGUI UIGrid 在数据量很多都时候,创建过多都GameObject对象,造成资源浪费.
/// 该类需要和 CustomScrollView.cs 以及 CustomDragScrollView.cs一起使用;
/// CustomScrollView 类只上把bounds字段给暴露出来，因为bounds都大小需要在外面设置了.
/// CustomDragScrollView 类 没有修改,因为默认都UIDragScrollView 默认里面调用都上UIScrollView 不能与我们都CustomScrollView兼容.
/// 所以只是将里面都UIScrollView 改为 CustomScrollView.
/// Item 是一个渲染操作类.可以自己定义,或者不要,并没有影响.
/// </summary>
/// 
public class CustomGrid : UIWidgetContainer
{
    public GameObject Item;

    public int m_cellHeight = 60;

    public int m_cellWidth = 700;

    private float m_height;

    private int m_maxLine;

    private Item[] m_cellList;

    private CustomScrollView mDrag;

    private float lastY = -1;

    private List<string> m_listData;

    private Vector3 defaultVec;

    void Awake()
    {
        m_listData = new List<string>();

        defaultVec = new Vector3(0, m_cellHeight, 0);

        mDrag = NGUITools.FindInParents<CustomScrollView>(gameObject);

        m_height = mDrag.panel.height;
		Debug.Log (m_height);

        m_maxLine = Mathf.CeilToInt(m_height / m_cellHeight) + 1;

        m_cellList = new Item[m_maxLine];

        CreateItem();
    }

    void Update()
    {
        if (mDrag.transform.localPosition.y != lastY)
        {
            Validate();

            lastY = mDrag.transform.localPosition.y;
        }
    }

    private void UpdateBounds(int count)
    {
        Vector3 vMin = new Vector3();
        vMin.x = -transform.localPosition.x;
        vMin.y = transform.localPosition.y - count * m_cellHeight;
        vMin.z = transform.localPosition.z;
        Bounds b = new Bounds(vMin, Vector3.one);
        b.Encapsulate(transform.localPosition);

        mDrag.bounds = b;
        mDrag.UpdateScrollbars(true);
        mDrag.RestrictWithinBounds(true);
    }

    public void AddItem(string name)
    {
        m_listData.Add(name);

        Validate();

        UpdateBounds(m_listData.Count);
    }

    private void Validate()
    {
        Vector3 position = mDrag.panel.transform.localPosition;

        float _ver = Mathf.Max(position.y, 0);

        int startIndex = Mathf.FloorToInt(_ver / m_cellHeight);
        int endIndex = Mathf.Min(m_listData.Count, startIndex + m_maxLine);

        Item cell;
        int index = 0;
        for (int i = startIndex; i < startIndex + m_maxLine; i++)
        {
            cell = m_cellList[index];

            if (i < endIndex)
            {
				Debug.Log(m_listData[i]);
                cell.text = m_listData[i];
                cell.transform.localPosition = new Vector3(0, i * -m_cellHeight, 0);
                cell.gameObject.SetActive(true);
            }
            else
            {
                cell.transform.localPosition = defaultVec;
            }

            index++;
        }
    }

    private void CreateItem()
    {
        for (int i = 0; i < m_maxLine; i++)
        {
            GameObject go;
            go = Instantiate(Item) as GameObject;
            go.transform.parent = transform;
            go.transform.localScale = Vector3.one;
            go.SetActive(false);
            go.name = "Item" + i;
            Item item = go.GetComponent<Item>();
            item.Init();
            m_cellList[i] = item;
        }
    }
}