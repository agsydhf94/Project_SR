using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

namespace SR
{
    public class PlayerListItem : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_Text playerText;
        private Player player;

        public void SetUp(Player _player)
        {
            player = _player;
            playerText.text = _player.NickName;
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            if(player == otherPlayer)
            {
                Destroy(gameObject);
            }
        }

        public override void OnLeftRoom()
        {
            Destroy(gameObject);
        }
    }
}
