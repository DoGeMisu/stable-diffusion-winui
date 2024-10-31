using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace stable_diffusion_winui.Tool
{
    public class NetTool
    {
        public static dynamic Data;
        public static string Get(string URL)
        {
            Data = null;
            if (URL != null)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = client.GetAsync(URL).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = response.Content.ReadAsStringAsync().Result;

                        Data = JsonConvert.DeserializeObject(jsonResponse);
                    }
                    else
                    {
                        Console.WriteLine("请求失败: {0}", response.StatusCode);
                    }
                }
            }
            else
            {
                Console.WriteLine("URL is Null");
            }

            return Data;
        }

        public static string Post(string URL,string PostData)
        {
            Data = null;
            // 创建 HttpClient 对象
            using (HttpClient client = new HttpClient())
            {
                // 设置请求头
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // 将数据转换为 JSON 字符串
                string jsonContent = JsonConvert.SerializeObject(PostData);

                // 创建 POST 请求内容
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // 发送 POST 请求
                HttpResponseMessage response = client.PostAsync(URL, content).Result;

                // 检查响应状态码
                if (response.IsSuccessStatusCode)
                {
                    // 读取响应内容
                    string jsonResponse = response.Content.ReadAsStringAsync().Result;

                    // 解析 JSON 数据
                    Data = JsonConvert.DeserializeObject(jsonResponse);
                }
                else
                {
                    Console.WriteLine("请求失败: {0}", response.StatusCode);
                }
            }

            return Data;
        }
    }
}
