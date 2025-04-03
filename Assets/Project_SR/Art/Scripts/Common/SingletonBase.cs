using UnityEngine;

namespace SR
{
    public class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    // ������ �Ҵ�� �ν��Ͻ��� �ִ��� Ȯ��
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        // ���ο� GameObject�� �����Ͽ� �̱��� �ν��Ͻ� �߰�
                        GameObject singletonObject = new GameObject(typeof(T).Name);
                        _instance = singletonObject.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

        // �� ��ȯ �� �ı����� �ʵ��� ����
        public virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
