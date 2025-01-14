using UnityEngine;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Text;

public class FTPManager : MonoBehaviour
{
    public static FTPManager Instance;
    
    public string serverPath = "ftp://metaverse.ohgiraffers.com/test04/Hephaistus/";
    public string m_UserName = "test04";
    public string m_Password = "pass04";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    /*private async void Start()
    {
        string filePath = "info/f3962c8c-1579-4fbb-a73b-bacf22152fe5.json";
        byte[] test = await FtpDownloadJson(filePath);
        string str = Encoding.Default.GetString(test);
        print(str);
    }*/

    public Task FtpUpload(string filePath, byte[] data)
    {
        FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(serverPath + filePath);
        ftpWebRequest.Credentials = new NetworkCredential(m_UserName, m_Password);
        //ftpWebRequest.EnableSsl = true; // TLS/SSL
        ftpWebRequest.UseBinary = false;   // ASCII, Binary(디폴트)
        ftpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;

        //byte[] data = File.ReadAllBytes(@"D:\upload.txt");

        using (var ftpStream = ftpWebRequest.GetRequestStream())
        {
            ftpStream.Write(data, 0, data.Length);
        }

        using (var response = (FtpWebResponse)ftpWebRequest.GetResponse())
        {
            Debug.Log(response.StatusDescription);
        }
        
        return Task.CompletedTask;
    }

    public Task<byte[]> FtpDownloadJson(string filePath)
    {
        FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(serverPath + filePath);
        ftpWebRequest.Credentials = new NetworkCredential(m_UserName, m_Password);
        ftpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;

        byte[] data = null;
        
        using (WebResponse response = ftpWebRequest.GetResponse())
        using (Stream responseStream = response.GetResponseStream())
        {
            //byte[] data = null;

            if (responseStream != null)
            {
                byte[] buffer = new byte[16 * 1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    int read;
                    while ((read = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    data = ms.ToArray();
                }
            }
        }
        return Task.FromResult(data);
    }

    public Task<Texture2D> FtpDownloadImage(string filePath)
    {
        FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(serverPath + filePath);
        ftpWebRequest.Credentials = new NetworkCredential(m_UserName, m_Password);
        ftpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;

        // 크기가 너무 작음
        //Texture2D tex = new Texture2D(2, 2);
        Texture2D tex = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        //print("filePath "+ filePath);
        //print(serverPath + filePath);
        using (WebResponse response = ftpWebRequest.GetResponse())
        using (Stream responseStream = response.GetResponseStream())
        {
            byte[] data = null;

            if (responseStream != null)
            {
                byte[] buffer = new byte[16 * 1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    int read;
                    while ((read = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    data = ms.ToArray();
                }
                //tex.LoadImage(data);
                tex.LoadRawTextureData(data);
                tex.Apply();
            }
        }
        return Task.FromResult(tex);
    }

    public Texture2D BytesToTexture2D(byte[] imageBytes)
    {
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(imageBytes);
        return tex;
    }
}