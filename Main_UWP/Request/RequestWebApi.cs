using MVVM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Main_UWP.Request
{
    public class RequestWebApi
    {
        private static RequestWebApi _instance;
        public static RequestWebApi Request
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RequestWebApi();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Get 요청
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public string GetRequest(string controller)
        {
            HttpWebRequest request = null;
            string result = string.Empty;
            try
            {
                Uri uri = new Uri("https://localhost:44373/api/"+ controller);
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = WebRequestMethods.Http.Get;
                request.Timeout = 5000;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader streamReader = new StreamReader(responseStream, System.Text.Encoding.UTF8))
                        {
                            result = streamReader.ReadToEnd();
                            //ConnectionList = JsonConvert.DeserializeObject<List<ConnectionModel>>(result).ToObservableCollection();
                        }
                    }
                }
                return result;

            }
            catch (Exception ex)
            {
                CommonFeature.Feature.ShowMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Post 요청
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public string PostRequest<T>(string controller,T postData)
        {
            HttpWebRequest request = null;
            string result = string.Empty;
            //ConnectionModel savedData = null;
            try
            {
                Uri uri = new Uri("https://localhost:44373/api/" + controller);
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = WebRequestMethods.Http.Post;
                request.Timeout = 5000;

                // 인코딩 UTF-8
                byte[] data = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(postData));
                request.ContentType = "application/json";
                request.ContentLength = data.Length;

                // 데이터 전송
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(data, 0, data.Length);

                    // 전송 응답
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            using (StreamReader streamReader = new StreamReader(responseStream, System.Text.Encoding.UTF8))
                            {
                                result = streamReader.ReadToEnd();                     
                                //savedData = JsonConvert.DeserializeObject<ConnectionModel>(result);
                            }
                        }
                    }
                }
                return result;

            }
            catch (Exception ex)
            {
                CommonFeature.Feature.ShowMessage(ex.Message);
                return null;
            }
        }
    }
}
