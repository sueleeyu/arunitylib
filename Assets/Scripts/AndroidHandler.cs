using UnityEngine;
using UnityEngine.UI;

namespace UnityEngine.XR.ARFoundation.Samples
{
    public class AndroidHandler : MonoBehaviour
    {       
        private AndroidJavaObject javaObject;//MainActivity对象

        [SerializeField]
        private Text messageText;//unity场景显示Text
        [SerializeField]
        private Text receiveMessageText;//unity场景显示Text

        // Start is called before the first frame update
        void Start()
        {
            //第二种
            AndroidJavaClass android = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            javaObject = android.GetStatic<AndroidJavaObject>("currentActivity");
        }
        public void OnClickToJavaFunSum()
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.MainActivity");
            AndroidJavaObject jo = jc.CallStatic<AndroidJavaObject>("GetInstance");
            messageText.text = jo.Call<int>("Sum", 5, 3).ToString();
        }


        public void OnClickToJavaFunSum2()
        {            
            messageText.text = javaObject.Call<int>("Sum", 5, 3).ToString();
        }       

        public void OnClickCall1()//android调用unity的启动示例，unity调用android的CallUnityFun方法，通过UnityPlayer.UnitySendMessage向unity发送消息
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.MainActivity");//MainActivity继承UnityPlayerActivity
            AndroidJavaObject jo = jc.CallStatic<AndroidJavaObject>("GetInstance");//GetInstance方法获取的当前对象
            jo.Call("CallUnityFun", "Test2");//调用android端方法CallUnityFun
        }
        public void OnClickCall2()
        {
            javaObject.Call("CallUnityFun", "Test1");//调用android端方法CallUnityFun
        }

        //接收来自android的参数
        public void Receive(string str)
        {
            receiveMessageText.text = str;
        }
    }
}