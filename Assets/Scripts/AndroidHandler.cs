using UnityEngine;
using UnityEngine.UI;

namespace UnityEngine.XR.ARFoundation.Samples
{
    public class AndroidHandler : MonoBehaviour
    {       
        private AndroidJavaObject javaObject;//MainActivity����

        [SerializeField]
        private Text messageText;//unity������ʾText
        [SerializeField]
        private Text receiveMessageText;//unity������ʾText

        // Start is called before the first frame update
        void Start()
        {
            //�ڶ���
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

        public void OnClickCall1()//android����unity������ʾ����unity����android��CallUnityFun������ͨ��UnityPlayer.UnitySendMessage��unity������Ϣ
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.MainActivity");//MainActivity�̳�UnityPlayerActivity
            AndroidJavaObject jo = jc.CallStatic<AndroidJavaObject>("GetInstance");//GetInstance������ȡ�ĵ�ǰ����
            jo.Call("CallUnityFun", "Test2");//����android�˷���CallUnityFun
        }
        public void OnClickCall2()
        {
            javaObject.Call("CallUnityFun", "Test1");//����android�˷���CallUnityFun
        }

        //��������android�Ĳ���
        public void Receive(string str)
        {
            receiveMessageText.text = str;
        }
    }
}