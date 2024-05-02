using System.Collections;
using System.Threading.Tasks;
using Firebase.Auth;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using System.IO;

public class AuthencationService : MonoBehaviour
{
    [SerializeField] private FirebaseAuth auth;
    [SerializeField] private DatabaseReference firebaseDBRef;
    private PlayerService playerService;
    [SerializeField] private string pathName;

    private void Awake()
    {
        firebaseDBRef = FirebaseDatabase.DefaultInstance.RootReference;
        playerService = GoogleServiceManager.Instance.PlayerService;
    }
    private void Start()
    {
        GoogleServiceManager.Instance.DisableServiceUseGoogle += SetLoginFalse;
        SignIn();
        playerService.GetUserDataWithAuthentication();
    }

    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
            {
                auth = FirebaseAuth.DefaultInstance;
                Credential credential = PlayGamesAuthProvider.GetCredential(code);
                StartCoroutine(GetAuthentication(credential));
            });
        }
        else
        {
            GoogleServiceManager.Instance.DisableServiceUseGoogle();
        }
    }

    IEnumerator GetAuthentication(Credential credential)
    {
        Task<FirebaseUser> task = auth.SignInWithCredentialAsync(credential);
        
        while (!task.IsCompleted) yield return null;
        if (task.IsCanceled || task.IsFaulted)
        {
            GoogleServiceManager.Instance.DisableServiceUseGoogle();
        }
        else
        {
            UserData userData = GameDataController.Instance.GoogleUserData;
            FirebaseUser user = task.Result;
            if(user.Metadata.CreationTimestamp == user.Metadata.LastSignInTimestamp) {
                playerService.RegisterUser();
                playerService.GetUserDataWithAuthentication();
            }
            userData.IsLogin = true;
            userData.UserId = user.UserId;
        }
    }

    private void SetLoginFalse(){
        GameDataController.Instance.GoogleUserData.IsLogin = false;
    }
}


