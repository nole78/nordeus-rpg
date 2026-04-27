using Assets.Scripts.DTOs;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ApiClient : MonoBehaviour
{
    private const string BASE_URL = "http://localhost:5192"; // change if needed

    public IEnumerator GetRunConfig(System.Action<RunConfigResponse> onSuccess,
                                    System.Action<string> onError)
    {
        var url = $"{BASE_URL}/Game/run-config";

        using var request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            onError?.Invoke(request.error);
            yield break;
        }

        var json = request.downloadHandler.text;

        // TODO handle exception here
        var data = JsonUtility.FromJson<RunConfigResponse>(json);
        onSuccess?.Invoke(data);
    }

    public IEnumerator SendNextMove(NextMoveRequest requestData,
                                   System.Action<NextMoveResponse> onSuccess,
                                   System.Action<string> onError)
    {
        var url = $"{BASE_URL}/Game/next-move";

        var json = JsonUtility.ToJson(requestData);
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
        var data = JsonUtility.FromJson<NextMoveResponse>(responseJson);
        onSuccess?.Invoke(data);
    }
}
