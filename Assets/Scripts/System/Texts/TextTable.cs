using System.Collections.Generic;
using UnityEngine;

namespace System
{
    /// <summary>
    /// <para>UIに表示されるテキストのテーブル</para>
    /// </summary>
    public class TextTable
    {
        private readonly Dictionary<string, string> table = new();

        private Lang lang = Lang.JP;

        /// <summary>
        /// <para>使用する言語を指定する</para>
        /// </summary>
        public void Bind(Lang lang)
        {
            this.lang = lang;
        }

        public void Set(string id, string texts = "")
        {
            this.Set(id, texts, this.lang);
        }

        /// <summary>
        /// <para>テーブルにテキストを追加する</para>
        /// </summary>
        public void Set(string id, string texts = "", Lang lang = Lang.EN)
        {
            this.table[Key(id, lang)] = texts;
        }

        public string Get(string id)
        {
            return this.Get(id, this.lang);
        }

        /// <summary>
        /// <para>テーブルに追加されたテキストを取得する</para>
        /// </summary>
        public string Get(string id, Lang lang = Lang.EN)
        {
            string key = this.Key(id, lang);

            return this.table.ContainsKey(key) ? this.table[key] : id;
        }

        private string Key(string id, Lang lang)
        {
            return $"[{lang}]{id}";
        }

        /// <summary>
        /// <para>言語を表す定数</para>
        /// </summary>
        public enum Lang
        {
            EN,
            JP
        }
    }
}
