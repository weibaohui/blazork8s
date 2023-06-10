using System.Collections.Generic;

namespace Entity;

public class RockAI
{
    public class SlotsItem
    {
        /// <summary>
        ///
        /// </summary>
        public string sign_value { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string matchtext { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string orig_sign_value { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string inherit { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string name { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string normvalue { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string end { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string value { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string begin { get; set; }
    }

    public class Semantic
    {
        /// <summary>
        ///
        /// </summary>
        public List<SlotsItem> slots { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string intent { get; set; }
    }

    public class Answer
    {
        /// <summary>
        ///
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// </summary>
        public string text { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string type { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string icons { get; set; }
    }

    public class Root
    {
        /// <summary>
        ///
        /// </summary>
        public Semantic semantic { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string code { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Answer answer { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        public string codedesc { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string service { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string msgid { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string source { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string text { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string sid { get; set; }
    }

    public class Chat
    {
        public string appid { get; set; }
        public string sid   { get; set; }
        public string text  { get; set; }
    }
}
