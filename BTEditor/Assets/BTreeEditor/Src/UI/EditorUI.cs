using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEditor;
using GameAI;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using LitJson;

public class EditorUI : MonoBehaviour
{
    public const string EditorUIPrefabPath = "Assets/BTreeEditor/Resources/Prefab/";
    #region 属性

    public ButtonCustom BtnLoad;
    public ButtonCustom BtnSave;
    public ButtonCustom BtnNew;

    public Transform Content;
    public Transform LinesContainer;
    public Transform NodeContainer;
    public Text TextDes;
    public PopUI popUI;
    public Transform ParamsContent;

    private float contentScale = 1;

    private int row = 0;

    private float startX = -3463f;
    private float startY = 256f;

    private List<GameObject> listNodes = new List<GameObject>();
    private List<GameObject> listLines = new List<GameObject>();
    private List<GameObject> parmsObjs = new List<GameObject>();

    public BTree currentTree;
    GameObject lastClickNodeButton;
    int clickTimes;
    BNode currentClickNode;

    int m_nodeId = 0;

    #endregion

    #region Start & Update

    void Awake()
    {
        RegisterTypes();
    }
    void Start()
    {
        BtnLoad.onClickCustom = OnClickBtnLoad;
        BtnSave.onClickCustom = OnClickBtnSave;
        BtnNew.onClickCustom = OnClickBtnNew;

        if (popUI == null)
        {
            popUI = (GameObject.Instantiate(AssetDatabase.LoadAssetAtPath(EditorUIPrefabPath + "PopUI.prefab", typeof(GameObject))) as GameObject).GetComponent<PopUI>();
            popUI.transform.SetParent(NodeContainer);
            popUI.transform.GetComponent<RectTransform>().localScale = Vector3.one;
            popUI.editorUI = this;
            popUI.gameObject.SetActive(false);
        }

    }

    void Update()
    {

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            contentScale += Input.GetAxis("Mouse ScrollWheel");

            if (contentScale < 0.4)
            {
                contentScale = 0.4f;
            }
            else if (contentScale > 1)
            {
                contentScale = 1f;
            }

            Content.localScale = new Vector3(contentScale, contentScale, 1);
        }

    }
    #endregion


    #region OnClicks

    void OnClickBtnLoad(GameObject sender)
    {
        string outPath = Application.dataPath + "/Config/BTJson";
        if (!Directory.Exists(outPath))
        {
            Directory.CreateDirectory(outPath);
        }

        string filepath = EditorUtility.OpenFilePanel("Bahvior Tree", outPath, "json");
        if (filepath == "") return;

        string txt = File.ReadAllText(filepath);

        if (currentTree == null)
        {
            currentTree = new BTree();
        }
        JsonData json = JsonMapper.ToObject(txt);
        currentTree.InitTreeByJsonData(json);

        DrawTree(currentTree);
    }

    void OnClickBtnSave(GameObject sender)
    {
        if (currentTree != null)
        {
            string outPath = Application.dataPath + "/Config/BTJson";
            if (!Directory.Exists(outPath))
            {
                Directory.CreateDirectory(outPath);
            }
            string filepath = EditorUtility.SaveFilePanel("Behavior Tree", outPath, currentTree.GetTreeName(), "json");
            Debug.Log(filepath);

            File.WriteAllText(filepath, currentTree.ToJsonData().ToJson());
        }

    }

    void OnClickBtnNew(GameObject sender)
    {
        DrawTree(new BTree());
    }

    void OnClickBtnNode(GameObject sender)
    {
        if (lastClickNodeButton != null && lastClickNodeButton != sender)
        {
            lastClickNodeButton.transform.parent.Find("SlectBG").gameObject.SetActive(false);
            clickTimes = 0;
        }
        currentClickNode = sender.transform.parent.GetComponent<NodeUIBase>().node;
        TextDes.text = currentClickNode.Description;

        sender.transform.parent.Find("SlectBG").gameObject.SetActive(true);

        if (lastClickNodeButton == sender)
        {
            clickTimes++;
            if (clickTimes >= 1)
            {
                Type nodeType = currentClickNode.GetType();
                if (!nodeType.BaseType.Equals(typeof(BNodeComposite)) && !nodeType.Equals(typeof(BNodeRoot))
                     )//如果不是复合节点，或者是根节点是根节点的子节点大于0，就应该隐藏add按钮
                {
                    popUI.HideAddButton(true);
                }
                else if (nodeType.Equals(typeof(BNodeRoot)))
                {
                    popUI.HideCopyAndPasteButton(true);
                    if (currentTree.rootNode.ListChildren.Count > 0)
                    {
                        popUI.HideAddButton(true);
                    }
                }
                else if (nodeType.Equals(typeof(BNodeInverse)) && currentClickNode.ListChildren.Count >= 1)
                {
                    popUI.HideAddButton(true);
                }
                if (nodeType.Equals(typeof(BNodeRoot)) && currentClickNode.ParentNode == null)
                {
                    popUI.HideDeleteButton(true);
                    popUI.HideInsertButton(true);
                }

                Vector3 pos = sender.transform.parent.GetComponent<RectTransform>().localPosition;
                pos.x += 120;
                pos.z = 0;
                popUI.gameObject.SetActive(true);

                popUI.currentNode = sender.transform.parent.GetComponent<NodeUIBase>().node;
                pos.y += 35f;

                popUI.gameObject.GetComponent<RectTransform>().localPosition = pos;
                popUI.transform.SetAsLastSibling();
            }
        }

        lastClickNodeButton = sender;

        ShowCurrentNodeParams(currentClickNode);

        Debug.LogError(sender.transform.parent.GetComponent<NodeUIBase>().node.NodeName);
    }
    #endregion

    #region Methods

    void ShowCurrentNodeParams(object node)
    {
        Type type = node.GetType();

        //Debug.LogError("-------------------" + type.Name);
        PropertyInfo[] ps = type.GetProperties();
        PropertyInfo[] basePs = type.BaseType.GetProperties();
        List<PropertyInfo> listInfo = new List<PropertyInfo>();
        foreach (PropertyInfo info in ps)
        {
            object obj = info.GetValue(node, null);

            bool flag = false;
            foreach (PropertyInfo baseinfo in basePs)
            {
                if (baseinfo.Name.Equals(info.Name))
                    flag = true;
            }
            if (flag)
                continue;
            listInfo.Add(info);
        }

        if (listInfo.Count > 7)
        {
            Vector2 size = ParamsContent.GetComponent<RectTransform>().sizeDelta;
            size.y = 350 + 40 * (listInfo.Count - 7);
            ParamsContent.GetComponent<RectTransform>().sizeDelta = size;
        }

        for (int i = 0; i < parmsObjs.Count; i++)
        {
            GameObject.Destroy(parmsObjs[i]);
        }
        parmsObjs.Clear();

        for (int i = 0; i < listInfo.Count; i++)
        {
            PropertyInfo info = listInfo[i];
            object[] attributesInfo = info.GetCustomAttributes(true);

            bool shouldShow = false;
            for (int l = 0; l < attributesInfo.Length; l++)
            {
                if (attributesInfo[l] is ShowInEditorUI)
                {
                    shouldShow = true;
                    break;
                }
            }
            if (!shouldShow)
                continue;

            if (info.PropertyType.Equals(typeof(System.Boolean)))
            {
                GameObject obj = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath(EditorUIPrefabPath + "ParamsItemBool.prefab", typeof(GameObject))) as GameObject;
                parmsObjs.Add(obj);
                obj.transform.SetParent(ParamsContent);
                obj.GetComponent<RectTransform>().localScale = Vector3.one;
                obj.GetComponent<RectTransform>().localPosition = new Vector3(0, -37 - 40 * i, 0);

                ParamsItemBool pin = obj.GetComponent<ParamsItemBool>();
                pin.TextTitle.text = info.Name;
                pin.pInfo = info;
                pin.infoObj = node;
                pin.SetIsSelect((System.Boolean)info.GetValue(node, null));
            }
            else if (info.PropertyType.BaseType.Equals(typeof(System.Enum)))
            {
                GameObject obj = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath(EditorUIPrefabPath + "ParamsItemEnum.prefab", typeof(GameObject))) as GameObject;
                parmsObjs.Add(obj);
                obj.transform.SetParent(ParamsContent);
                obj.GetComponent<RectTransform>().localScale = Vector3.one;
                obj.GetComponent<RectTransform>().localPosition = new Vector3(0, -37 - 40 * i, 0);

                ParamsItemEnum pin = obj.GetComponent<ParamsItemEnum>();
                pin.TextTitle.text = info.Name;
                pin.pInfo = info;
                pin.infoObj = node;
                pin.SetIsSelect((System.Enum)info.GetValue(node, null));
                pin.ResetEnumsWithType(info.PropertyType);
            }
            else
            {
                GameObject obj = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath(EditorUIPrefabPath + "ParamsItemNormal.prefab", typeof(GameObject))) as GameObject;
                parmsObjs.Add(obj);
                obj.transform.SetParent(ParamsContent);
                obj.GetComponent<RectTransform>().localScale = Vector3.one;
                obj.GetComponent<RectTransform>().localPosition = new Vector3(0, -37 - 40 * i, 0);

                ParamsItemNormal pin = obj.GetComponent<ParamsItemNormal>();
                pin.TextTitle.text = info.Name;

                pin.pInfo = info;
                pin.infoObj = node;
                if (info.PropertyType.Equals(typeof(System.String)))
                {
                    pin.InputField.transform.Find("Placeholder").GetComponent<Text>().text = "文字";
                    pin.SetInputfieldType(InputField.ContentType.Standard);
                }
                else if (info.PropertyType.Equals(typeof(System.Int32)) || info.PropertyType.Equals(typeof(System.Int64)) || info.PropertyType.Equals(typeof(System.Int16)))
                {
                    pin.InputField.transform.Find("Placeholder").GetComponent<Text>().text = "数字";
                    pin.SetInputfieldType(InputField.ContentType.IntegerNumber);
                }
                else if (info.PropertyType.Equals(typeof(System.Single)) || info.PropertyType.Equals(typeof(System.Double)))
                {
                    pin.InputField.transform.Find("Placeholder").GetComponent<Text>().text = "小数";
                    pin.SetInputfieldType(InputField.ContentType.DecimalNumber);
                }
                pin.InputField.text = info.GetValue(node, null).ToString();
            }
        }
    }


    public void DrawTree(BTree tree)
    {
        //先清空上次画的
        for (int i = 0; i < listNodes.Count; i++)
        {
            GameObject.Destroy(listNodes[i]);
        }
        listNodes.Clear();

        for (int i = 0; i < listLines.Count; i++)
        {
            GameObject.Destroy(listLines[i]);
        }
        listLines.Clear();

        //画此次的
        row = 0;
        m_nodeId = 0;
        currentTree = tree;
        DrawChild(currentTree.rootNode, 0, 0, m_nodeId);
    }

    void DrawChild(BNode node, int level, int rowLastLevel, int nodeId)// 根据第几层第几行画出节点
    {
        //Debug.LogError(node.NodeName + "  level  " + level + "  row  " + row);

        #region drawnode
        string uiname = NodesManager.Instance.GetUIByType(node.GetType());
        //Debug.LogError("uiname  " + uiname);
        GameObject ui = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath(EditorUIPrefabPath +uiname + ".prefab", typeof(GameObject))) as GameObject;
        ui.GetComponent<NodeUIBase>().Button.onClickCustom = OnClickBtnNode;
        ui.GetComponent<NodeUIBase>().Button.transform.Find("Text").GetComponent<Text>().text = string.Format("({0}){1}", nodeId, node.NodeName);//node.NodeName;
        ui.GetComponent<NodeUIBase>().node = node;
        //row * 50  level * 60
        ui.transform.SetParent(NodeContainer);
        Vector3 pos = new Vector3(startX + level * 60, startY - row * 50, 0);
        ui.GetComponent<RectTransform>().localPosition = pos;
        ui.GetComponent<RectTransform>().localScale = Vector3.one;

        listNodes.Add(ui.gameObject);
        #endregion

        #region drawline

        if (level != 0)
        {
            //横线
            GameObject lineH = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath(EditorUIPrefabPath + "ImageLine" + ".prefab", typeof(GameObject))) as GameObject;
            lineH.transform.SetParent(LinesContainer);
            lineH.GetComponent<RectTransform>().localPosition = new Vector3(pos.x - 60, pos.y, 0);
            lineH.GetComponent<RectTransform>().localScale = Vector3.one;
            lineH.GetComponent<RectTransform>().pivot = new Vector2(1f, 0.5f);
            lineH.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, 10f);
            listLines.Add(lineH.gameObject);
            //竖线
            GameObject lineV = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath(EditorUIPrefabPath + "ImageLine" + ".prefab", typeof(GameObject))) as GameObject;
            lineV.transform.SetParent(LinesContainer);
            lineV.GetComponent<RectTransform>().localPosition = new Vector3(pos.x - 60 - 50, pos.y - 5, 0);
            lineV.GetComponent<RectTransform>().localScale = Vector3.one;
            lineV.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0f);
            lineV.GetComponent<RectTransform>().sizeDelta = new Vector2(10f, 50 * (row - rowLastLevel));
            listLines.Add(lineV.gameObject);
        }

        #endregion

        row++;

        if (node is BNodeRoot && level != 0)
        {
            return;
        }

        if (node.ListChildren.Count > 0)
        {
            int rowLast = row - 1;
            level++;
            for (int i = 0; i < node.ListChildren.Count; i++)
            {
                BNode child = node.ListChildren[i];
                m_nodeId += 1;

                DrawChild(child, level, rowLast, m_nodeId);
            }
        }
    }


    void RegisterTypes()
    {
        Assembly ass = Assembly.GetExecutingAssembly();
        var types = ass.GetTypes();

        foreach (var item in types)
        {
            if (item.Namespace == "GameAI")
            {
                var type = item.BaseType;
                if (type == typeof(GameAI.BNodeAction) || type == typeof(GameAI.BNodeComposite)
                    || type == typeof(GameAI.BNodeCondition) || type == typeof(GameAI.BNodeDecorator) || item == typeof(GameAI.BNodeRoot))
                {
                    NodesManager.Instance.RegisterNode(item);
                }
            }
        }
    }

    #endregion


}