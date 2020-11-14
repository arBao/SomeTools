using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System;
using System.Reflection;

namespace GameAI
{
    public class BTree
    {

        public BNodeRoot rootNode;
        public BTree()
        {
            rootNode = new BNodeRoot();
            #region test

            //BNodeSequence nodeSequence1 = new BNodeSequence();
            //nodeSequence1.AddChild(new ConditionNothing());
            //nodeSequence1.AddChild(new ActionNodeNothing());

            //BNodeSequence nodeSequence2 = new BNodeSequence();
            //nodeSequence2.AddChild(new ConditionNothing());
            //nodeSequence2.AddChild(new DecoratorNothing());

            //BNodeSelector nodeSelector = new BNodeSelector();

            //nodeSelector.AddChild(nodeSequence1);
            //nodeSelector.AddChild(nodeSequence2);
            //rootNode.AddChild(nodeSelector);
            //return;
            #endregion
            
        }
        public ActionResult Tick(BTInput input)
        {
            return rootNode.RunNode(input);
        }

        public JsonData ToJsonData()
        {
            JsonData data = new JsonData();
            data["tree"] = new JsonData();
            data["tree"].SetJsonType(JsonType.Object);
            JsonData rootChild = data["tree"];
            rootChild["name"] = rootNode.NodeName;
            JsonData children = new JsonData();
            children.SetJsonType(JsonType.Array);
            rootChild["children"] = children;

            for (int i = 0; i < rootNode.ListChildren.Count; i++)
            {
                BNode child = rootNode.ListChildren[i];
                WriteChildrenJsonData(child, rootChild["children"]);
            }
            return data;
        }

        void WriteChildrenJsonData(BNode childNode, JsonData parentData)
        {
            JsonData nodeData = new JsonData();
            parentData.Add(nodeData);
            nodeData.SetJsonType(JsonType.Object);
            nodeData["name"] = childNode.NodeName;

            nodeData["type"] = childNode.GetType().ToString();

            JsonData args = new JsonData();
            args.SetJsonType(JsonType.Array);
            nodeData["args"] = args;
            #region args
            Type type = childNode.GetType();
            PropertyInfo[] ps = type.GetProperties();
            PropertyInfo[] basePs = type.BaseType.GetProperties();
            List<PropertyInfo> listInfo = new List<PropertyInfo>();
            foreach (PropertyInfo info in ps)
            {
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
            for (int i = 0; i < listInfo.Count; i++)
            {
                PropertyInfo info = listInfo[i];
                JsonData arg = new JsonData();
                arg["argname"] = info.Name;
                arg["argtype"] = info.PropertyType.ToString();
                arg["argvalue"] = info.GetValue(childNode, null).ToString();
                args.Add(arg);
            }
            #endregion

            JsonData childrenData = new JsonData();
            childrenData.SetJsonType(JsonType.Array);
            nodeData["children"] = childrenData;
            for (int i = 0; i < childNode.ListChildren.Count; i++)
            {
                BNode child = childNode.ListChildren[i];
                WriteChildrenJsonData(child, childrenData);
            }

        }

        public void SetTreeName(string name)
        {
            rootNode.NodeName = name;
        }
        public string GetTreeName()
        {
            return rootNode.NodeName;
        }
        public bool InitTreeByJsonString(string jsonStr)
        {
            JsonData json = JsonMapper.ToObject(jsonStr);
            return InitTreeByJsonData(json);
        }

        public bool InitTreeByJsonData(JsonData jsonData)
        {
            rootNode = null;
            rootNode = new BNodeRoot();

            JsonData tree = jsonData["tree"];
            rootNode.NodeName = (string)tree["name"];
            //Debug.LogError("rootNode.NodeName " + rootNode.NodeName);
            for (int i = 0; i < tree["children"].Count; i++)
            {
                JsonData nodeJson = tree["children"][i];
                AddChildNode(nodeJson, rootNode);
            }
            return true;
        }

        void AddChildNode(JsonData jsonData, BNode fatherNode)
        {
            //Debug.LogError("jsonData  " + jsonData["name"]);
            Type t = Type.GetType((string)jsonData["type"]);
            BNode nodeChild = Activator.CreateInstance(t) as BNode;
            #region args
            JsonData args = jsonData["args"];
            PropertyInfo[] pInfos = nodeChild.GetType().GetProperties();
            for (int i = 0; i < args.Count; i++)
            {
                JsonData arg = args[i];

                PropertyInfo pi = nodeChild.GetType().GetProperty(arg["argname"].ToString());
                if (Type.GetType(arg["argtype"].ToString()).Equals(typeof(System.String)))
                {
                    pi.SetValue(nodeChild, arg["argvalue"].ToString(),null);
                }
                else if(Type.GetType(arg["argtype"].ToString()).Equals(typeof(System.Single)))
                {
                    pi.SetValue(nodeChild, float.Parse(arg["argvalue"].ToString()), null);
                }
                else if(Type.GetType(arg["argtype"].ToString()).Equals(typeof(System.Int32)))
                {
                    pi.SetValue(nodeChild, int.Parse(arg["argvalue"].ToString()), null);
                }
                else if (Type.GetType(arg["argtype"].ToString()).BaseType.Equals(typeof(System.Enum)))
                {
                    //pi.SetValue(nodeChild, int.Parse(arg["argvalue"].ToString()), null);
                    //arg["argvalue"].ToString()
                    //Debug.LogError("--------------------------------------ffff");

                    FieldInfo fiSelect = null;
                    FieldInfo[] fields = pi.PropertyType.GetFields(BindingFlags.Static | BindingFlags.Public);
                    for (int j = 0; j < fields.Length; j++)
                    {
                        FieldInfo fi = fields[j];
                        if (fi.Name.Equals(arg["argvalue"].ToString()))
                        {
                            fiSelect = fi;
                        }
                    }
                    pi.SetValue(nodeChild, fiSelect.GetValue(null), null);
                }
                else if (Type.GetType(arg["argtype"].ToString()).Equals(typeof(System.Boolean)))
                {
                    if (arg["argvalue"].ToString().Equals("True"))
                    {
                        pi.SetValue(nodeChild, true, null);
                    }
                    else
                    {
                        pi.SetValue(nodeChild, false, null);
                    }
                }
            }

            #endregion

            fatherNode.AddChild(nodeChild);

            for (int i = 0; i < jsonData["children"].Count; i++)
            {
                JsonData nodeJson = jsonData["children"][i];
                AddChildNode(nodeJson, nodeChild);
            }
        }
    }
}
