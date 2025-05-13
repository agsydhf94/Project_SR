using UnityEngine;
using TMPro;
using Photon.Pun;

namespace SR
{
    public class PhotonLauncher : MonoBehaviourPunCallbacks
    {
        private MenuManager menuManager;
        [SerializeField] TMP_InputField roomNameInputField;
        [SerializeField] TMP_Text errorText;
        [SerializeField] TMP_Text roomNameText;

        private void Start()
        {
            // Connects to the master server
            Debug.Log("Connecting to master server");
            PhotonNetwork.ConnectUsingSettings();
            menuManager = MenuManager.Instance;
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to master server");
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            menuManager.OpenMenu("title");
            Debug.Log("Joined to lobby");
        }

        public void CreateRoom()
        {
            if (string.IsNullOrEmpty(roomNameInputField.text))
                return;

            PhotonNetwork.CreateRoom(roomNameInputField.text);
            menuManager.OpenMenu("loading");
        }

        public override void OnJoinedRoom()
        {
            menuManager.OpenMenu("roomMenu");
            roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            errorText.text = "Room Creation Failed " + message;
            menuManager.OpenMenu("errorMenu");
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            menuManager.OpenMenu("loading");
        }

        public override void OnLeftRoom()
        {
            menuManager.OpenMenu("title");
        }
    }
}
