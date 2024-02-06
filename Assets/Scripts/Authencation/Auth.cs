using Firebase.Auth;
using Firebase.Extensions;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames.BasicApi.SavedGame;
using Firebase;

public class Auth : MonoBehaviour
{
    [SerializeField] TMP_Text textBox;
    [SerializeField] Text NoteCreateUser;
    string id;
    // Start is called before the first frame update
    public void Login()
    {
        try
        {
            textBox.text += "Authenticate";
            PlayGamesPlatform.Activate();
            
            //Auth with Google Play
            PlayGamesPlatform.Instance.Authenticate(status =>
            {
                if (status == SignInStatus.Success)
                {
                    textBox.text += "\nSuccess\nRequestServerSideAccess";
                    
                    try
                    {
                        PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>{
                            Debug.Log("Bug game");
                            NoteCreateUser.text = code;
                        });
                        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                            var dependencyStatus = task.Result;
                            if (dependencyStatus == Firebase.DependencyStatus.Available)
                            {
                                // Create and hold a reference to your FirebaseApp,
                                // where app is a Firebase.FirebaseApp property of your application class.
                                FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;

                                // Set a flag here to indicate whether Firebase is ready to use by your app.
                            }
                            else
                            {
                                UnityEngine.Debug.LogError(System.String.Format(
                                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                                // Firebase Unity SDK is not safe to use here.
                            }
                        });
                        //Ask for Auth code - This is why we need the Web Server Client ID's and secrets from Firebase
                        PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                        {
                            FirebaseAuth auth = FirebaseAuth.DefaultInstance;
                            Credential credential = PlayGamesAuthProvider.GetCredential(code);
                            NoteCreateUser.text = code;
                            //Firebase.Auth.FirebaseUser user = auth.CurrentUser;
                            //if(user != null && user.IsValid())
                            //{
                            //    string playerName = user.DisplayName;
                            //    string uid = user.UserId;
                            //}
                            //move Firebase get to Coroutine so we can log to our textbox
                            StartCoroutine(AuthGet());

                            IEnumerator AuthGet()
                            {
                                System.Threading.Tasks.Task<FirebaseUser> task = auth.SignInWithCredentialAsync(credential);

                                while (!task.IsCompleted) yield return null;

                                if (task.IsCanceled)
                                {
                                    textBox.text += "\ncanceled";
                                }
                                else if (task.IsFaulted)
                                {
                                    textBox.text += "\nerror " + task.Exception;
                                }
                                else
                                {
                                    string googlePlayId = PlayGamesPlatform.Instance.GetUserId();
                                    id = googlePlayId;
                                    //string displayName = PlayGamesPlatform.Instance.GetUserDisplayName();
                                    //FirebaseUser newUser = task.Result;
                                    //CreateUserInFirebase(auth, googlePlayId, displayName);
                                    textBox.text += "\n\nFULLY AUTHENTICATED\n\n";
                                    //textBox.text += newUser.ToString();
                                }
                            }
                        });
                    }
                    catch (Exception e)
                    {
                        textBox.text += "RequestServerSideAccess error\n" + e.ToString();
                    }
                }
                else textBox.text += "Failure";
            });
        }
        catch (Exception e)
        {
            textBox.text += "Authenticate error\n" + e.ToString();
        }
    }

    private void CreateUserInFirebase(FirebaseAuth auth, string userId, string displayName)
    {
        auth.CreateUserWithEmailAndPasswordAsync(userId + "@gmail.com", "defaultPassword123")
            .ContinueWithOnMainThread(task => {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.LogError("Failed to create Firebase user: " + task.Exception);
                }
                else
                {
                    // T?o tài kho?n thành công, có th? l?y thông tin ng??i dùng Firebase t? task.Result.User
                    var firebaseUser = task.Result.User;
                    Debug.Log("Firebase user created successfully: " + firebaseUser.UserId);

                    // C?p nh?t thông tin ng??i dùng Firebase (ví d?: displayName)
                    UserProfile userProfile = new UserProfile { DisplayName = displayName };
                    firebaseUser.UpdateUserProfileAsync(userProfile).ContinueWithOnMainThread(updateTask => {
                        if (updateTask.IsCompleted)
                        {
                            Debug.Log("Firebase user profile updated successfully");
                        }
                        else
                        {
                            Debug.LogError("Failed to update Firebase user profile: " + updateTask.Exception);
                        }
                    });
                }
            });
    }
}