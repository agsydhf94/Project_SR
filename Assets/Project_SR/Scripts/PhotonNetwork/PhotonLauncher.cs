using UnityEngine;
using TMPro;
using Photon.Pun;
using System.Collections.Generic;
using Photon.Realtime;

namespace SR
{
    public class PhotonLauncher : MonoBehaviourPunCallbacks
    {
        public static PhotonLauncher Instance { get; private set; }

        private MenuManager menuManager;
        [SerializeField] private TMP_InputField roomNameInputField;
        [SerializeField] private TMP_Text errorText;
        [SerializeField] private TMP_Text roomNameText;
        [SerializeField] private Transform roomListContent;
        [SerializeField] private GameObject roomListItemPrefab;

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

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

        public void JoinRoom(RoomInfo roomInfo)
        {
            PhotonNetwork.JoinRoom(roomInfo.Name);
            menuManager.OpenMenu("loading");
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

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            foreach(Transform _transform in roomListContent)
            {
                Destroy(_transform.gameObject);
            }

            for(int i = 0; i < roomList.Count; i++)
            {
                Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
            }
        }
    }
}
