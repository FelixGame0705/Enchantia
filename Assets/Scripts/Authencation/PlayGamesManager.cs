using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
public class PlayGamesManager : MonoBehaviour
{
    public TextMeshProUGUI DetailsText;
    // Start is called before the first frame update
    void Start()
    {
        SignIn();
    }

    public void SignIn()
    {
        PlayGamesPlatform.Activate();

        // ??ng nh?p v�o Google Play Services
        Social.localUser.Authenticate(success => {
            if (success)
            {
                Debug.Log("??ng nh?p th�nh c�ng v�o Google Play Services");
                DetailsText.text = "Success \n ";
            }
            else
            {
                Debug.LogWarning("??ng nh?p th?t b?i v�o Google Play Services");
                DetailsText.text = "Failure \n ";
            }
        });
       // PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }
    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            // Continue with Play Games Services

            string name = PlayGamesPlatform.Instance.GetUserDisplayName();
            string id = PlayGamesPlatform.Instance.GetUserId();
            string ImgUrl = PlayGamesPlatform.Instance.GetUserImageUrl();


            DetailsText.text = "Success \n " + name;

        }
        else
        {
            DetailsText.text = "Sign in Failed!!";

            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }

}