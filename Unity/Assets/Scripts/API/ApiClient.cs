using NordeusRPG.DTOs;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;

public class ApiClient : MonoBehaviour
{
    public static ApiClient Instance;
    private const string BASE_URL = "http://localhost:5192"; // change if needed
    private static readonly JsonSerializerSettings Settings = new()
    {
        MissingMemberHandling = MissingMemberHandling.Ignore,
        NullValueHandling = NullValueHandling.Ignore,
        ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
    };
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public IEnumerator GetRunConfig(Action<RunConfigResponse> onSuccess, Action<string> onError)
    {
        var url = $"{BASE_URL}/api/Game/run-config";

        using var request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            onError?.Invoke(request.error);
            yield break;
        }

        var json = request.downloadHandler.text;

        // TODO handle exception here
        try
        {
            var data = JsonConvert.DeserializeObject<RunConfigResponse>(json, Settings);
            onSuccess?.Invoke(data);
        }
        catch (Exception ex)
        {
            onError?.Invoke(ex.Message);
        }
    }

    public IEnumerator SendNextMove(NextMoveRequest requestData, Action<NextMoveResponse> onSuccess, Action<string> onError)
    {
        var url = $"{BASE_URL}/api/Game/next-move";

        var json = JsonConvert.SerializeObject(requestData,Settings);
        var bodyRaw = Encoding.UTF8.GetBytes(json);

        using var request = new UnityWebRequest(url, "POST");

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            onError?.Invoke(request.error + "\n" + request.downloadHandler.text);
            yield break;
        }

        var responseJson = request.downloadHandler.text;

        // TODO handle exception here
        try
        {
            var data = JsonConvert.DeserializeObject<NextMoveResponse>(responseJson,Settings);
            onSuccess?.Invoke(data);
        }
        catch(Exception ex)
        {
            onError?.Invoke(ex.Message);
        }
    }
}
