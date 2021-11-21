using System;

namespace Blazor.MetaEvent
{
    public class EventTest
    {
        private static EventTest tt;

        private string name;

        public delegate void NumManipulationHandler(string name);


        public event NumManipulationHandler ChangeNum;

        protected virtual void OnNumChanged()
        {
            Console.WriteLine("OnNumChanged");
            if (ChangeNum != null)
            {
                ChangeNum(name); /* 事件被触发 */
            }
        }


        public EventTest()
        {
            Console.WriteLine("EventTest初始化");
        }

        public static EventTest GetInstance()
        {
            if (tt == null)
            {
                tt = new EventTest();
            }

            return tt;
        }

        public void SetValue(string name)
        {
            Console.WriteLine($"SetValue初始化{name}");
            this.name = name;
            OnNumChanged();
        }
    }
}
