using TMPro;
using UnityEngine;
using Photon.Realtime;

namespace SR
{
    public class RoomListItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text roomNameText;
        private RoomInfo roomInfo;

        public void SetUp(RoomInfo _roomInfo)
        {
            roomInfo = _roomInfo;
            roomNameText.text = _roomInfo.Name;
        }

        public void OnClick()
        {
            PhotonLauncher.Instance.JoinRoom(roomInfo);
        }
    }
}
