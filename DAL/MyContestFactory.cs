using System.Runtime.Remoting.Messaging;

namespace DAL
{
    public class MyContestFactory
    {
        public static MyDBContext GetMyDBContext()
        {
            //通过CallContext数据槽，可以实现线程类实例唯一的功能
            MyDBContext context = CallContext.GetData("context") as MyDBContext;
            if (context == null)
            {
                context = new MyDBContext();
                CallContext.SetData("context", context);
            }
            return context;
        }
    }
}
