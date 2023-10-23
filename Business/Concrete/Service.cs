using Business.Abstract;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text;

namespace Business.Concrete
{
    public class Service<T> : IService<T> where T : class
    {
        public string bearerToken = "071fb19d77f647fa2cd8b3fd820ba229";
        List<T> emptyResponse = new();
        public async Task<string> Complete(string file, string last_code)
        {
            var url = "challenge.photier.com/complete";
            Guid guid = Guid.NewGuid();
            var zipTargetPath = $"{file}-{guid}-PhotierProject.zip";
            ZipFile.CreateFromDirectory(file, zipTargetPath);
            Console.WriteLine("The folder has been successfully compressed into zip file.");

            using (HttpClient client = new HttpClient())
            {
                var formDatas = new MultipartFormDataContent();
                byte[] zipContent = System.IO.File.ReadAllBytes(zipTargetPath);
                var content = new ByteArrayContent(zipContent);
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = zipTargetPath
                };
                formDatas.Add(content, "FILE");
                var textContent = new StringContent(last_code, Encoding.UTF8);
                formDatas.Add(textContent, "CODE");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                HttpResponseMessage response = await client.PostAsync(url, formDatas);
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
        }
        public async Task<List<T>> DeleteAsync(int id)
        {
            var url = $"https://challenge.photier.com/todos?id={id}";
            using (HttpClient client = new())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                var response = await client.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<T>>(responseBody);
                    return result;
                }
                return emptyResponse;
            }
        }
        public async Task<List<T>> GetAsync(string query) //Search
        {
            var url = $"https://challenge.photier.com/todos/search?query={query}";
            using (HttpClient client = new())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<T>>(responseBody);
                    return result;
                }
                return emptyResponse;
            }
        }
        public async Task<List<T>> GetListAsync()
        {
            var url = "https://challenge.photier.com/todos";
            using (HttpClient client = new())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<T>>(responseBody);
                    return result;
                }
                return emptyResponse;
            }
        }
        public async Task<string> Start(string mail)
        {
            var url = "https://challenge.photier.com/start";
            using (HttpClient client = new())
            {
                var formData = new KeyValuePair<string, string>("email", mail);
                var encodedFormData = new FormUrlEncodedContent(new List<KeyValuePair<string, string>> { formData });

                var response = await client.PostAsync(url, encodedFormData);
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
        }
    }
}
