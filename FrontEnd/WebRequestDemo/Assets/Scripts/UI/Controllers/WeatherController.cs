using Azusa.Unity.Util.Async;
using Azusa.WeatherApi.UnitySharedDomain.Const;
using Azusa.WeatherApi.UnitySharedDomain.Entities;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace UI.Controllers
{
    public class WeatherController : MonoBehaviour
    {
        private const string BaseUrl = "localhost:7890";

        /// <summary>
        /// 获取天气信息
        /// </summary>
        /// <param name="location">地区</param>
        /// <param name="isMock">获取模拟数据</param>
        /// <returns></returns>
        public IAsyncTask<WeatherInfo[]> GetRecentWeatherInfoAsync(string location, bool isMock = false)
        {
            var url = BaseUrl + (isMock ? ApiRoutes.Weather.Mock : ApiRoutes.Weather.AMap) + $"?address={location}";
            print($"Requesting to {url}");
            // 用静态方法创建对象，HTTP方法为GET
            var req = UnityWebRequest.Get(url);
            req.timeout = 5; // 5秒超时
            
            
            // 创建异步任务
            return AsyncActionBase.Create<WeatherInfo[]>(task => // 任务委托
                    {
                        var op = req.SendWebRequest(); // 发送网络请求
                        op.completed += operation => // 监听完成事件
                        {
                            if (req == null) // 如果任务资源被释放，则停止执行
                            {
                                Debug.LogWarning("任务已取消");
                                return;
                            }
                            
                            var webReq = ((UnityWebRequestAsyncOperation)operation).webRequest;
                            if (webReq.isNetworkError) // 网络错误
                            {
                                task.Error($"无法连接到服务器，请检查网络连接"); // 任务失败
                            }
                            else if (webReq.isHttpError) // 非正常Http响应
                            {
                                var codePrefix = webReq.responseCode.ToString()[0];
                                switch (codePrefix)
                                {
                                    // Http 4xx
                                    case '4':
                                        task.Error("请求参数有误，请检查输入的地址");
                                        break;
                                    // Http 5xx
                                    case '5':
                                        task.Error("无法获取数据，服务器内部错误");
                                        break;
                                    default:
                                        task.Error("无法获取数据，未知错误");
                                        break;
                                }
                            }
                            else
                            {
                                print(webReq.downloadHandler.text);
                                // 任务完成，返回反序列化后的数据
                                task.Complete(
                                    JsonConvert.DeserializeObject<WeatherInfo[]>(webReq.downloadHandler.text));
                            }
                        };
                    },
                    () => { req?.Abort(); }, // 如果调用方取消，则关闭连接
                    false, // 发生异常不抛出
                    true // 立即执行
                ).WhenInterrupted(_ => Debug.LogWarning("服务器连接失败")) // 当任务被中断打印信息
                .WhenFinished(_ =>
                {
                    req.Dispose(); // 任务结束时释放资源
                    req = null; // 将引用置空作为资源已释放的标识
                });
        }
    }
}