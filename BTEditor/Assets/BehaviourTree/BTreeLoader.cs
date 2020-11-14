using UnityEngine;
using System.Collections;

namespace GameAI
{
    public class BTreeLoader
    {
        GameAI.BTree btTree;
        BTInput m_input;
        public BTreeLoader(string jsonStr,BTInput input)
        {
            btTree = new GameAI.BTree();
            btTree.InitTreeByJsonString(jsonStr);
            m_input = input;
        }
        public ActionResult Tick()
        {
            ActionResult result = btTree.Tick(m_input);
            return result;
        }

    }

}
