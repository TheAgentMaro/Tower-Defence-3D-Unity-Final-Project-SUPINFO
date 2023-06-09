using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    #region Setup
    [SerializeField] GameObject joinChatButton;
    ChatClient chatClient;
    bool isConnected;

    public void ChatConnectOnClick()
    {
        isConnected = true;
        chatClient = new ChatClient(this);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(GameOptions.GetUsername()));
        Debug.Log("Connecting");
    }
    #endregion Setup

    #region General
    [SerializeField] GameObject chatPanel;
    [SerializeField] InputField chatField;
    [SerializeField] Text chatDisplay;

    // Update is called once per frame
    void Update()
    {
        if (isConnected && chatClient != null)
        {
            chatClient.Service();
        }

        if (chatField != null && !string.IsNullOrEmpty(chatField.text) && Input.GetKey(KeyCode.Return))
        {
            SubmitPublicChatOnClick();
        }
    }
    #endregion General

    #region PublicChat
    public void SubmitPublicChatOnClick()
    {
        if (!string.IsNullOrEmpty(chatField.text))
        {
            chatClient.PublishMessage("RegionChannel", chatField.text);
            chatField.text = "";
        }
    }
    public void TypeChatOnValueChange(string valueIn)
    {
    }
    #endregion PublicChat

    #region Callbacks
    public void DebugReturn(DebugLevel level, string message)
    {
    }

    public void OnChatStateChange(ChatState state)
    {
        Debug.Log("Chat state changed: " + state);

        if (state == ChatState.ConnectedToFrontEnd)
        {
            joinChatButton.SetActive(false);
            chatPanel.SetActive(true);
            chatClient.Subscribe(new string[] { "RegionChannel" });
        }
        else if (state == ChatState.Disconnected)
        {
            isConnected = false;
            joinChatButton.SetActive(true);
            chatPanel.SetActive(false);
        }
    }

    public void OnConnected()
    {
        Debug.Log("Connected");
    }

    public void OnDisconnected()
    {
        Debug.Log("Disconnected");
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        for (int i = 0; i < senders.Length; i++)
        {
            string sender = senders[i];
            string message = messages[i]?.ToString();

            if (!string.IsNullOrEmpty(message))
            {
                string msg = string.Format("{0}: {1}", sender, message);
                chatDisplay.text += "\n" + msg;
            }
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
 
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {

    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        Debug.Log("Subscribed to channels: " + string.Join(", ", channels));
    }

    public void OnUnsubscribed(string[] channels)
    {
    }

    public void OnUserSubscribed(string channel, string user)
    {
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
    }
    #endregion Callbacks
}
