using UnityEngine;

namespace Util
{
    /// <summary>
    /// <para>UI関連のユーティリティー</para>
    /// </summary>
    public class UIUtil
    {
        /// <summary>
        /// <para>指定した名前の子GameObjectを返す</para>
        /// <para>存在しない場合はnullを返す</para>
        /// </summary>
        public static GameObject GetChild(GameObject parent, string name, char split = '/')
        {
            if (parent == null)
                return null;

            string[] names = name.Split(split);
            int targetIndex = 0;

            Transform targetParent = parent.transform;
            Transform targetChild;
            int childCount;

            Main:
            while (true)
            {
                childCount = targetParent.transform.childCount;

                if (childCount > 0)
                {
                    for (int i = 0; i < childCount; ++i)
                    {
                        targetChild = targetParent.transform.GetChild(i);

                        if (targetChild != null && targetChild.gameObject != null && targetChild.gameObject.name == names[targetIndex])
                        {
                            ++targetIndex;

                            if (targetIndex < names.Length)
                            {
                                // さらに下の階層を探す
                                targetParent = targetChild;
                                targetChild = null;

                                goto Main;
                            }
                            else
                            {
                                // 子GameObjectを返す
                                return targetChild.gameObject;
                            }
                        }
                    }
                }

                break;
            }

            return null;
        }
    }
}