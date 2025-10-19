namespace NewLandPackages
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Networking;
    using UnityEngine.UI;

    public class Root
    {
        public int user_num { get; set; }
        public string user_id { get; set; }
        public string user_pw { get; set; }
        public int user_price { get; set; }
        public string user_name { get; set; }
        public int user_status { get; set; }
    }

    public class NW_NAccount : MonoBehaviour
    {
        private const string ACCOUNT_SERVER_URL = "http://newland2019.kro.kr:8002/accounts";

        // login condition and user number
        public static bool login;
        public static int userNum;

        // Panels
        [SerializeField]
        private GameObject newLandAccountPanel;
        [SerializeField]
        private GameObject waitPanel;

        // Id Input Field and Password Input Field
        [SerializeField]
        private InputField idField;
        [SerializeField]
        private InputField passwordField;

        // Wait panel in logging text
        [SerializeField]
        private Text loggingTxt;

        // IdField and PasswordField of animator controller
        [SerializeField]
        private Animator inputFieldAnim;

        // logging data and current index
        private string[] loggingData = new string[3] { ".", "..", "..." };

        private int currentIndex;

        private static List<Root> datas;

        [SerializeField]
        private GameObject ui;

        private void Awake()
        {
            if (!login)
            {
                ui.SetActive(true);
            }
        }

        private IEnumerator Start()
        {
            StartCoroutine(UpdateLoggingText());

            while (!login)
            {
                newLandAccountPanel.SetActive(true);

                yield return null;
            }
        }

        // On clicked login button
        public void LoginButtonClick()
        {
            if (idField.text.Length > 0 && passwordField.text.Length > 0)
            {
                StartCoroutine(NewLandAccountLogin());
            }
            else
            {
                inputFieldAnim.SetTrigger("Oscillate");
            }
        }

        private IEnumerator NewLandAccountLogin()
        {
            waitPanel.SetActive(true);

            StartCoroutine(LoadDataToWeb());

            yield return new WaitForSeconds(Random.Range(2f, 5f));

            for (int i = 0; i < datas.Count; i++)
            {
                if (datas[i].user_id.Equals(idField.text) && datas[i].user_pw.Equals(passwordField.text))
                {
                    userNum = i;
                    login = true;

                    newLandAccountPanel.SetActive(false);

#if UNITY_EDITOR
                    Debug.Log("로그인 성공!");
#endif

                    break;
                }
            }

            idField.text = string.Empty;
            passwordField.text = string.Empty;

            waitPanel.SetActive(false);
        }

        // Update text to logging
        private IEnumerator UpdateLoggingText()
        {
            while (!login)
            {
                loggingTxt.text = loggingData[currentIndex];
                currentIndex = (currentIndex + 1) % loggingData.Length;

                yield return new WaitForSeconds(0.5f);
            }
        }

        private IEnumerator LoadDataToWeb()
        {
            UnityWebRequest www = UnityWebRequest.Get(ACCOUNT_SERVER_URL);

            yield return www.SendWebRequest();

            if (www.error != null)
            {
#if UNITY_EDITOR
                Debug.LogError(www.error);
#endif

                StopCoroutine(NewLandAccountLogin());

                idField.text = string.Empty;
                passwordField.text = string.Empty;

                waitPanel.SetActive(false);

                yield break;
            }

            datas = JsonToObject<Root>(www.downloadHandler.text);
        }

        private List<T> JsonToObject<T>(string jsonData) => JsonUtility.FromJson<List<T>>(jsonData);

        public static Root GetAccount() => datas[userNum];
    }
}
