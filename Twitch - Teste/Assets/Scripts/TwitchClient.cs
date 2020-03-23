using System.Collections;
using System.Collections.Generic;
using TwitchLib.Client.Models;
using TwitchLib.Unity;
using UnityEngine;
using UnityEngine.UI;

public class TwitchClient : MonoBehaviour
{
    public InputField inputField;
    public Text feedbackTxt;
    private bool connected = false;

    public Client client;
    private string channel_name;

    void Start()
    {
        Application.runInBackground = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && connected)
        {
            client.SendMessage(client.JoinedChannels[0], "This is a test message from the bot.");
            feedbackTxt.text = "The bot sent a message.";
        }
    }

    void ConnectClient()
    {
        ConnectionCredentials credentials = new ConnectionCredentials("starfruitgamestudio", Secrets.bot_access_token);
        client = new Client();
        client.Initialize(credentials, channel_name);

        client.OnConnected += OnConnected;
        client.OnMessageReceived += OnMessageReceived;

        client.Connect();
        connected = true;
    }

    private void OnConnected(object sender, TwitchLib.Client.Events.OnConnectedArgs e)
    {
        Debug.Log($"The bot {e.BotUsername} succesfully connected to Twitch.");
        feedbackTxt.text = $"The bot {e.BotUsername} succesfully connected to Twitch.";

        /*if (!string.IsNullOrWhiteSpace(e.AutoJoinChannel))
            Debug.Log($"The bot will now attempt to automatically join the channel provided when the Initialize method was called: {e.AutoJoinChannel}");*/
    }

    private void OnMessageReceived(object sender, TwitchLib.Client.Events.OnMessageReceivedArgs e)
    {
        Debug.Log($"Message received from {e.ChatMessage.Username}: {e.ChatMessage.Message}");
        feedbackTxt.text = $"Message received from {e.ChatMessage.Username}: {e.ChatMessage.Message}";
    }

    public void EnterUsername()
    {
        if(!connected)
        {
            channel_name = inputField.text;
            ConnectClient();
        }
    }
}
