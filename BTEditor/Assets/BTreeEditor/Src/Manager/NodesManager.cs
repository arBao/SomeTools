using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameAI;
using System;

public class NodesManager
{
    //public Dictionary<Type, List<Type>> dicTypes = new Dictionary<Type, List<Type>>();
    public Dictionary<Type, string> dicTypeToUI = new Dictionary<Type, string>();
    public List<TypeEntry> listTypeEntry = new List<TypeEntry>();

    private static NodesManager m_Instance = null;
    public static NodesManager Instance
    {
        get{
            if(m_Instance == null)
            {
                m_Instance = new NodesManager();
            }
            return m_Instance;
        }
    }

    NodesManager()
    {
        TypeEntry typeEntry = new TypeEntry(typeof(BNodeAction),(new BNodeAction()).NodeName);
        listTypeEntry.Add(typeEntry);

        typeEntry = new TypeEntry(typeof(BNodeComposite),(new BNodeComposite()).NodeName);
        listTypeEntry.Add(typeEntry);

        typeEntry = new TypeEntry(typeof(BNodeCondition),(new BNodeCondition()).NodeName);
        listTypeEntry.Add(typeEntry);

        typeEntry = new TypeEntry(typeof(BNodeDecorator),(new BNodeDecorator()).NodeName);
        listTypeEntry.Add(typeEntry);

        typeEntry = new TypeEntry(typeof(BNodeRoot), (new BNodeRoot()).NodeName);
        listTypeEntry.Add(typeEntry);
    }

    public void RegisterNode(Type type)
    {
        if(type.BaseType.Equals(typeof(BNodeAction)))
        {
            TypeEntry typeEntry = listTypeEntry.Find(e => e.type == type.BaseType);
            typeEntry.childrenType.Add(new TypeEntry(type, (Activator.CreateInstance(type) as BNode).NodeName));

            Debug.LogError("RegisterNode BNodeAction  " + type.Name);
            dicTypeToUI.Add(type, "NodeActionUI");
        }
        else if(type.BaseType.Equals(typeof(BNodeCondition)))
        {
            TypeEntry typeEntry = listTypeEntry.Find(e => e.type == type.BaseType);
            typeEntry.childrenType.Add(new TypeEntry(type, (Activator.CreateInstance(type) as BNode).NodeName));

            Debug.LogError("RegisterNode BNodeCondition  " + type.Name);
            dicTypeToUI.Add(type, "NodeConditionUI");
        }
        else if (type.BaseType.Equals(typeof(BNodeComposite)))
        {
            TypeEntry typeEntry = listTypeEntry.Find(e => e.type == type.BaseType);
            typeEntry.childrenType.Add(new TypeEntry(type, (Activator.CreateInstance(type) as BNode).NodeName));
            Debug.LogError("(Activator.CreateInstance(type) as BNode).NodeName  " + (Activator.CreateInstance(type) as BNode).NodeName + "  type  " + type);

            Debug.LogError("RegisterNode BNodeComposite  " + type.Name);
            if (type.Equals(typeof(BNodeIterator)))
            {
                dicTypeToUI.Add(type, "NodeIteratorUI");
            }
            else if(type.Equals(typeof(BNodeParallel)))
            {
                dicTypeToUI.Add(type, "NodeParallelUI");
            }
            else if (type.Equals(typeof(BNodeRandom)))
            {
                dicTypeToUI.Add(type, "NodeRandomUI");
            }
            else if (type.Equals(typeof(BNodeSelector)))
            {
                dicTypeToUI.Add(type, "NodeSelectorUI");
            }
            else if (type.Equals(typeof(BNodeSequence)))
            {
                dicTypeToUI.Add(type, "NodeSequenceUI");
            }
            else if (type.Equals(typeof(BNodeInverse)))
            {
                dicTypeToUI.Add(type, "NodeInverseUI");
            }
        }
        else if (type.BaseType.Equals(typeof(BNodeDecorator)))
        {
            TypeEntry typeEntry = listTypeEntry.Find(e => e.type == type.BaseType);
            typeEntry.childrenType.Add(new TypeEntry(type, (Activator.CreateInstance(type) as BNode).NodeName));

            Debug.LogError("RegisterNode BNodeDecorator  " + type.Name);
            dicTypeToUI.Add(type, "NodeDecoratorUI");
        }
        else if(type.Equals(typeof(BNodeRoot)))
        {
            Debug.LogError("ui BNodeRoot  " + type.Name);
            dicTypeToUI.Add(type, "NodeRootUI");
        }
    }
    public string GetUIByType(Type type)
    {
        return dicTypeToUI[type];
    }

}

public class TypeEntry
{
    public Type type;
    public string typeName;
    public List<TypeEntry> childrenType = new List<TypeEntry>();

    public TypeEntry(Type typeParm,string typeNameParm)
    {
        type = typeParm;
        typeName = typeNameParm;
    }
}