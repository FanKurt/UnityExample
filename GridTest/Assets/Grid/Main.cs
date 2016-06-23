using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour 
{
    public int count;
    public bool isUpdate = true;


    private CustomGrid grid;
    private int index = 0;
    private int i = 0;
    private UILabel label;

    void Awake()
    {
        grid = GetComponentInChildren<CustomGrid>();

        label = transform.FindChild("back/back/Label").GetComponent<UILabel>();
    }

	void Start () 
    {
        while (i < count)
        {
            Add("" + i++);
        }

        label.text = string.Format("创建 {0} 个Item", i.ToString());
	}

    

    void Update()
    {
        if (isUpdate)
        {

            if (index % 30 == 0)
            {
                Add(i.ToString());
                i++;

                label.text = string.Format("创建 {0} 个Item", i.ToString());
            }

            index++;
        }
    }

    private void Add(string text)
    {
        grid.AddItem(text);
    }
}
