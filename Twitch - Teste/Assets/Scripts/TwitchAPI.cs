using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib.Unity;
using TwitchLib.Api.Models.Undocumented.Chatters;

public class TwitchAPI : MonoBehaviour
{
    public Api api;

    void Start()
    {
        Application.runInBackground = true;
        api = new Api();
        api.Settings.AccessToken = Secrets.bot_access_token;
        api.Settings.ClientId = Secrets.client_id;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            api.Invoke(
                api.Undocumented.GetChattersAsync("topengy"), //client.JoinedChannels[0].Channel
                GetChatterListCallback
                );
        }
    }

    private void GetChatterListCallback(List<ChatterFormatted> listOfChatters)
    {
        Debug.Log("List of " + listOfChatters.Count + " Viewers: ");
        foreach(var chatterObject in listOfChatters)
        {
            Debug.Log(chatterObject.Username);
        }
    }
}
