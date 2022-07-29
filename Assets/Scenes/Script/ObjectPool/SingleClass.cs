using System;
using System.Threading;

namespace Assets.Low_Poly_FPS_Pack.Components.Scripts.ObjectPool
{
    public class SingleClass<T>
    {
        private static SingleClass<T> singleObject = null;
        protected SingleClass()
        {
            Thread.Sleep(5000);
            Console.WriteLine("当前实例对象:{0},当前线程ID:{1}", this.GetType(), Thread.CurrentThread.ManagedThreadId);
        }
        public static SingleClass<T> CreateSingleClass()
        {
            if (singleObject == null)
            {
                singleObject = new SingleClass<T>();
            }
            return singleObject;
        }

        public void ShowText()
        {
            Console.WriteLine("调用显示方法");
        }
    }
}