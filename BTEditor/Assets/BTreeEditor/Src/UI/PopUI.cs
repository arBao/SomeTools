using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using GameAI;
using System;
using System.Reflection;
using System.IO;
using UnityEditor;
using LitJson;

public class PopUI : MonoBehaviour
{

    #region 属性
    public Image imageMask;
    public Transform Pop1;
    public Transform Pop2;
    public ButtonCustom BtnInsert;
    public ButtonCustom BtnAdd;
    public ButtonCustom BtnDelete;
    public ButtonCustom BtnCopy;
    public ButtonCustom BtnPaste;
    public Transform Pop1ItemsContainer;
    public Transform Pop2ItemsContainer;

    bool isInsert = false;
    

    public BNode currentNode;
    public BNode copyNode;

    public EditorUI editorUI;
    GameObject lastClickPop1Item;

    List<GameObject> listPop2Items = new List<GameObject>();

    Color selectColor = new Color(29f / 255, 140f / 255, 206f / 255);
    Color normalColor = new Color(67f / 255, 39f / 255, 214f / 255);

    #endregion

    #region Start & Update
    void Start()
    {
        EventTriggerListener.Get(imageMask.gameObject).onClick = OnClickImageMask;

        BtnAdd.onClickCustom = OnClickBtnAdd;
        BtnDelete.onClickCustom = OnClickBtnDelete;
        BtnCopy.onClickCustom = OnClickBtnCopy;
        BtnPaste.onClickCustom = OnClickBtnPaste;
        BtnInsert.onClickCustom = OnClickBtnInsert;
        BtnPaste.gameObject.SetActive(false);

        InitPop1Items();
    }

    void Update()
    {

    }
    #endregion


    #region OnClicks

    void OnClickBtnInsert(GameObject sender)
    {
        isInsert = true;
        Pop1.gameObject.SetActive(true);
        sender.GetComponent<Image>().color = selectColor;
    }

    void OnClickBtnCopy(GameObject sender)
    {
        copyNode = currentNode;
        BtnPaste.gameObject.SetActive(true);
        Reset();
    }

    void OnClickBtnPaste(GameObject sender)
    {
        currentNode.AddChild(copyNode);
        BtnPaste.gameObject.SetActive(false);
        copyNode = null;
        editorUI.DrawTree(editorUI.currentTree);
        Reset();
    }

    void OnClickImageMask(GameObject sender)
    {
        Reset();   
    }

    void OnClickBtnDelete(GameObject sender)
    {
        currentNode.DeleteSelf();
        editorUI.DrawTree(editorUI.currentTree);

        Reset();
    }

    void OnClickBtnAdd(GameObject sender)
    {
        isInsert = false;
        Pop1.gameObject.SetActive(true);
        sender.GetComponent<Image>().color = selectColor;
    }

    void OnClickPop1Item(GameObject sender)
    {
        if (lastClickPop1Item == sender)
            return;
        if (lastClickPop1Item != null)
        {
            lastClickPop1Item.GetComponent<Image>().color = normalColor;
        }
        TypeEntry typeEntry = sender.GetComponent<BtnNodeAddItem>().typeEntry;
        
        if (typeEntry.type == typeof(BNodeRoot))
        {
            Debug.LogError("BNodeRoot");

            string outPath = Application.dataPath + "/Config/BTJson";
            if (!Directory.Exists(outPath))
            {
                Directory.CreateDirectory(outPath);
            }

            string filepath = EditorUtility.OpenFilePanel("Bahvior Tree", outPath, "json");
            if (filepath == "") return;

            string txt = File.ReadAllText(filepath);

            BTree tree = new BTree();
            JsonData json = JsonMapper.ToObject(txt);
            tree.InitTreeByJsonData(json);

            if(isInsert)
            {
                int index = currentNode.ParentNode.ListChildren.IndexOf(currentNode);
                currentNode.ParentNode.InsertChild(tree.rootNode, index);
            }
            else
            {
                currentNode.AddChild(tree.rootNode);
            }

            editorUI.DrawTree(editorUI.currentTree);

            Reset();

        }
        else
        {
            for (int i = 0; i < listPop2Items.Count; i++)
            {
                GameObject.Destroy(listPop2Items[i].gameObject);
            }
            listPop2Items.Clear();
            
            sender.GetComponent<Image>().color = selectColor;

            Pop2.gameObject.SetActive(true);
            Vector3 pos = Pop2.GetComponent<RectTransform>().localPosition;
            pos.y = sender.GetComponent<RectTransform>().localPosition.y;
            Pop2.GetComponent<RectTransform>().localPosition = pos;

            for (int i = 0; i < typeEntry.childrenType.Count; i++)
            {
                TypeEntry te = typeEntry.childrenType[i];

                GameObject btnNodeAddItem = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath(EditorUI.EditorUIPrefabPath + "BtnNodeAddItemPop2.prefab", typeof(GameObject))) as GameObject;
                listPop2Items.Add(btnNodeAddItem);
                btnNodeAddItem.transform.SetParent(Pop2ItemsContainer);
                btnNodeAddItem.GetComponent<RectTransform>().localScale = Vector3.one;
                btnNodeAddItem.GetComponent<ButtonCustom>().onClickCustom = OnClickPop2Item;
                pos = new Vector3();
                pos.x = 35;
                pos.y = 170 - 40 * i;
                pos.z = 0;
                btnNodeAddItem.GetComponent<RectTransform>().localPosition = pos;
                btnNodeAddItem.GetComponent<BtnNodeAddItem>().typeEntry = te;
                btnNodeAddItem.transform.Find("Text").GetComponent<Text>().text = te.typeName;
            }
            Vector2 size = Pop2.Find("Pop2BG").GetComponent<RectTransform>().sizeDelta;
            if (typeEntry.childrenType.Count > 4)
            {
                size.y = 200 + (typeEntry.childrenType.Count - 4) * 40;
            }
            else
            {
                size.y = 200;
            }

            Pop2.Find("Pop2BG").GetComponent<RectTransform>().sizeDelta = size;
            lastClickPop1Item = sender;
        }
        
        
    }

    void OnClickPop2Item(GameObject sender)
    {
        BNode child = (BNode)Activator.CreateInstance(sender.GetComponent<BtnNodeAddItem>().typeEntry.type);

        if (isInsert)
        {
            int index = currentNode.ParentNode.ListChildren.IndexOf(currentNode);
            currentNode.ParentNode.InsertChild(child, index);
        }
        else
        {
            currentNode.AddChild(child);
        }

        editorUI.DrawTree(editorUI.currentTree);

        Reset();
    }
    #endregion

    #region Methods
    void Reset()
    {
        isInsert = false;
        gameObject.SetActive(false);
        Pop1.gameObject.SetActive(false);
        Pop2.gameObject.SetActive(false);
        if (lastClickPop1Item != null)
        {
            lastClickPop1Item.GetComponent<Image>().color = normalColor;
        }
        BtnAdd.GetComponent<Image>().color = normalColor;
        BtnInsert.GetComponent<Image>().color = normalColor;
        HideAddButton(false);
        HideCopyAndPasteButton(false);
        HideDeleteButton(false);
        HideInsertButton(false);
        lastClickPop1Item = null;
    }
    public void HideInsertButton(bool hide)
    {
        if (hide)
        {
            BtnInsert.gameObject.SetActive(false);
        }
        else
        {
            BtnInsert.gameObject.SetActive(true);
        }
    }
    public void HideDeleteButton(bool hide)
    {
        if (hide)
        {
            BtnDelete.gameObject.SetActive(false);
        }
        else
        {
            BtnDelete.gameObject.SetActive(true);
        }
    }

    public void HideAddButton(bool hide)
    {
        if(hide)
        {
            BtnAdd.gameObject.SetActive(false);
        }
        else
        {
            BtnAdd.gameObject.SetActive(true);
        }
    }

    public void HideCopyAndPasteButton(bool hide)
    {
        if(hide)
        {
            BtnCopy.gameObject.SetActive(false);
            BtnPaste.gameObject.SetActive(false);
        }
        else
        {
            BtnCopy.gameObject.SetActive(true);
            if(copyNode == null)
            {
                BtnPaste.gameObject.SetActive(false);
            }
            else
            {
                BtnPaste.gameObject.SetActive(true);
            }
        }
    }

    void InitPop1Items()
    {
        for (int i = 0; i < NodesManager.Instance.listTypeEntry.Count; i++)
        {
            TypeEntry te = NodesManager.Instance.listTypeEntry[i];
            GameObject btnNodeAddItem = GameObject.Instantiate(Resources.Load("Prefab/BtnNodeAddItem")) as GameObject;
            btnNodeAddItem.transform.SetParent(Pop1ItemsContainer);
            btnNodeAddItem.GetComponent<RectTransform>().localScale = Vector3.one;
            btnNodeAddItem.GetComponent<ButtonCustom>().onClickCustom = OnClickPop1Item;
            Vector3 pos = new Vector3();
            pos.x = 0;
            pos.y = 40 - 40 * i;
            pos.z = 0;
            btnNodeAddItem.GetComponent<RectTransform>().localPosition = pos;
            btnNodeAddItem.GetComponent<BtnNodeAddItem>().typeEntry = te;
            btnNodeAddItem.transform.Find("Text").GetComponent<Text>().text = te.typeName;

        }
        
    }

    #endregion


}