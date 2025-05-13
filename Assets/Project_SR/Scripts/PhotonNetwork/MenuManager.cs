using UnityEngine;

namespace SR
{
    public class MenuManager : SingletonBase<MenuManager>
    {
        [SerializeField] private MenuUI[] menuUIs;

        public void OpenMenu(string menuName)
        {
            for(int i = 0; i < menuUIs.Length; i++)
            {
                if (menuUIs[i].menuName == menuName)
                {
                    OpenMenuUI(menuUIs[i]);
                }
                else if (menuUIs[i].isOpen)
                {
                    CloseMenuUI(menuUIs[i]);
                }
            }
        }

        public void OpenMenuUI(MenuUI menuUI) => menuUI.Open();


        public void CloseMenuUI(MenuUI menuUI) => menuUI.Close();



    }
}
